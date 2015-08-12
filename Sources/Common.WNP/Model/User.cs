//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    /// <summary>
    /// Data model class representing User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        /// <value>
        /// The users first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        /// <value>
        /// The users last name.
        /// </value>
        public string LastName { get; set; }
    }
}
