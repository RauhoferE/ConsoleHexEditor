//-----------------------------------------------------------------------
// <copyright file="ICurrentView.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    /// <summary>
    /// The <see cref="ICurrentView"/> interface.
    /// </summary>
    public interface ICurrentView
    {
        /// <summary>
        /// Gets the name of the view.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets or sets the name of the modifier.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        string Modifier
        {
            get;
            set;
        }
    }
}