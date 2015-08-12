//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        public User(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets company where user works.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public Company Company { get; set; }

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
