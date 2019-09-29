//-----------------------------------------------------------------------
// <copyright file="SplitView.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="SplitView"/> class.
    /// </summary>
    public class SplitView : ICurrentView
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
            get { return "SplitView"; }
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
                if (value == "End")
                {
                    this.mod = value;
                }
                else if (value == "Begin")
                {
                    this.mod = value;
                }
                else if (value == null)
                {
                    this.mod = null;
                }
                else
                {
                    throw new ArgumentException("Error the value has to be begin or end.");
                }
            }
        }
    }
}