//-----------------------------------------------------------------------
// <copyright file="IRenderer.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="IRenderer"/> interface.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Gets the window width.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        int WindowWith { get; }

        /// <summary>
        /// Gets the window height.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        int WindowHeight { get; }

        /// <summary>
        /// Gets the length of the offset.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        int LengthOfPositionvalue { get; }

        /// <summary>
        /// Renders the hex view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        void RenderHexView(object sender, PageEventArgs page);

        /// <summary>
        /// Renders the text view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        void RenderTextView(object sender, PageEventArgs page);

        /// <summary>
        /// Renders the split view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        void RenderSplitView(object sender, PageEventArgs page);

        /// <summary>
        /// Changes the console encoding.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CustomEncodingEventArgs"/>. </param>
        void ChangeEncoding(object sender, CustomEncodingEventArgs e);

        /// <summary>
        /// Changes the length of the offset.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="LongEventArgs"/>. </param>
        void ChangeLengthOfPositionValue(object sender, LongEventArgs e);

        /// <summary>
        /// Prints an error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void PrintErrorMessage(object sender, StringEventArgs e);

        /// <summary>
        /// Prints a normal message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void PrintMessage(object sender, StringEventArgs e);

        /// <summary>
        /// Prints a page an marks the current position of the cursor.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        void PrintPageToEdit(object sender, PageAndPositionEventArgs page);

        /// <summary>
        /// Prints a page and colors the position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        void PrintSplitViewAndColorPosition(object sender, PageAndPositionEventArgs page);

        /// <summary>
        /// Prints the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void PrintUserInput(object sender, StringEventArgs e);

        /// <summary>
        /// Deletes the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        void DeleteUserInput(object sender, EventArgs e);
    }
}