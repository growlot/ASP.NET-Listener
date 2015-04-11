//-----------------------------------------------------------------------
// <copyright file="SiteInfo.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Globalization;
    using log4net;
    using ListenerModel = AMSLLC.Listener.Common.Model;
    using WnpModel = AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Implements SiteInfo web service interface
    /// </summary>
    public class SiteInfo : ISiteInfo
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The string manager
        /// </summary>
        private ResourceManager stringManager = Init.StringManager;

        /// <summary>
        /// The transaction log manager
        /// </summary>
        private ITransactionManager transactionLogManager;

        /// <summary>
        /// The device manager
        /// </summary>
        private IDeviceManager deviceManager;

        /// <summary>
        /// The WNP system
        /// </summary>
        private WNPSystem wnpSystem;

        /// <summary>
        /// The site info response message
        /// </summary>
        private ListenerModel.Site response;

        /// <summary>
        /// The owner identifier
        /// </summary>
        private int ownerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteInfo"/> class.
        /// </summary>
        public SiteInfo()
        {
            this.transactionLogManager = StaticPersistence.TransactionLogManager;
            this.deviceManager = StaticPersistence.DeviceManager;
            this.wnpSystem = StaticPersistence.WnpSystem;
            this.ownerId = int.Parse(ConfigurationManager.AppSettings["WecoMobile.Owner"], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Web service contract for receiving site information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The site information
        /// </returns>
        public ListenerModel.Site GetSiteInfo(SiteInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            Log.Info("GetSiteInfo started.");

            IList<ListenerModel.TransactionType> transactionTypes = this.transactionLogManager.GetTransactionTypes(TransactionDataLookup.Site, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WebServiceCall, "WecoMobile");

            // do nothing if no transactions are configured for this action.
            if (transactionTypes.Count == 0)
            {
                throw new NotImplementedException("Transaction for Site data retrieval by WECO mobile web service is not configured.");
            }

            this.response = new ListenerModel.Site();
            if (!string.IsNullOrWhiteSpace(request.ServiceType) && !string.IsNullOrWhiteSpace(request.EquipmentType) && !string.IsNullOrWhiteSpace(request.EquipmentNumber))
            {
                ListenerModel.EquipmentType equipmentType = this.deviceManager.GetEquipmentTypeByInternalCode(request.ServiceType, request.EquipmentType);
                if (equipmentType == null)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("DeviceTypeUnknown", CultureInfo.CurrentCulture), request.EquipmentType, request.ServiceType);
                    Log.Error(message);
                    throw new ArgumentException(message);
                }

                switch (equipmentType.InternalCode)
                {
                    case "EM":
                        WnpModel.Meter meter = this.wnpSystem.GetEquipment<WnpModel.Meter>(request.EquipmentNumber, this.ownerId);
                        if (meter == null)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("MeterNotFound", CultureInfo.CurrentCulture), request.EquipmentNumber);
                            Log.Error(message);
                            throw new ArgumentException(message);
                        }

                        this.FillSiteInfo(meter.Site);
                        break;    
                    case "CT":
                        WnpModel.CurrentTransformer currentTransformer = this.wnpSystem.GetEquipment<WnpModel.CurrentTransformer>(request.EquipmentNumber, this.ownerId);
                        if (currentTransformer == null)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("CTNotFound", CultureInfo.CurrentCulture), request.EquipmentNumber);
                            Log.Error(message);
                            throw new ArgumentException(message);
                        }

                        this.FillSiteInfo(currentTransformer.Site);
                        break;
                    case "PT":
                        WnpModel.PotentialTransformer potentialTransformer = this.wnpSystem.GetEquipment<WnpModel.PotentialTransformer>(request.EquipmentNumber, this.ownerId);
                        if (potentialTransformer == null)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("PTNotFound", CultureInfo.CurrentCulture), request.EquipmentNumber);
                            Log.Error(message);
                            throw new ArgumentException(message);
                        }

                        this.FillSiteInfo(potentialTransformer.Site);
                        break;
                    default:
                        string message1 = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("DeviceTypeNotSupported", CultureInfo.CurrentCulture), equipmentType.Description, equipmentType.ServiceType.Description);
                        Log.Error(message1);
                        throw new ArgumentException(message1);
                }
            }

            return this.response;
        }

        /// <summary>
        /// Adds the meter test results.
        /// </summary>
        /// <param name="request">The request.</param>
        public void AddMeterTestResults(MeterTestResultsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (MeterTestResultRequest test in request.MeterTestResults)
            {
                foreach (ListenerModel.MeterTestStep testStep in test.MeterTest.MeterTestSteps)
                {
                    WnpModel.MeterTestResult meterTestResult = new WnpModel.MeterTestResult()
                    {
                        AsFound = testStep.AsFound,
                        AsLeft = testStep.AsLeft,
                        DesiredAccuracy = testStep.DesiredAccuracy,
                        Element = testStep.Element,
                        EquipmentNumber = test.EquipmentNumber,
                        Frequency = testStep.Frequency,
                        KH = testStep.KH,
                        Location = test.MeterTest.Location,
                        LowerLimit = testStep.LowerLimit,
                        Optics = testStep.Optics,
                        Owner = new WnpModel.Owner(this.ownerId),
                        PhaseAngle = testStep.PhaseAngle,
                        PrimaryTestReason = test.MeterTest.PrimaryTestReason,
                        ReversePower = testStep.ReversePower,
                        ServiceType = testStep.ServiceType,
                        StandardMode = testStep.StandardMode,
                        StationId = test.MeterTest.BoardNumber,
                        StepNumber = testStep.TestStep,
                        TestAmps = testStep.TestAmps,
                        TestDate = test.MeterTest.TestDate,
                        TestDateStop = test.MeterTest.TestDateStop,
                        TesterId = test.MeterTest.TesterId,
                        TestRevisions = testStep.TestRevisions,
                        TestStandard = testStep.TestStandard,
                        TestType = testStep.TestType,
                        TestVolts = testStep.TestVolts,
                        UpperLimit = testStep.UpperLimit,
                        WecoSerialNumber = testStep.WecoSerialNumber
                    };

                    this.wnpSystem.AddEquipmentTestResult<WnpModel.MeterTestResult>(meterTestResult);
                }
            }
        }

        /// <summary>
        /// Adds the related files to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        public void AddRelatedFiles(AddRelatedFilesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (AddRelatedFileRequest relatedFile in request.RelatedFiles)
            {
                if (!string.IsNullOrWhiteSpace(relatedFile.ServiceType) && !string.IsNullOrWhiteSpace(relatedFile.EquipmentType) && !string.IsNullOrWhiteSpace(relatedFile.EquipmentNumber))
                {
                    WnpModel.Multimedia multimedia = new WnpModel.Multimedia()
                    {
                        CreateDate = relatedFile.RelatedFile.CreatedDate,
                        CreateUser = relatedFile.RelatedFile.CreatedBy,
                        EquipmentNumber = relatedFile.EquipmentNumber,
                        EquipmentType = relatedFile.EquipmentType,
                        FileContent = relatedFile.RelatedFile.FileContent,
                        FileDescription = relatedFile.RelatedFile.Description,
                        FileType = relatedFile.RelatedFile.FileType,
                        Owner = new WnpModel.Owner(this.ownerId)
                    };

                    this.wnpSystem.AddEquipmentMultimedia(multimedia);
                }
                else if (relatedFile.SiteId.HasValue)
                {
                    WnpModel.SiteMultimedia multimedia = new WnpModel.SiteMultimedia()
                    {
                        CreateDate = relatedFile.RelatedFile.CreatedDate,
                        CreateUser = relatedFile.RelatedFile.CreatedBy,
                        FileContent = relatedFile.RelatedFile.FileContent,
                        FileDescription = relatedFile.RelatedFile.Description,
                        FileType = relatedFile.RelatedFile.FileType,
                        Owner = new WnpModel.Owner(this.ownerId),
                        Site = new WnpModel.Site(relatedFile.SiteId.Value)
                    };

                    this.wnpSystem.AddSiteMultimedia(multimedia);
                }
            }
        }

        /// <summary>
        /// Removes the related files from site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        public void RemoveRelatedFiles(RemoveRelatedFilesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (RemoveRelatedFileRequest relatedFile in request.RelatedFiles)
            {
                if (!string.IsNullOrWhiteSpace(relatedFile.ServiceType) && !string.IsNullOrWhiteSpace(relatedFile.EquipmentType) && !string.IsNullOrWhiteSpace(relatedFile.EquipmentNumber))
                {
                    WnpModel.Multimedia multimedia = new WnpModel.Multimedia()
                    {
                        EquipmentNumber = relatedFile.EquipmentNumber,
                        EquipmentType = relatedFile.EquipmentType,
                        FileIndex = relatedFile.FileIndex,
                        Owner = new WnpModel.Owner(this.ownerId)
                    };

                    this.wnpSystem.RemoveEquipmentMultimedia(multimedia);
                }
                else if (relatedFile.SiteId.HasValue)
                {
                    WnpModel.SiteMultimedia multimedia = new WnpModel.SiteMultimedia()
                    {
                        FileIndex = relatedFile.FileIndex,
                        Owner = new WnpModel.Owner(this.ownerId),
                        Site = new WnpModel.Site(relatedFile.SiteId.Value)
                    };

                    this.wnpSystem.RemoveSiteMultimedia(multimedia);
                }
            }
        }

        /// <summary>
        /// Updates specified circuit location data.
        /// </summary>
        /// <param name="request">The request.</param>
        public void UpdateCircuitLocation(UpdateCircuitLocationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            WnpModel.Site site = new WnpModel.Site(request.SiteId);
            WnpModel.Circuit circuit = this.wnpSystem.GetCircuit(site, request.CircuitIndex);

            if (circuit == null)
            {
                string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("CircuitNotFound", CultureInfo.CurrentCulture), request.CircuitIndex, request.SiteId);
                Log.Error(message);
                throw new ArgumentException(message);
            }

            circuit.Longitude = request.Longitude;
            circuit.Latitude = request.Latitude;

            this.wnpSystem.AddOrReplaceCircuit(circuit);
        }

        /// <summary>
        /// Adds the comments to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        public void AddComments(AddCommentsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (AddCommentRequest comment in request.Comments)
            {
                if (!string.IsNullOrWhiteSpace(comment.ServiceType) && !string.IsNullOrWhiteSpace(comment.EquipmentType) && !string.IsNullOrWhiteSpace(comment.EquipmentNumber))
                {
                    WnpModel.Comment wnpComment = new WnpModel.Comment()
                    {
                        CommentText = comment.Comment.Text,
                        CreateDate = comment.Comment.CreatedDate,
                        CreateUser = comment.Comment.CreatedBy,
                        EquipmentNumber = comment.EquipmentNumber,
                        EquipmentType = comment.EquipmentType,
                        Source = "FAS",                        
                        Owner = new WnpModel.Owner(this.ownerId)
                    };
                    if (comment.Comment.Popup)
                    {
                        wnpComment.CommentType = 'P';
                    }
                    else
                    {
                        wnpComment.CommentType = 'G';
                    }

                    this.wnpSystem.AddEquipmentComment(wnpComment);
                }
                else if (comment.SiteId.HasValue)
                {
                    WnpModel.SiteComment wnpComment = new WnpModel.SiteComment()
                    {
                        CommentText = comment.Comment.Text,
                        CreateDate = comment.Comment.CreatedDate,
                        CreateUser = comment.Comment.CreatedBy,
                        Site = new WnpModel.Site(comment.SiteId.Value),
                        Owner = new WnpModel.Owner(this.ownerId)
                    };
                    if (comment.Comment.Popup)
                    {
                        wnpComment.CommentType = 'P';
                    }
                    else
                    {
                        wnpComment.CommentType = 'G';
                    }

                    this.wnpSystem.AddSiteComment(wnpComment);
                }
            }
        }

        /// <summary>
        /// Removes the comments from site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        public void RemoveComments(RemoveCommentsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (RemoveCommentRequest comment in request.Comments)
            {
                if (!string.IsNullOrWhiteSpace(comment.ServiceType) && !string.IsNullOrWhiteSpace(comment.EquipmentType) && !string.IsNullOrWhiteSpace(comment.EquipmentNumber))
                {
                    WnpModel.Comment wnpComment = new WnpModel.Comment()
                    {
                        EquipmentNumber = comment.EquipmentNumber,
                        EquipmentType = comment.EquipmentType,
                        CommentIndex = comment.CommentIndex,
                        Owner = new WnpModel.Owner(this.ownerId)
                    };

                    this.wnpSystem.RemoveEquipmentComment(wnpComment);
                }
                else if (comment.SiteId.HasValue)
                {
                    WnpModel.SiteComment wnpComment = new WnpModel.SiteComment()
                    {
                        CommentIndex = comment.CommentIndex,
                        Owner = new WnpModel.Owner(this.ownerId),
                        Site = new WnpModel.Site(comment.SiteId.Value)
                    };

                    this.wnpSystem.RemoveSiteComment(wnpComment);
                }
            }
        }

        /// <summary>
        /// Updates the comments for site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        public void UpdateComments(UpdateCommentsRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "Can not process request if it is not specified");
            }

            foreach (UpdateCommentRequest comment in request.Comments)
            {
                if (!string.IsNullOrWhiteSpace(comment.ServiceType) && !string.IsNullOrWhiteSpace(comment.EquipmentType) && !string.IsNullOrWhiteSpace(comment.EquipmentNumber))
                {
                    WnpModel.Comment wnpComment = this.wnpSystem.GetEquipmentComment(comment.EquipmentNumber, this.ownerId, comment.EquipmentType, comment.CommentIndex);

                    if (comment.Popup)
                    {
                        wnpComment.CommentType = 'P';
                    }
                    else
                    {
                        wnpComment.CommentType = 'G';
                    } 
                    
                    this.wnpSystem.UpdateEquipmentComment(wnpComment);
                }
                else if (comment.SiteId.HasValue)
                {
                    WnpModel.SiteComment wnpComment = this.wnpSystem.GetSiteComment(new WnpModel.Site(comment.SiteId.Value), comment.CommentIndex);

                    if (comment.Popup)
                    {
                        wnpComment.CommentType = 'P';
                    }
                    else
                    {
                        wnpComment.CommentType = 'G';
                    }

                    this.wnpSystem.UpdateSiteComment(wnpComment);
                }
            }
        }

        /// <summary>
        /// Fills the site information in response message.
        /// </summary>
        /// <param name="site">The site.</param>
        private void FillSiteInfo(WnpModel.Site site)
        {
            ListenerModel.Address address = new ListenerModel.Address()
            {
                Address1 = site.Address,
                Address2 = site.Address2,
                City = site.City,
                Country = site.Country,
                State = site.State,
                Zip = site.ZipCode
            };
            ListenerModel.Client account = new ListenerModel.Client()
            {
                AccountName = site.AccountName,
                AccountNumber = site.AccountNumber,
            };

            this.response.Id = site.Id;
            this.response.Description = site.Description;
            this.response.PremiseNumber = site.PremiseNumber;
            this.response.Client = account;
            this.response.Address = address;

            this.FillSiteCircuits(site);
            this.FillSiteComments(site);
            this.FillSiteRelatedFiles(site);
        }

        /// <summary>
        /// Fills the site circuits.
        /// </summary>
        /// <param name="site">The site.</param>
        private void FillSiteCircuits(WnpModel.Site site)
        {
            IList<WnpModel.Circuit> siteCircuits = this.wnpSystem.GetSiteCircuits(site);

            foreach (WnpModel.Circuit currentCircuit in siteCircuits)
            {
                ListenerModel.Circuit circuit = new ListenerModel.Circuit()
                {
                    Index = currentCircuit.CircuitIndex,
                    Description = currentCircuit.Description,
                    Longitude = currentCircuit.Longitude,
                    Latitude = currentCircuit.Latitude
                };

                this.FillCircuitDevices(currentCircuit, circuit);

                this.response.Circuits.Add(circuit);
            }
        }

        /// <summary>
        /// Fills the site comments.
        /// </summary>
        /// <param name="site">The site.</param>
        private void FillSiteComments(WnpModel.Site site)
        {
            IList<WnpModel.SiteComment> siteComments = this.wnpSystem.GetSiteComments(site);
            
            foreach (WnpModel.SiteComment siteComment in siteComments)
            {
                ListenerModel.Comment comment = new ListenerModel.Comment()
                {
                    Text = siteComment.CommentText,
                    CreatedBy = siteComment.CreateUser,
                    CreatedDate = siteComment.CreateDate,
                    Index = siteComment.CommentIndex,
                };

                switch (siteComment.CommentType)
                {
                    case 'G':
                        comment.Popup = false;
                        break;
                    case 'P':
                        comment.Popup = true;
                        break;
                    default:
                        string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("CommentTypeUnrecognized", CultureInfo.CurrentCulture), siteComment.CommentType);
                        Log.Error(message);
                        throw new ArgumentException(message);
                }

                this.response.Comments.Add(comment);
            }
        }
        
        /// <summary>
        /// Fills the site related files.
        /// </summary>
        /// <param name="site">The site.</param>
        private void FillSiteRelatedFiles(WnpModel.Site site)
        {
            IList<WnpModel.SiteMultimedia> siteMultimedia = this.wnpSystem.GetSiteMultimedia(site);

            foreach (WnpModel.SiteMultimedia multimedia in siteMultimedia)
            {
                ListenerModel.RelatedFile relatedFile = new ListenerModel.RelatedFile()
                {
                    CreatedBy = multimedia.CreateUser,
                    CreatedDate = multimedia.CreateDate,
                    Description = multimedia.FileDescription,
                    FileContent = multimedia.FileContent,
                    FileType = multimedia.FileType,
                    FileIndex = multimedia.FileIndex
                };

                this.response.RelatedFiles.Add(relatedFile);
            }
        }

        /// <summary>
        /// Fills the circuit devices.
        /// </summary>
        /// <param name="wnpCircuit">The WNP circuit.</param>
        /// <param name="listenerCircuit">The listener circuit.</param>
        private void FillCircuitDevices(WnpModel.Circuit wnpCircuit, ListenerModel.Circuit listenerCircuit)
        {
            this.FillMeters(wnpCircuit, listenerCircuit);
            this.FillCurrentTransformers(wnpCircuit, listenerCircuit);
            this.FillPotentialTransformers(wnpCircuit, listenerCircuit);
        }

        /// <summary>
        /// Fills the meters.
        /// </summary>
        /// <param name="wnpCircuit">The WNP circuit.</param>
        /// <param name="listenerCircuit">The listener circuit.</param>
        private void FillMeters(WnpModel.Circuit wnpCircuit, ListenerModel.Circuit listenerCircuit)
        {
            IList<WnpModel.Meter> meters = this.wnpSystem.GetEquipmentByCircuit<WnpModel.Meter>(wnpCircuit);

            foreach (WnpModel.Meter meter in meters)
            {
                ListenerModel.Device device = new ListenerModel.Device()
                {
                    Base = meter.Base,
                    Company = this.deviceManager.GetCompanyByInternalCode(this.ownerId.ToString(CultureInfo.InvariantCulture)),
                    EquipmentNumber = meter.EquipmentNumber,
                    EquipmentType = this.deviceManager.GetEquipmentTypeByInternalCode("E", "EM"),
                    Form = meter.Form,
                    KH = meter.KH
                };

                if (meter.TestAmps.HasValue)
                {
                    device.TestAmps = meter.TestAmps.Value;
                }

                if (meter.TestVolts.HasValue)
                {
                    device.TestVolts = meter.TestVolts.Value;
                }

                listenerCircuit.Devices.Add(device);
                this.FillDeviceComments(meter, "EM", device);
                this.FillDeviceRelatedFiles(meter, "EM", device);
                this.FillMeterTests(meter, device);
            }
        }

        /// <summary>
        /// Fills the current transformers.
        /// </summary>
        /// <param name="wnpCircuit">The WNP circuit.</param>
        /// <param name="listenerCircuit">The listener circuit.</param>
        private void FillCurrentTransformers(WnpModel.Circuit wnpCircuit, ListenerModel.Circuit listenerCircuit)
        {
            IList<WnpModel.CurrentTransformer> currentTransformers = this.wnpSystem.GetEquipmentByCircuit<WnpModel.CurrentTransformer>(wnpCircuit);

            foreach (WnpModel.CurrentTransformer currentTransformer in currentTransformers)
            {
                ListenerModel.Device device = new ListenerModel.Device()
                {
                    Company = this.deviceManager.GetCompanyByInternalCode(this.ownerId.ToString(CultureInfo.InvariantCulture)),
                    EquipmentNumber = currentTransformer.EquipmentNumber,
                    EquipmentType = this.deviceManager.GetEquipmentTypeByInternalCode("E", "CT")
                };

                listenerCircuit.Devices.Add(device);
                this.FillDeviceComments(currentTransformer, "CT", device);
                this.FillDeviceRelatedFiles(currentTransformer, "CT", device);
            }
        }

        /// <summary>
        /// Fills the potential transformers.
        /// </summary>
        /// <param name="wnpCircuit">The WNP circuit.</param>
        /// <param name="listenerCircuit">The listener circuit.</param>
        private void FillPotentialTransformers(WnpModel.Circuit wnpCircuit, ListenerModel.Circuit listenerCircuit)
        {
            IList<WnpModel.PotentialTransformer> potentialTransformers = this.wnpSystem.GetEquipmentByCircuit<WnpModel.PotentialTransformer>(wnpCircuit);

            foreach (WnpModel.PotentialTransformer potentialTransformer in potentialTransformers)
            {
                ListenerModel.Device device = new ListenerModel.Device()
                {
                    Company = this.deviceManager.GetCompanyByInternalCode(this.ownerId.ToString(CultureInfo.InvariantCulture)),
                    EquipmentNumber = potentialTransformer.EquipmentNumber,
                    EquipmentType = this.deviceManager.GetEquipmentTypeByInternalCode("E", "PT")
                };

                listenerCircuit.Devices.Add(device);
                this.FillDeviceComments(potentialTransformer, "PT", device);
                this.FillDeviceRelatedFiles(potentialTransformer, "PT", device);
            }
        }

        /// <summary>
        /// Fills the device comments.
        /// </summary>
        /// <param name="wnpEquipment">The WNP equipment.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="listenerDevice">The listener device.</param>
        /// <exception cref="System.ArgumentException">Throws exception if comment type is unrecognized.</exception>
        private void FillDeviceComments(WnpModel.IEquipment wnpEquipment, string equipmentType, ListenerModel.Device listenerDevice)
        {
            IList<WnpModel.Comment> comments = this.wnpSystem.GetEquipmentComments(wnpEquipment.EquipmentNumber, this.ownerId, equipmentType);

            foreach (WnpModel.Comment comment in comments)
            {
                ListenerModel.Comment deviceComment = new ListenerModel.Comment()
                {
                    Text = comment.CommentText,
                    CreatedBy = comment.CreateUser,
                    CreatedDate = comment.CreateDate,
                    Index = comment.CommentIndex
                };

                switch (comment.CommentType)
                {
                    case 'G':
                        deviceComment.Popup = false;
                        break;
                    case 'P':
                        deviceComment.Popup = true;
                        break;
                    default:
                        string message = string.Format(CultureInfo.InvariantCulture, this.stringManager.GetString("CommentTypeUnrecognized", CultureInfo.CurrentCulture), comment.CommentType);
                        Log.Error(message);
                        throw new ArgumentException(message);
                }

                listenerDevice.Comments.Add(deviceComment);
            }
        }

        /// <summary>
        /// Fills the device related files.
        /// </summary>
        /// <param name="wnpEquipment">The WNP equipment.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="listenerDevice">The listener device.</param>
        private void FillDeviceRelatedFiles(WnpModel.IEquipment wnpEquipment, string equipmentType, ListenerModel.Device listenerDevice)
        {
            IList<WnpModel.Multimedia> equipmentMultimedia = this.wnpSystem.GetEquipmentMultimedia(wnpEquipment.EquipmentNumber, this.ownerId, equipmentType);

            foreach (WnpModel.Multimedia multimedia in equipmentMultimedia)
            {
                ListenerModel.RelatedFile relatedFile = new ListenerModel.RelatedFile()
                {
                    CreatedBy = multimedia.CreateUser,
                    CreatedDate = multimedia.CreateDate,
                    Description = multimedia.FileDescription,
                    FileContent = multimedia.FileContent,
                    FileType = multimedia.FileType,
                    FileIndex = multimedia.FileIndex
                };

                listenerDevice.RelatedFiles.Add(relatedFile);
            }
        }

        /// <summary>
        /// Fills the meter tests.
        /// </summary>
        /// <param name="wnpEquipment">The WNP equipment.</param>
        /// <param name="listenerDevice">The listener device.</param>
        private void FillMeterTests(WnpModel.IEquipment wnpEquipment, ListenerModel.Device listenerDevice)
        {
            IList<WnpModel.MeterTestResult> tests = this.wnpSystem.GetEquipmentAllTestResult<WnpModel.MeterTestResult>(wnpEquipment.EquipmentNumber, this.ownerId);

            if (tests.Count > 0)
            {
                WnpModel.MeterTestResult anyTestResult = tests.First<WnpModel.MeterTestResult>();

                ListenerModel.DeviceTest deviceTest = new ListenerModel.DeviceTest()
                {
                    Location = anyTestResult.Location,
                    PrimaryTestReason = anyTestResult.PrimaryTestReason,
                    TestDate = anyTestResult.TestDate,
                    TestDateStop = anyTestResult.TestDateStop,
                    TesterId = anyTestResult.TesterId
                };
                listenerDevice.Tests.Add(deviceTest);

                foreach (WnpModel.MeterTestResult test in tests)
                {
                    ListenerModel.MeterTestStep testStep = new ListenerModel.MeterTestStep()
                    {
                        AsFound = test.AsFound,
                        AsLeft = test.AsLeft,
                        DesiredAccuracy = test.DesiredAccuracy,
                        Element = test.Element,
                        Frequency = test.Frequency,
                        KH = test.KH,
                        LowerLimit = test.LowerLimit,
                        Optics = test.Optics,
                        PhaseAngle = test.PhaseAngle,
                        ReversePower = test.ReversePower,
                        ServiceType = test.ServiceType,
                        StandardMode = test.StandardMode,
                        StationId = test.StationId,
                        TestAmps = test.TestAmps,
                        TestRevisions = test.TestRevisions,
                        TestStandard = test.TestStandard,
                        TestStep = test.StepNumber,
                        TestType = test.TestType,
                        TestVolts = test.TestVolts,
                        UpperLimit = test.UpperLimit,
                        WecoSerialNumber = test.WecoSerialNumber
                    };

                    deviceTest.MeterTestSteps.Add(testStep);
                }
            }
        }
    }
}
