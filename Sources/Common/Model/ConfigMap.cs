//-----------------------------------------------------------------------
// <copyright file="ConfigMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="Config"/> class
    /// </summary>
    public class ConfigMap : ClassMapping<Config>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigMap"/> class.
        /// </summary>
        public ConfigMap()
        {
            this.Table("wndba.tlistener_config");
            this.Lazy(false);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("config_id");
                    m.Generator(Generators.Native);
                });
            this.Property(x => x.Name, m => m.Column("config_name"));
            this.Property(x => x.Value, m => m.Column("config_value"));
        }
    }
}