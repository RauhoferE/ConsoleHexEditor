//-----------------------------------------------------------------------
// <copyright file="HexNumber.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="HexNumber"/> class.
    /// </summary>
    public class HexNumber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HexNumber"/> class.
        /// </summary>
        /// <param name="hex"> The hex number. </param>
        public HexNumber(string hex)
        {
            this.Number = hex;
        }

        /// <summary>
        /// Gets or sets the position in the file.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long Position
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the hex number.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string Number
        {
            get;
            set;
        }
    }
}