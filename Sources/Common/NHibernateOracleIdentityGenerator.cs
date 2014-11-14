//-----------------------------------------------------------------------
// <copyright file="NHibernateOracleIdentityGenerator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using NHibernate.Engine;
    using NHibernate.Id;
    using NHibernate.Id.Insert;
    
    /// <summary>
    /// A generator which combines identity generation with immediate retrieval
    /// by attaching a output parameter to the SQL command
    /// </summary>
    public class NHibernateOracleIdentityGenerator : IdentityGenerator
    {
        /// <summary>
        /// Gets the insert generated identifier delegate.
        /// </summary>
        /// <param name="persister">The persistence implementation.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="isGetGeneratedKeysEnabled">Not used. Needed only to implement interface.</param>
        /// <returns>The insert generated identifier delegate.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "isGetGeneratedKeysEnabled", Justification = "Needed to correctly implement interface")]
        public override IInsertGeneratedIdentifierDelegate GetInsertGeneratedIdentifierDelegate(
            IPostInsertIdentityPersister persister,
            ISessionFactoryImplementor factory,
            bool isGetGeneratedKeysEnabled)
        {
            return new OutputParamReturningDelegate(persister, factory);
        }
    }
}