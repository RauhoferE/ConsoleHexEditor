//-----------------------------------------------------------------------
// <copyright file="NullView.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    using System;

    /// <summary>
    /// The <see cref="NullView"/> class.
    /// </summary>
    public class NullView : ICurrentView
    {
        /// <summary>
        /// The name of the modifier.
        /// </summary>
        private string mod;

        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string Name
        {
            get { return "NullView"; }
        }

        /// <summary>
        /// Gets or sets the name of the modifier.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string Modifier
        {
            get
            {
                return this.mod;
            }

            set
            {
                throw new ArgumentException("Error cant set null.");
            }
        }
    }
}