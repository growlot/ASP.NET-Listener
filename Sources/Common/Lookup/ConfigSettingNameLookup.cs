//-----------------------------------------------------------------------
// <copyright file="ConfigSettingNameLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// List of possible listener config settings
    /// </summary>
    public enum ConfigSettingNameLookup
    {
        /// <summary>
        /// The listener state
        /// </summary>
        [Description("listener.enabled")]
        ListenerEnabled,

        /// <summary>
        /// The listener URL
        /// </summary>
        [Description("listener.url")]
        ListenerUrl,

        /// <summary>
        /// The listener database connection string
        /// </summary>
        [Description("listener.db")]
        ListenerDb,

        /// <summary>
        /// The notifications state
        /// </summary>
        [Description("notifications.enabled")]
        NotificationsEnabled,

        /// <summary>
        /// The SMTP server used for notification sending
        /// </summary>
        [Description("notifications.smpt.server")]
        NotificationsSmtpServer,

        /// <summary>
        /// The SMTP user used for notification sending
        /// </summary>
        [Description("notifications.smpt.user")]
        NotificationsSmtpUser,

        /// <summary>
        /// The SMTP user password used for notification sending
        /// </summary>
        [Description("notifications.smpt.password")]
        NotificationsSmtpPassword
    }
}
