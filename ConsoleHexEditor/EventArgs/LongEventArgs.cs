//-----------------------------------------------------------------------
// <copyright file="LongEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="LongEventArgs"/> class.
    /// </summary>
    public class LongEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LongEventArgs"/> class.
        /// </summary>
        /// <param name="value"> The value. </param>
        public LongEventArgs(long value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value of the <see cref="long"/>.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long Value
        {
            get;
            private set;
        }
    }
}