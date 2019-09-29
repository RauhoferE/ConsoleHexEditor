//-----------------------------------------------------------------------
// <copyright file="CustomEncodingEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    using System.Text;

    /// <summary>
    /// The <see cref="CustomEncodingEventArgs"/> class.
    /// </summary>
    public class CustomEncodingEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomEncodingEventArgs"/> class.
        /// </summary>
        /// <param name="enc"> The chosen encoding. </param>
        public CustomEncodingEventArgs(Encoding enc)
        {
            this.Current = enc;
        }

        /// <summary>
        /// Gets the current encoding.
        /// </summary>
        /// <value> A <see cref="Encoding"/> object. </value>
        public Encoding Current
        {
            get;
            private set;
        }
    }
}