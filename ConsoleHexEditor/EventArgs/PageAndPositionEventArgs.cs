//-----------------------------------------------------------------------
// <copyright file="PageAndPositionEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="PageAndPositionEventArgs"/> class.
    /// </summary>
    public class PageAndPositionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageAndPositionEventArgs"/> class.
        /// </summary>
        /// <param name="page"> The <see cref="PageElement"/> to be printed. </param>
        /// <param name="pos"> The position to be colored. </param>
        public PageAndPositionEventArgs(PageElement page, long pos)
        {
            this.Page = page;
            this.PositionToMark = pos;
        }

        /// <summary>
        /// Gets the <see cref="PageElement"/>.
        /// </summary>
        /// <value> A <see cref="PageElement"/> object. </value>
        public PageElement Page
        {
            get;
        }

        /// <summary>
        /// Gets the position to be marked.
        /// </summary>
        /// <value> A <see cref="long"/> value. </value>
        public long PositionToMark
        {
            get;
        }
    }
}