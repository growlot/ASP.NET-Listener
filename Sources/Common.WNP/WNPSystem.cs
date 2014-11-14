//-----------------------------------------------------------------------
// <copyright file="WNPSystem.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.WNP.Model;
    using NHibernate.Criterion;

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
            }

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
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));

            return this.persistenceManager.RetrieveFirstEqual<T>(criteria);
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
            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("EquipmentNumber", equipmentNumber));
            criteria.Add(Restrictions.Eq("Owner", new Owner(ownerId)));
            criteria.Add(Restrictions.Eq("TestDate", testDate));

            return this.persistenceManager.RetrieveAllEqual<T>(criteria);
        }

        /// <summary>
        /// Gets the barcode.
        /// </summary>
        /// <typeparam name="T">Type of barcode</typeparam>
        /// <param name="barcode">The barcode.</param>
        /// <returns>
        /// The barcode
        /// </returns>
        /// <exception cref="System.ArgumentNullException">barcode;Can not retrieve meter barcode, if retrieval criteria is not specified.</exception>
        /// <exception cref="ArgumentNullException">barcode;Can not retrieve barcode, if retrieval criteria is not specified.</exception>
        public T GetBarcode<T>(T barcode) where T : IBarcode
        {
            if (barcode == null)
            {
                throw new ArgumentNullException("barcode", "Can not retrieve meter barcode, if retrieval criteria is not specified.");
            }

            DetachedCriteria criteria = DetachedCriteria.For<T>();
            criteria.Add(Restrictions.Eq("LookupCode", barcode.LookupCode));
            criteria.Add(Restrictions.Eq("Owner", barcode.Owner));

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
    }
}
