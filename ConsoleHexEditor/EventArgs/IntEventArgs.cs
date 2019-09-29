//-----------------------------------------------------------------------
// <copyright file="IntEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="IntEventArgs"/> class.
    /// </summary>
    public class IntEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntEventArgs"/> class.
        /// </summary>
        /// <param name="i"> The number. </param>
        public IntEventArgs(int i)
        {
            this.Number = i;
        }

        /// <summary>
        /// Gets the current number.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        public int Number
        {
            get;
            private set;
        }
    }
}