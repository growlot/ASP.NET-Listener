//-----------------------------------------------------------------------
// <copyright file="PersistenceManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using AMSLLC.Listener.Common.Model;
    using log4net;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Criterion;
    using NHibernate.Dialect;
    using NHibernate.Engine;
    using Conf = System.Configuration;

    /// <summary>
    /// Specifies whether to begin a new session, continue an existing session, or end an existing session.
    /// </summary>
    public enum SessionAction 
    {
        /// <summary>
        /// Create new session for request and leave it open.
        /// </summary>
        Begin,

        /// <summary>
        /// Use current session for request and leave it open.
        /// </summary>
        Continue,

        /// <summary>
        /// Close current session after request is done.
        /// </summary>
        End,

        /// <summary>
        /// Create new session and close it after request is done.
        /// </summary>
        BeginAndEnd 
    }

    /// <summary>
    /// Manages sessions and implements CRUD for data entities.
    /// </summary>
    public class PersistenceManager : IPersistenceManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The session factory
        /// </summary>
        private ISessionFactory sessionFactory = null;

        /// <summary>
        /// The session
        /// </summary>
        private ISession session = null;

        /// <summary>
        /// The connection string
        /// </summary>
        private string connectionString = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceManager"/> class.
        /// </summary>
        public PersistenceManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public PersistenceManager(string connectionString)
        {
            this.connectionString = connectionString;
            this.ConfigureNHibernate();
        }

        /// <summary>
        /// Gets the database type.
        /// </summary>
        /// <value>
        /// The database (ex. MSSQL2005, Oracle, etc.)
        /// </value>
        public string Database 
        {
            get
            {
                var implementor = this.sessionFactory as ISessionFactoryImplementor;
                Dialect dialect = implementor.Dialect;
                string database = dialect.ToString();
                database = database.Replace("NHibernate.Dialect.", string.Empty);
                database = database.Replace("Dialect", string.Empty);
                return database;
            }
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString 
        {
            get
            {
                return this.connectionString;
            }
        }
        
        /// <summary>
        /// Retrieves all objects of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="sessionAction">The session action.</param>
        /// <returns>
        /// A list of all objects of the specified type.
        /// </returns>
        public IList<T> RetrieveAll<T>(SessionAction sessionAction)
        {
            /* Note that NHibernate guarantees that two object references will point to the
             * same object only if the references are set in the same session. For example,
             * Order #123 under the Customer object Able Inc and Order #123 in the Orders
             * list will point to the same object only if we load Customers and Orders in 
             * the same session. If we load them in different sessions, then changes that
             * we make to Able Inc's Order #123 will not be reflected in Order #123 in the
             * Orders list, since the references point to different objects. That's why we
             * maintain a session as a member variable, instead of as a local variable. */
            
            // Open a new session if specified
            if ((sessionAction == SessionAction.Begin) || (sessionAction == SessionAction.BeginAndEnd))
            {
                this.session = this.sessionFactory.OpenSession();
            }

            // Retrieve all objects of the type passed in
            ICriteria targetObjects = this.session.CreateCriteria(typeof(T));            
            IList<T> itemList = targetObjects.List<T>();

            // Close the session if specified
            if ((sessionAction == SessionAction.End) || (sessionAction == SessionAction.BeginAndEnd))
            {
                this.session.Close();
                this.session.Dispose();
            }

            // Set return value
            return itemList;
        }

        /// <summary>
        /// Retrieves all objects of a specified type meeting specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="selectCriteria">The selection criteria.</param>
        /// <returns>
        /// A list of all objects of the specified type meeting specified criteria.
        /// </returns>
        public IList<T> RetrieveAllEqual<T>(DetachedCriteria selectCriteria)
        {
            if (selectCriteria == null)
            {
                string exceptionMessage = "Can not retrieve results because selectCriteria is null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("selectCriteria", exceptionMessage);
            }

            IList<T> matchingObjects = null;
            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = selectCriteria.GetExecutableCriteria(localSession);

                // Get the matching objects
                matchingObjects = criteria.List<T>();
            }

            // Set return value
            return matchingObjects;
        }

        /// <summary>
        /// Retrieves first object of a specified type meeting specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="selectCriteria">The selection criteria.</param>
        /// <returns>
        /// First object of the specified type meeting specified criteria.
        /// </returns>
        public T RetrieveFirstEqual<T>(DetachedCriteria selectCriteria)
        {
            if (selectCriteria == null)
            {
                string exceptionMessage = "Can not retrieve results because selectCriteria is null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("selectCriteria", exceptionMessage);
            }

            T matchingObjects = default(T);
            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = selectCriteria.GetExecutableCriteria(localSession);

                // Get the matching objects
                matchingObjects = criteria.UniqueResult<T>();
            }

            // Set return value
            return matchingObjects;
        }

        /// <summary>
        /// Retrieves first object of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>
        /// First object meeting the specified criteria.
        /// </returns>
        public T RetrieveFirstEqual<T>(string propertyName, object propertyValue)
        {
            T matchingObjects = default(T);

            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                // Create a criteria object with the specified criteria
                ICriteria criteria = localSession.CreateCriteria(typeof(T));
                criteria.Add(Restrictions.Eq(propertyName, propertyValue));
                criteria.SetMaxResults(1);

                // Get the matching objects
                matchingObjects = criteria.UniqueResult<T>();
            }

            // Set return value
            return matchingObjects;
        }

        /// <summary>
        /// Gets the object by primary key.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="key">The primary key.</param>
        /// <returns>Object represented by primary key.</returns>
        public T GetByKey<T>(object key)
        {
            return this.GetByKey<T>(key, false);
        }

        /// <summary>
        /// Gets the object by primary key.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="key">The primary key.</param>
        /// <param name="lazy">If set to <c>true</c> [lazy] uses nHibernate Load method. If set to <c>false</c> uses Get method.</param>
        /// <returns>
        /// Object represented by primary key.
        /// </returns>
        public T GetByKey<T>(object key, bool lazy)
        {
            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                if (lazy)
                {
                    return localSession.Load<T>(key);
                }

                return localSession.Get<T>(key);
            }
        }
        
        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be saved.</typeparam>
        /// <param name="item">The item.</param>
        public void Save<T>(T item)
        {
            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                using (localSession.BeginTransaction())
                {
                    localSession.SaveOrUpdate(item);
                    localSession.Transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Saves all objects and their persistent children using statelessSession.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be saved.</typeparam>
        /// <param name="items">The list of items.</param>
        public void SaveBulk<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                // localSession.SetBatchSize(100);
                using (localSession.BeginTransaction())
                {
                    foreach (T item in items)
                    {
                        localSession.SaveOrUpdate(item);
                    }

                    localSession.Transaction.Commit();
                }
            } 

            ////using (IStatelessSession statelessSession = sessionFactory.OpenStatelessSession())
            ////{
            ////    statelessSession.SetBatchSize(100);
            ////    using (statelessSession.BeginTransaction())
            ////    {
            ////        foreach (T item in items)
            ////        {
            ////            statelessSession.Insert(item);
            ////        }
            ////        statelessSession.Transaction.Commit();
            ////    }
            ////}
        }

        /// <summary>
        /// Deletes all persisted items of specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public void DeleteAll<T>()
        {
            using (ISession localSession = this.sessionFactory.OpenSession())
            {
                localSession.CreateQuery("delete from " + typeof(T).ToString()).ExecuteUpdate();
            } 
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.sessionFactory != null)
                {
                    this.sessionFactory.Close();
                }
            }
        }

        /// <summary>
        /// Configures NHibernate and creates a member-level session factory.
        /// </summary>
        private void ConfigureNHibernate()
        {
            // Initialize
            Configuration configuration = new Configuration();
            if (this.connectionString != null)
            {
                configuration.SetProperty("connection.connection_string", this.connectionString);
            }
            else
            {
                configuration.SetProperty("connection.connection_string", Conf.ConfigurationManager.ConnectionStrings["ListenerDB"].ConnectionString);
            }

            configuration.Configure();

            /* Note: The AddAssembly() method requires that mappings be 
             * contained in hbm.xml files whose BuildAction properties 
             * are set to ‘Embedded Resource’. */

            // Add class mappings to configuration object
            Assembly thisAssembly = typeof(Config).Assembly;
            configuration.AddAssembly(thisAssembly);

            // Create session factory from configuration object
            this.sessionFactory = configuration.BuildSessionFactory();
            this.connectionString = configuration.Properties["connection.connection_string"];
        }
    }
}
