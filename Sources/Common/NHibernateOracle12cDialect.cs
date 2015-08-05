//-----------------------------------------------------------------------
// <copyright file="NHibernateOracle12cDialect.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using NHibernate;
    using NHibernate.Dialect;
    using NHibernate.Id;
    using NHibernate.SqlCommand;
    using Environment = NHibernate.Cfg.Environment;

    /// <summary>
    /// Oracle12c dialect implementation for NHibernate 4.0
    /// Some ideas taken from <see href="https://github.com/quaff/hibernate-orm/commit/f035dcd0571e3c07adcb325993c4554592fdddec">hibernate</see>
    /// Should be shared on <see href="https://nhibernate.jira.com/browse/NH-3495">NH-3495</see>
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "c", Justification = "False possitive. Tried to add to CustomDictionary, but it is still reported as violation.")]
    public class NHibernateOracle12cDialect : Oracle10gDialect
    {
        /// <summary>
        /// Does this dialect support identity column key generation?
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports identity columns]; otherwise, <c>false</c>.
        /// </value>
        public override bool SupportsIdentityColumns
        {
            get { return true; }
        }

        /// <summary>
        /// Does the dialect support some form of inserting and selecting
        /// the generated IDENTITY value all in the same statement.
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports insert select identity]; otherwise, <c>false</c>.
        /// </value>
        public override bool SupportsInsertSelectIdentity
        {
            get { return true; }
        }

        /// <summary>
        /// The keyword used to specify an identity column, if native key generation is supported
        /// </summary>
        /// <value>
        /// The identity column string.
        /// </value>
        public override string IdentityColumnString
        {
            get { return "generated as identity"; }
        }

        /// <summary>
        /// The class (which implements <see cref="NHibernate.Id.IIdentifierGenerator" />)
        /// which acts as this dialects native generation strategy.
        /// </summary>
        /// <value>
        /// The native identifier generator class.
        /// </value>
        /// <remarks>
        /// Comes into play whenever the user specifies the native generator.
        /// </remarks>
        public override System.Type NativeIdentifierGeneratorClass
        {
            get { return typeof(NHibernateOracleIdentityGenerator); }
        }

        /// <summary>
        /// Registers the default properties.
        /// </summary>
        protected override void RegisterDefaultProperties()
        {
            base.RegisterDefaultProperties();
            this.DefaultProperties[Environment.UseGetGeneratedKeys] = "true";
        }
    }
}
