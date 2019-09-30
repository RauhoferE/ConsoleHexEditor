//-----------------------------------------------------------------------
// <copyright file="StringEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="StringEventArgs"/> class.
    /// </summary>
    public class StringEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEventArgs"/> class.
        /// </summary>
        /// <param name="message"> The message to be printed. </param>
        public StringEventArgs(string message)
        {
            this.Text = message;
        }

        /// <summary>
        /// Gets the <see cref="string"/> to be printed.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string Text
        {
            get;
        }
    }
}