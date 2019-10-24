//-----------------------------------------------------------------------
// <copyright file="EditorForHexaElements.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="EditorForHexaElements"/> class.
    /// </summary>
    public class EditorForHexaElements
    {
        /// <summary>
        /// Represents the <see cref="PageElement"/> object.
        /// </summary>
        private PageElement page;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorForHexaElements"/> class.
        /// </summary>
        public EditorForHexaElements()
        {
            this.Page = new PageElement();
            this.Position = 0;
            this.TotalHexNumbersOnPage = 0;
        }

        /// <summary>
        /// This event fires when the position has been changed.
        /// </summary>
        public event System.EventHandler<PageAndPositionEventArgs> OnPositionChanged;

        /// <summary>
        /// This event fires when the changes should be saved.
        /// </summary>
        public event EventHandler<PageEventArgs> OnSaveChanges;

        /// <summary>
        /// Gets the position of the cursor.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long Position
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value> A normal <see cref="PageElement"/>. </value>
        public PageElement Page
        {
            get
            {
                return this.page;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Error page is null.");
                }

                this.page = value;
            }
        }

        /// <summary>
        /// Gets the total hex numbers of the site.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long TotalHexNumbersOnPage
        {
            get;
            private set;
        }

        /// <summary>
        /// This method calculates the hex numbers on the page.
        /// </summary>
        public void CalculateTotalHexNumbersOnPage()
        {
            this.TotalHexNumbersOnPage = 0;

            foreach (var element in this.Page.HexElements)
            {
                foreach (var hexNumber in element.HexSegment)
                {
                    this.TotalHexNumbersOnPage = this.TotalHexNumbersOnPage + 2;
                }
            }
        }

        /// <summary>
        /// This method sets the page of the editor.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="PageEventArgs"/>. </param>
        public void SetPage(object sender, PageEventArgs e)
        {
            this.Page = new PageElement();
            foreach (var item in e.Page.HexElements)
            {
                var hex = new HexElement(item.TextSegment);
                foreach (var item2 in item.HexSegment)
                {
                    var hexseg = new HexNumber(item2.Number);
                    hexseg.Position = item2.Position;
                    hex.HexSegment.Add(hexseg);
                }

                this.Page.HexElements.Add(hex);
            }

            this.Position = 0;
            this.CalculateTotalHexNumbersOnPage();

            this.FireOnPositionChanged(new PageAndPositionEventArgs(this.Page, this.Position));
        }

        /// <summary>
        /// This method jumps the cursor to the next position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void JumpToNextPosition(object sender, EventArgs e)
        {
            this.Position++;

            if (this.Position == this.TotalHexNumbersOnPage)
            {
                this.Position = 0;
            }

            this.FireOnPositionChanged(new PageAndPositionEventArgs(this.Page, this.Position));
        }

        /// <summary>
        /// This method jumps the cursor to the previous position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void JumpToPreviousPosition(object sender, EventArgs e)
        {
            this.Position--;

            if (this.Position < 0)
            {
                this.Position = this.TotalHexNumbersOnPage - 1;
            }

            this.FireOnPositionChanged(new PageAndPositionEventArgs(this.Page, this.Position));
        }

        /// <summary>
        /// This method changes the hex at the position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void ChangeHexAtCurrentPositionChanges(object sender, ConsoleKeyEventArgs e)
        {
            int temp = 0;

            foreach (var element in this.Page.HexElements)
            {
                foreach (var hex in element.HexSegment)
                {
                    foreach (var character in hex.Number)
                    {
                        if (temp == this.Position)
                        {
                            if (temp % 2 == 0)
                            {
                                hex.Number = hex.Number.Remove(0, 1);
                                hex.Number = e.ConsoleChar.ToString() + hex.Number;
                                element.CalculateText();
                            }
                            else
                            {
                                hex.Number = hex.Number.Remove(1, 1);
                                hex.Number = hex.Number + e.ConsoleChar.ToString();
                                element.CalculateText();
                            }
                        }

                        temp++;
                    }
                }
            }

            this.FireOnPositionChanged(new PageAndPositionEventArgs(this.Page, this.Position));
        }

        /// <summary>
        /// This method saves the changes.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SaveChanges(object sender, EventArgs e)
        {
            this.FireOnSaveChanges(new PageEventArgs(this.Page));
        }

        /// <summary>
        /// This method fires the <see cref="OnPositionChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageAndPositionEventArgs"/>. </param>> 
        protected virtual void FireOnPositionChanged(PageAndPositionEventArgs e)
        {
            this.OnPositionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnSaveChanges"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnSaveChanges(PageEventArgs e)
        {
            this.OnSaveChanges?.Invoke(this, e);
        }
    }
}