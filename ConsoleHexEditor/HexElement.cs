//-----------------------------------------------------------------------
// <copyright file="HexElement.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="HexElement"/> class.
    /// </summary>
    public class HexElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HexElement"/> class.
        /// </summary>
        /// <param name="text"> The character. </param>
        public HexElement(char text)
        {
            this.TextSegment = text;
            this.HexSegment = new List<HexNumber>();
        }

        /// <summary>
        /// Gets the list of <see cref="HexNumber"/>.
        /// </summary>
        /// <value> A normal list of <see cref="HexNumber"/>. </value>
        public List<HexNumber> HexSegment
        {
            get;
        }

        /// <summary>
        /// Gets or sets the byte as a character.
        /// </summary>
        /// <value> A normal <see cref="char"/>. </value>
        public char TextSegment
        {
            get;
            set;
        }

        /// <summary>
        /// This method calculates the character out of the bytes.
        /// </summary>
        public void CalculateText()
        {
            string wholeHexaDecNumber = string.Empty;
            
            foreach (var number in this.HexSegment)
            {
                wholeHexaDecNumber = wholeHexaDecNumber + number.Number;
            }

            var temp = Enumerable.Range(0, wholeHexaDecNumber.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(wholeHexaDecNumber.Substring(x, 2), 16))
                .ToArray();

           this.TextSegment = CustomEncoding.UTF8.GetChars(temp).FirstOrDefault();
        }

        /// <summary>
        /// This method calculates the hex numbers out of the character.
        /// </summary>
        public void CalculateHexNumber()
        {
            var temp = CustomEncoding.UTF8.GetBytes(new char[] { this.TextSegment });
            
            StringBuilder hex = new StringBuilder(temp.Length * 2);

            foreach (byte b in temp)
            {
                hex.AppendFormat("{0:X2}", b);
            }

            if (hex.Length % 2 != 0)
            {
                hex.Append('0');
            }

            for (int i = 0; i < hex.Length;)
            { 
                this.HexSegment.Add(new HexNumber(hex.ToString().Substring(i, 2)));
                i = i + 2;
            }
        }
    }
}