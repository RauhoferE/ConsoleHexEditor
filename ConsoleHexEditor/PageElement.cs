//-----------------------------------------------------------------------
// <copyright file="PageElement.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="PageElement"/> class.
    /// </summary>
    public class PageElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageElement"/> class.
        /// </summary>
        public PageElement()
        {
            this.HexElements = new List<HexElement>();
        }

        /// <summary>
        /// Gets the list of <see cref="HexElement"/> class.
        /// </summary>
        /// <value> A normal list of <see cref="HexElement"/>. </value>
        public List<HexElement> HexElements
        {
            get;
        }
    }
}