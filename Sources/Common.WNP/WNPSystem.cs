//-----------------------------------------------------------------------
// <copyright file="WNPSystem.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.WNP.Model;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    /// <summary>
    /// Manages all business objects related to Watt Net Plus application.
    /// </summary>
    public class WNPSystem
    {
        /// <summary>
        /// The persistence manager
        /// </summary>
        private IPersistenceManager persistenceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPSystem" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <exception cref="System.ArgumentNullException">persistenceManager;Can't initialize WNPSystem without persistenceManager.</exception>
        public WNPSystem(IPersistenceManager persistenceManager)
        {
            if (persistenceManager == null)
            {
                throw new ArgumentNullException("persistenceManager", "Can't initialize WNPSystem without persistenceManager.");
            }

            this.persistenceManager = persistenceManager;
        }

        /// <summary>
        /// Adds or replaces the equipment.
        /// </summary>
        /// <typeparam name="T">Equipment type</typeparam>
        /// <param name="equipment">The equipment.</param>
        /// <returns>
        /// Added or updated equipment identifier.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">equipment;Can not add equipment if it is not specified</exception>
        public int AddOrReplaceEquipment<T>(T equipment) where T : IEquipment
        {
            if (equipment == null)
            {
                throw new ArgumentNullException("equipment", "Can not add equipment if it is not specified");
            }

            IEquipment retrievedEquipment = this.GetEquipment<T>(equipment.EquipmentNumber, equipment.Owner.Id);

            if (retrievedEquipment != null)
            {
                equipment.Id = retrievedEquipment.Id;
                equipment.CreateDate = retrievedEquipment.CreateDate;
            }

            if (equipment.CreateDate == default(DateTime))
            {
                equipment.CreateDate = DateTime.Now;
            }

            equipment.ModifiedDate = DateTime.Now;

            this.persistenceManager.Save((T)equipment);

            return equipment.Id;
        }

        /// <summary>
        /// Gets the equipment.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>
        /// The equipment.
        /// </returns>
        public T GetEquipment<T>(string equipmentNumber, int ownerId) where T : IEquipment
        {
            return this.GetEquipment<T>(equipmentNumber, ownerId, true);
        }

        /// <summary>
        /// Gets the equipment.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>
        /// The equipment.
        /// </returns>
        public T GetEquipment<T>(string equipmentNumber, int ownerId, bool sanitized) where T : IEquipment
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));

            T equipment = this.persistenceManager.RetrieveFirstEqual<T>(criteria);

            if (sanitized)
            {
                equipment = Utilities.Sanitize(equipment);
            }

            return equipment;
        }

        /// <summary>
        /// Gets the equipment.
        /// </summary>
        /// <typeparam name="T">The type of equipment.</typeparam>
        /// <returns>Returns all equipment.</returns>
        public IList<T> GetEquipmentIdOnly<T>()
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            ProjectionList projectionList = Projections.ProjectionList();
            projectionList.Add(Projections.Property("EquipmentNumber"), "EquipmentNumber");
            projectionList.Add(Projections.Property("Owner"), "Owner");

            criteria.SetProjection(projectionList).SetResultTransformer(Transformers.AliasToBean<T>());

            return this.persistenceManager.RetrieveAllEqual<T>(criteria);
        }

        /// <summary>
        /// Gets the equipment by batch.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="newBatch">The new batch.</param>
        /// <returns>The list of equipment that belongs to batch.</returns>
        public IList<T> GetEquipmentByBatch<T>(NewBatch newBatch) where T : IEquipment
        {
            return this.GetEquipmentByBatch<T>(newBatch, false);
        }

        /// <summary>
        /// Gets the equipment by batch.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="newBatch">The new batch.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>The list of equipment that belongs to batch.</returns>
        public IList<T> GetEquipmentByBatch<T>(NewBatch newBatch, bool sanitized) where T : IEquipment
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("NewBatch", newBatch));

            IList<T> equipmentList = this.persistenceManager.RetrieveAllEqual<T>(criteria);

            if (sanitized)
            {
                IList<T> sanitizedEquipmentList = new List<T>();

                foreach (T equipment in equipmentList)
                {
                    sanitizedEquipmentList.Add(Utilities.Sanitize(equipment));
                }

                equipmentList = sanitizedEquipmentList;
            }

            return equipmentList;
        }

        /// <summary>
        /// Gets the equipment by circuit.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="circuit">The circuit.</param>
        /// <returns>The list of equipment that belongs to circuit.</returns>
        public IList<T> GetEquipmentByCircuit<T>(Circuit circuit) where T : IEquipment
        {
            return this.GetEquipmentByCircuit<T>(circuit, false);
        }

        /// <summary>
        /// Gets the equipment by circuit.
        /// </summary>
        /// <typeparam name="T">The equipment type.</typeparam>
        /// <param name="circuit">The circuit.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>
        /// The list of equipment that belongs to circuit.
        /// </returns>
        public IList<T> GetEquipmentByCircuit<T>(Circuit circuit, bool sanitized) where T : IEquipment
        {
            if (circuit == null)
            {
                throw new ArgumentNullException("circuit", "Can not find equipment if circuit is not specified.");
            }

            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("Site", circuit.Site));
            criteria.Add(Restrictions.Eq("Circuit", circuit.CircuitIndex));

            IList<T> equipmentList = this.persistenceManager.RetrieveAllEqual<T>(criteria);

            if (sanitized)
            {
                IList<T> sanitizedEquipmentList = new List<T>();

                foreach (T equipment in equipmentList)
                {
                    sanitizedEquipmentList.Add(Utilities.Sanitize(equipment));
                }

                equipmentList = sanitizedEquipmentList;
            }

            return equipmentList;
        }

        /// <summary>
        /// Gets equipment test result.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>
        /// The equipment test result
        /// </returns>
        public IList<T> GetEquipmentTestResult<T>(string equipmentNumber, int ownerId, DateTime testDate) where T : IEquipmentTestResult
        {
             return this.GetEquipmentTestResult<T>(equipmentNumber, ownerId, testDate, true);
        }

        /// <summary>
        /// Gets equipment test result.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>
        /// The equipment test result
        /// </returns>
        public IList<T> GetEquipmentTestResult<T>(string equipmentNumber, int ownerId, DateTime testDate, bool sanitized) where T : IEquipmentTestResult
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("TestDate", testDate));

            IList<T> testResults = this.persistenceManager.RetrieveAllEqual<T>(criteria);
            if (sanitized)
            {
                IList<T> sanitizedTestResults = new List<T>();
                foreach (T testResult in testResults)
                {
                    sanitizedTestResults.Add(Utilities.Sanitize(testResult));
                }

                testResults = sanitizedTestResults;
            } 
            
            return testResults;
        }

        /// <summary>
        /// Gets equipment test result.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <param name="step">The test result step.</param>
        /// <returns>
        /// The equipment test result step
        /// </returns>
        public T GetEquipmentTestResultStep<T>(string equipmentNumber, int ownerId, DateTime testDate, int step) where T : IEquipmentTestResult
        {
            return this.GetEquipmentTestResultStep<T>(equipmentNumber, ownerId, testDate, step, false);
        }

        /// <summary>
        /// Gets equipment test result step.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <param name="step">The test result step.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>
        /// The equipment test result step
        /// </returns>
        public T GetEquipmentTestResultStep<T>(string equipmentNumber, int ownerId, DateTime testDate, int step, bool sanitized) where T : IEquipmentTestResult
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("TestDate", testDate));
            criteria.Add(Restrictions.Eq("StepNumber", step));

            T testResultStep = this.persistenceManager.RetrieveFirstEqual<T>(criteria);
            if (sanitized)
            {
                testResultStep = Utilities.Sanitize(testResultStep);
            }

            return testResultStep;
        }

        /// <summary>
        /// Adds the equipment test result.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="testResult">The test result.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not add test result if it is not specified</exception>
        public void AddEquipmentTestResult<T>(T testResult) where T : IEquipmentTestResult
        {
            if (testResult == null)
            {
                throw new ArgumentNullException("testResult", "Can not add test result if it is not specified");
            }

            T retrievedTestResult = this.GetEquipmentTestResultStep<T>(testResult.EquipmentNumber, testResult.Owner.Id, testResult.TestDate, testResult.StepNumber);

            if (retrievedTestResult == null)
            {
                this.persistenceManager.Save((T)testResult);
            }
        }

        /// <summary>
        /// Gets the equipment comments.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <returns>The equipment comments</returns>
        public IList<Comment> GetEquipmentComments(string equipmentNumber, int ownerId, string equipmentType)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("EquipmentType", equipmentType));

            return this.persistenceManager.RetrieveAllEqual<Comment>(criteria);
        }

        /// <summary>
        /// Gets the specific equipment comment.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="commentIndex">Index of the comment.</param>
        /// <returns>
        /// The equipment comments
        /// </returns>
        public Comment GetEquipmentComment(string equipmentNumber, int ownerId, string equipmentType, int commentIndex)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("EquipmentType", equipmentType));
            criteria.Add(Restrictions.Eq("CommentIndex", commentIndex));

            return this.persistenceManager.RetrieveFirstEqual<Comment>(criteria);
        }

        /// <summary>
        /// Gets the equipment multimedia.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <returns>The equipment multimedia</returns>
        public IList<Multimedia> GetEquipmentMultimedia(string equipmentNumber, int ownerId, string equipmentType)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Multimedia>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("EquipmentType", equipmentType));

            return this.persistenceManager.RetrieveAllEqual<Multimedia>(criteria);
        }

        /// <summary>
        /// Adds the multimedia to equipment.
        /// </summary>
        /// <param name="multimedia">The multimedia.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not add multimedia if it is not specified</exception>
        public void AddEquipmentMultimedia(Multimedia multimedia)
        {
            if (multimedia == null)
            {
                throw new ArgumentNullException("multimedia", "Can not add multimedia if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<Multimedia>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", multimedia.EquipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", multimedia.Owner));
            criteria.Add(Restrictions.Eq("EquipmentType", multimedia.EquipmentType));
            criteria.Add(Restrictions.Eq("CreateDate", multimedia.CreateDate));
            criteria.Add(Restrictions.Eq("FileDescription", multimedia.FileDescription));

            Multimedia currentMultimedia = this.persistenceManager.RetrieveFirstEqual<Multimedia>(criteria);

            if (currentMultimedia == null)
            {
                criteria = DetachedCriteria.For<Multimedia>();
                criteria.SetProjection(Projections.Max("FileIndex"));

                int fileIndex = this.persistenceManager.RetrieveUnique<int>(criteria);
                multimedia.FileIndex = fileIndex + 1;

                this.persistenceManager.Save(multimedia);
            }
        }

        /// <summary>
        /// Remove the multimedia from the equipment.
        /// </summary>
        /// <param name="multimedia">The multimedia.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not remove multimedia if it is not specified</exception>
        public void RemoveEquipmentMultimedia(Multimedia multimedia)
        {
            if (multimedia == null)
            {
                throw new ArgumentNullException("multimedia", "Can not remove multimedia if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<Multimedia>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", multimedia.EquipmentNumber));
            criteria.Add(Restrictions.Eq("EquipmentType", multimedia.EquipmentType));
            criteria.Add(Restrictions.Eq("Owner", multimedia.Owner));
            criteria.Add(Restrictions.Eq("FileIndex", multimedia.FileIndex));

            Multimedia currentMultimedia = this.persistenceManager.RetrieveFirstEqual<Multimedia>(criteria);

            if (currentMultimedia != null)
            {
                this.persistenceManager.Delete<Multimedia>(currentMultimedia.Id);
            }
        }

        /// <summary>
        /// Adds the comment to equipment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not add comment if it is not specified</exception>
        public void AddEquipmentComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not add comment if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", comment.EquipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", comment.Owner));
            criteria.Add(Restrictions.Eq("EquipmentType", comment.EquipmentType));
            criteria.Add(Restrictions.Eq("CreateDate", comment.CreateDate));
            criteria.Add(Restrictions.Eq("CommentText", comment.CommentText));

            Comment currentComment = this.persistenceManager.RetrieveFirstEqual<Comment>(criteria);

            if (currentComment == null)
            {
                criteria = DetachedCriteria.For<Comment>();
                criteria.Add(Restrictions.Eq("EquipmentNumber", comment.EquipmentNumber));
                criteria.Add(Restrictions.Eq("Owner", comment.Owner));
                criteria.Add(Restrictions.Eq("EquipmentType", comment.EquipmentType));
                criteria.SetProjection(Projections.Max("CommentIndex"));

                int commentIndex = this.persistenceManager.RetrieveUnique<int>(criteria);
                comment.CommentIndex = commentIndex + 1;

                this.persistenceManager.Save(comment);
            }
        }

        /// <summary>
        /// Remove the comment from the equipment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not remove comment if it is not specified</exception>
        public void RemoveEquipmentComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not remove comment if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", comment.EquipmentNumber));
            criteria.Add(Restrictions.Eq("EquipmentType", comment.EquipmentType));
            criteria.Add(Restrictions.Eq("Owner", comment.Owner));
            criteria.Add(Restrictions.Eq("CommentIndex", comment.CommentIndex));

            Comment currentComment = this.persistenceManager.RetrieveFirstEqual<Comment>(criteria);

            if (currentComment != null)
            {
                this.persistenceManager.Delete<Comment>(currentComment.Id);
            }
        }

        /// <summary>
        /// Update the comment from the equipment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not update comment if it is not specified</exception>
        public void UpdateEquipmentComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not update comment if it is not specified");
            }

            this.persistenceManager.Save<Comment>(comment);
        }

        /// <summary>
        /// Gets all test results for specified equipment.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>
        /// The test results for specified equipment.
        /// </returns>
        public IList<T> GetEquipmentAllTestResult<T>(string equipmentNumber, int ownerId) where T : IEquipmentTestResult
        {
            return this.GetEquipmentAllTestResult<T>(equipmentNumber, ownerId, false);
        }

        /// <summary>
        /// Gets all test results for specified equipment.
        /// </summary>
        /// <typeparam name="T">The equipment test result type.</typeparam>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="sanitized">If set to <c>true</c> trim spaces from strings and set to null if string is empty.</param>
        /// <returns>
        /// The test results for specified equipment.
        /// </returns>
        public IList<T> GetEquipmentAllTestResult<T>(string equipmentNumber, int ownerId, bool sanitized) where T : IEquipmentTestResult
        {
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));

            IList<T> testResults = this.persistenceManager.RetrieveAllEqual<T>(criteria);
            if (sanitized)
            {
                IList<T> sanitizedTestResults = new List<T>();
                foreach (T testResult in testResults)
                {
                    sanitizedTestResults.Add(Utilities.Sanitize(testResult));
                }

                testResults = sanitizedTestResults;
            }

            return testResults;
        }

        /// <summary>
        /// Gets the barcode.
        /// </summary>
        /// <typeparam name="T">Type of barcode</typeparam>
        /// <param name="barcode">The barcode.</param>
        /// <returns>
        /// The barcode
        /// </returns>
        /// <exception cref="ArgumentNullException">barcode;Can not retrieve barcode, if retrieval criteria is not specified.</exception>
        public T GetBarcode<T>(T barcode) where T : IBarcode
        {
            if (barcode == null)
            {
                throw new ArgumentNullException("barcode", "Can not retrieve meter barcode, if retrieval criteria is not specified.");
            }

            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("BarcodeId", barcode.BarcodeId));

            return this.persistenceManager.RetrieveFirstEqual<T>(criteria);
        }

        /// <summary>
        /// Gets all barcodes.
        /// </summary>
        /// <typeparam name="T">Type of barcode</typeparam>
        /// <returns>
        /// The list of all barcodes.
        /// </returns>
        public IList<T> GetBarcodes<T>()
        {
            return this.persistenceManager.RetrieveAll<T>(SessionAction.BeginAndEnd);
        }

        /// <summary>
        /// Updates barcodes.
        /// </summary>
        /// <typeparam name="T">Type of barcode</typeparam>
        /// <param name="barcodes">The barcodes.</param>
        public void UpdateBarcodes<T>(IList<T> barcodes)
        {
            this.persistenceManager.SaveBulk<T>(barcodes);
        }

        /// <summary>
        /// Cleans all barcodes.
        /// </summary>
        /// <typeparam name="T">Type of barcode</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "No parameters are needed for this method, but I don't want to add 3 different methods for eatch barcode type")]
        public void CleanBarcodes<T>()
        {
            this.persistenceManager.DeleteAll<T>();
        }

        /// <summary>
        /// Gets the comments related to specific test.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>The list of comments.</returns>
        public IList<Comment> GetTestComment(string equipmentNumber, int ownerId, string equipmentType, DateTime testDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("EquipmentType", equipmentType));
            criteria.Add(Restrictions.Eq("CreateDate", testDate));

            return this.persistenceManager.RetrieveAllEqual<Comment>(criteria);
        }

        /// <summary>
        /// Gets the comments related to specific test and concatenates them to one string.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>The concatenated comments or null if no comments were found.</returns>
        public string GetTestCommentsConcatenated(string equipmentNumber, int ownerId, string equipmentType, DateTime testDate)
        {
            string result = null;

            DetachedCriteria criteria = DetachedCriteria.For<Comment>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("EquipmentType", equipmentType));
            criteria.Add(Restrictions.Eq("CreateDate", testDate));

            IList<Comment> comments = this.persistenceManager.RetrieveAllEqual<Comment>(criteria);
            if (comments.Count > 0)
            {
                result = string.Join(" ", comments.Select(i => i.CommentText));
            }

            return result;
        }

        /// <summary>
        /// Gets the test reading.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <param name="readLabel">The read label.</param>
        /// <returns>
        /// Reading value.
        /// </returns>
        public IList<Reading> GetTestReading(string equipmentNumber, int ownerId, DateTime testDate, string readLabel)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Reading>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("ReadDate", testDate));
            criteria.Add(Restrictions.Eq("ReadLabel", readLabel));

            return this.persistenceManager.RetrieveAllEqual<Reading>(criteria);
        }

        /// <summary>
        /// Gets the test reading.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>
        /// Reading value.
        /// </returns>
        public IList<Reading> GetTestReading(string equipmentNumber, int ownerId, DateTime testDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Reading>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("ReadDate", testDate));

            return this.persistenceManager.RetrieveAllEqual<Reading>(criteria);
        }

        /// <summary>
        /// Gets the new batch.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>The new batch.</returns>
        public NewBatch GetNewBatch(string batchNumber)
        {
            return this.persistenceManager.GetByKey<NewBatch>(batchNumber);
        }

        /// <summary>
        /// Adds the or replaces circuit.
        /// </summary>
        /// <param name="circuit">The circuit.</param>
        /// <returns>Saved or updated circuit.</returns>
        public Circuit AddOrReplaceCircuit(Circuit circuit)
        {
            this.persistenceManager.Save<Circuit>(circuit);

            return circuit;
        }

        /// <summary>
        /// Gets the circuit.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="circuitIndex">Index of the circuit.</param>
        /// <returns>The circuit</returns>
        public Circuit GetCircuit(Site site, int circuitIndex)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Circuit>();
            criteria.Add(Restrictions.Eq("Site", site));
            criteria.Add(Restrictions.Eq("CircuitIndex", circuitIndex));

            return this.persistenceManager.RetrieveFirstEqual<Circuit>(criteria);
        }

        /// <summary>
        /// Gets the circuits that belong to specified site.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns>
        /// The list of circuits that belong to the site.
        /// </returns>
        public IList<Circuit> GetSiteCircuits(Site site)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Circuit>();
            criteria.Add(Restrictions.Eq("Site", site));

            return this.persistenceManager.RetrieveAllEqual<Circuit>(criteria);
        }

        /// <summary>
        /// Gets the site comments.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns>The list of comments</returns>
        public IList<SiteComment> GetSiteComments(Site site)
        {
            DetachedCriteria criteria = DetachedCriteria.For<SiteComment>();
            criteria.Add(Restrictions.Eq("Site", site));

            return this.persistenceManager.RetrieveAllEqual<SiteComment>(criteria);
        }

        /// <summary>
        /// Gets the specific site comment.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="commentIndex">Index of the comment.</param>
        /// <returns>
        /// The site comment
        /// </returns>
        public SiteComment GetSiteComment(Site site, int commentIndex)
        {
            DetachedCriteria criteria = DetachedCriteria.For<SiteComment>();
            criteria.Add(Restrictions.Eq("Site", site));
            criteria.Add(Restrictions.Eq("CommentIndex", commentIndex));

            return this.persistenceManager.RetrieveFirstEqual<SiteComment>(criteria);
        }

        /// <summary>
        /// Gets the site multimedia.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <returns>The list of multimedia files</returns>
        public IList<SiteMultimedia> GetSiteMultimedia(Site site)
        {
            DetachedCriteria criteria = DetachedCriteria.For<SiteMultimedia>();
            criteria.Add(Restrictions.Eq("Site", site));

            return this.persistenceManager.RetrieveAllEqual<SiteMultimedia>(criteria);
        }

        /// <summary>
        /// Adds the multimedia to site.
        /// </summary>
        /// <param name="multimedia">The multimedia.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not add multimedia if it is not specified</exception>
        public void AddSiteMultimedia(SiteMultimedia multimedia)
        {
            if (multimedia == null)
            {
                throw new ArgumentNullException("multimedia", "Can not add multimedia if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<SiteMultimedia>();
            criteria.Add(Restrictions.Eq("Site", multimedia.Site));
            criteria.Add(Restrictions.Eq("Owner", multimedia.Owner));
            criteria.Add(Restrictions.Eq("CreateDate", multimedia.CreateDate));
            criteria.Add(Restrictions.Eq("FileDescription", multimedia.FileDescription));

            SiteMultimedia currentMultimedia = this.persistenceManager.RetrieveFirstEqual<SiteMultimedia>(criteria);

            if (currentMultimedia == null)
            {
                criteria = DetachedCriteria.For<SiteMultimedia>();
                criteria.SetProjection(Projections.Max("FileIndex"));

                int fileIndex = this.persistenceManager.RetrieveUnique<int>(criteria);
                multimedia.FileIndex = fileIndex + 1;

                this.persistenceManager.Save(multimedia);
            }
        }

        /// <summary>
        /// Remove the multimedia from the site.
        /// </summary>
        /// <param name="multimedia">The multimedia.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not remove multimedia if it is not specified</exception>
        public void RemoveSiteMultimedia(SiteMultimedia multimedia)
        {
            if (multimedia == null)
            {
                throw new ArgumentNullException("multimedia", "Can not remove multimedia if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<SiteMultimedia>();
            criteria.Add(Restrictions.Eq("Site", multimedia.Site));
            criteria.Add(Restrictions.Eq("Owner", multimedia.Owner));
            criteria.Add(Restrictions.Eq("FileIndex", multimedia.FileIndex));

            SiteMultimedia currentMultimedia = this.persistenceManager.RetrieveFirstEqual<SiteMultimedia>(criteria);

            if (currentMultimedia != null)
            {
                this.persistenceManager.Delete<SiteMultimedia>(currentMultimedia.Id);
            }
        }
        
        /// <summary>
        /// Adds the comment to site.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not add comment if it is not specified</exception>
        public void AddSiteComment(SiteComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not add comment if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<SiteComment>();
            criteria.Add(Restrictions.Eq("Site", comment.Site));
            criteria.Add(Restrictions.Eq("Owner", comment.Owner));
            criteria.Add(Restrictions.Eq("CreateDate", comment.CreateDate));
            criteria.Add(Restrictions.Eq("CommentText", comment.CommentText));

            SiteComment currentComment = this.persistenceManager.RetrieveFirstEqual<SiteComment>(criteria);

            if (currentComment == null)
            {
                criteria = DetachedCriteria.For<SiteComment>();
                criteria.Add(Restrictions.Eq("Site", comment.Site));
                criteria.Add(Restrictions.Eq("Owner", comment.Owner));
                criteria.SetProjection(Projections.Max("CommentIndex"));

                int commentIndex = this.persistenceManager.RetrieveUnique<int>(criteria);
                comment.CommentIndex = commentIndex + 1;

                this.persistenceManager.Save(comment);
            }
        }
        
        /// <summary>
        /// Remove the comment from the site.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not remove comment if it is not specified</exception>
        public void RemoveSiteComment(SiteComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not remove comment if it is not specified");
            }

            DetachedCriteria criteria = DetachedCriteria.For<SiteComment>();
            criteria.Add(Restrictions.Eq("Site", comment.Site));
            criteria.Add(Restrictions.Eq("Owner", comment.Owner));
            criteria.Add(Restrictions.Eq("CommentIndex", comment.CommentIndex));

            SiteComment currentComment = this.persistenceManager.RetrieveFirstEqual<SiteComment>(criteria);

            if (currentComment != null)
            {
                this.persistenceManager.Delete<SiteComment>(currentComment.Id);
            }
        }

        /// <summary>
        /// Update the comment from the site.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <exception cref="System.ArgumentNullException">testResult;Can not update comment if it is not specified</exception>
        public void UpdateSiteComment(SiteComment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment", "Can not update comment if it is not specified");
            }

            this.persistenceManager.Save<SiteComment>(comment);
        }
    }
}
