//-----------------------------------------------------------------------
// <copyright file="PageEventArgs.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="PageEventArgs"/> class.
    /// </summary>
    public class PageEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageEventArgs"/> class.
        /// </summary>
        /// <param name="page"> The <see cref="PageElement"/> to be printed. </param>
        public PageEventArgs(PageElement page)
        {
            this.Page = page;
        }

        /// <summary>
        /// Gets the <see cref="PageElement"/>.
        /// </summary>
        /// <value> A normal <see cref="PageElement"/>. </value>
        public PageElement Page
        {
            get;
            private set;
        }
    }
}