//-----------------------------------------------------------------------
// <copyright file="TextElementCreator.cs" company="FH Wiener Neustadt">
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

    /// <summary>
    /// The <see cref="TextElementCreator"/> class.
    /// </summary>
    public class TextElementCreator
    {
        /// <summary>
        /// The file reader.
        /// </summary>
        private FileReader reader;

        /// <summary>
        /// The current position of the hex.
        /// </summary>
        private long currentPosCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextElementCreator"/> class.
        /// </summary>
        /// <param name="path"> The file path. </param>
        public TextElementCreator(string path)
        {
            this.reader = new FileReader(path);
            this.reader.OnErrorMessagePrint += this.RedirectErrorMessage;
            this.MaxLineCount = FileHelper.GetMaxCharacterOfFile(path);
            this.CurrentPosCount = 0;
            this.CharacterCount = 0;
            this.HexElementCount = 0;
        }

        /// <summary>
        /// This event fires when a page has been created.
        /// </summary>
        public event EventHandler<PageEventArgs> OnPageCreated;

        /// <summary>
        /// This event fires when the length of the file has been counted.
        /// </summary>
        public event EventHandler<LongEventArgs> OnMaxLineCounted;

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessageCreated;

        /// <summary>
        /// This event fires when a message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessageCreated; 

        /// <summary>
        /// Gets the current position of the hex.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long CurrentPosCount
        {
            get
            {
                return this.currentPosCount;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Error position cant be smaller than zero.");
                }

                this.currentPosCount = value;
            }
        }

        /// <summary>
        /// Gets the current length of the file.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long MaxLineCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the byte array from the file.
        /// </summary>
        /// <value> A normal array of <see cref="byte"/>. </value>
        public byte[] Buffer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current character count.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long CharacterCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the hex element count.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long HexElementCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last position on current page.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long LastPosOnCurrentPage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the last character on current page.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long LastCharacterCountOnCurrrentPage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of the current page.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long CurrentPage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current number of the page from the buffer.
        /// </summary>
        /// <value> A normal <see cref="long"/>. </value>
        public long CurrentLocalPage
        {
            get;
            private set;
        }

        /// <summary>
        /// This method changes the size of the buffer.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="IntEventArgs"/>. </param>
        public void ChangeBufferSizeOfReader(object sender, IntEventArgs e)
        {
            this.reader.SetPageSize(e.Number);
        }

        /// <summary>
        /// This method saves the page to the file.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="PageEventArgs"/>. </param>
        public void SaveChangesToBuffer(object sender, PageEventArgs e)
        {
            long posTemp = 0;
            List<string> hexNumbers = new List<string>();

            foreach (var element in e.Page.HexElements)
            {
                foreach (var hex in element.HexSegment)
                {
                    hexNumbers.Add(hex.Number);
                    posTemp++;
                }
            }

            List<byte> tempBytes = this.Buffer.Skip((int)this.HexElementCount - (int)posTemp).Take((int)posTemp).ToList();

            for (int i = 0; i < tempBytes.Count; i++)
            {
                this.Buffer[(int)this.HexElementCount - (int)posTemp + i] = Convert.ToByte(hexNumbers[i], 16);
            }

            this.Buffer = this.reader.SaveChangesToFileAndReturnNewBuffer(this.Buffer);

            this.GetPreviousPagesOutOfBuffer(null, EventArgs.Empty);
            this.GetNextPagesOutOfBuffer(null, EventArgs.Empty);
        }

        /// <summary>
        /// This method gets the next page out of the buffer.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetNextPagesOutOfBuffer(object sender, EventArgs e)
        {
            if (this.Buffer.Length > this.HexElementCount)
            {
                var tempP = new PageElement();
                this.CurrentLocalPage++;
                foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer.Skip((int)this.HexElementCount).Take(10000)
                    .ToArray()))
                {
                    var hexElement = new HexElement(element);
                    hexElement.CalculateHexNumber();

                    foreach (var hex in hexElement.HexSegment)
                    {
                        hex.Position = this.currentPosCount;
                        this.currentPosCount++;
                        this.HexElementCount++;
                    }

                    this.CharacterCount++;
                    tempP.HexElements.Add(hexElement);
                }

                this.FireOnPageCreated(new PageEventArgs(tempP));
            }
            else
            {
                this.CreateNextPageElement(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// This method gets the previous page out of the buffer.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void GetPreviousPagesOutOfBuffer(object sender, EventArgs e)
        {
            if (this.CurrentLocalPage > 1)
            {
                this.FireOnMessageCreated(new StringEventArgs("Please wait."));
                var temp = 0;
                var newPage = this.CurrentLocalPage - 2;
                for (int i = 0; i < this.CurrentLocalPage; i++)
                {
                    foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer.Skip(temp).Take(10000)
                        .ToArray()))
                    {
                        var hexElement = new HexElement(element);
                        hexElement.CalculateHexNumber();

                        foreach (var hex in hexElement.HexSegment)
                        {
                            hex.Position = this.currentPosCount;
                            this.currentPosCount--;
                            this.HexElementCount--;
                            temp++;
                        }

                        this.CharacterCount--;
                    }
                }

                this.CurrentLocalPage = 0;

                for (int i = 0; i < newPage; i++)
                {
                    this.CurrentLocalPage++;
                    foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer.Skip((int)this.HexElementCount).Take(10000)
                        .ToArray()))
                    {
                        var hexElement = new HexElement(element);
                        hexElement.CalculateHexNumber();

                        foreach (var hex in hexElement.HexSegment)
                        {
                            hex.Position = this.currentPosCount;
                            this.currentPosCount++;
                            this.HexElementCount++;
                        }

                        this.CharacterCount++;
                    }
                }

                this.GetNextPagesOutOfBuffer(this, EventArgs.Empty);
            }
            else
            {
                this.CreatePreviousPageElement(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// This method gets the next buffer out of the file and creates a page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void CreateNextPageElement(object sender, EventArgs e)
        {
            if (this.ReadNextPage())
            {
                this.HexElementCount = 0;
                this.CurrentLocalPage++;
                var tempP = new PageElement();
                foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer.Skip((int)this.HexElementCount).Take(10000).ToArray()))
                {
                    var hexElement = new HexElement(element);
                    hexElement.CalculateHexNumber();

                    foreach (var hex in hexElement.HexSegment)
                    {
                        hex.Position = this.currentPosCount;
                        this.currentPosCount++;
                        this.HexElementCount++;
                    }

                    this.CharacterCount++;
                    tempP.HexElements.Add(hexElement);
                }

                this.FireOnPageCreated(new PageEventArgs(tempP));
            }
            else
            {
                this.FireOnMessageCreated(new StringEventArgs("End Reached."));
            }
        }

        /// <summary>
        /// This method gets the previous buffer out of the file and creates a page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void CreatePreviousPageElement(object sender, EventArgs e)
        {
            if (this.ReadPreviousPage())
            {
                this.HexElementCount = 0;
                PageElement tempP = new PageElement();

                while (this.HexElementCount < this.Buffer.Length)
                {
                    tempP = new PageElement();
                    this.CurrentLocalPage++;
                    foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer.Skip((int)this.HexElementCount).Take(10000).ToArray()))
                    {
                        var hexElement = new HexElement(element);
                        hexElement.CalculateHexNumber();

                        foreach (var hex in hexElement.HexSegment)
                        {
                            hex.Position = this.currentPosCount;
                            this.currentPosCount++;
                            this.HexElementCount++;
                        }

                        this.CharacterCount++;
                        tempP.HexElements.Add(hexElement);
                    }
                }

                this.FireOnPageCreated(new PageEventArgs(tempP));
            }
            else
            {
                this.FireOnMessageCreated(new StringEventArgs("Beginn Reached."));
            }
        }

        /// <summary>
        /// This method jumps to the first page.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void JumpToFirstPage(object sender, EventArgs e)
        {
            string path = this.reader.Path;
            this.reader = new FileReader(path);
            this.CurrentPage = 0;
            this.currentPosCount = 0;
            this.CurrentLocalPage = 0;
            this.CharacterCount = 0;
            this.HexElementCount = 0;
            this.LastPosOnCurrentPage = 0;
            this.LastCharacterCountOnCurrrentPage = 0;
            this.CalculateMaxHexNumberLength();

            this.CreateNextPageElement(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method deletes the buffer and the file.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void DeleteBufferAndFile(object sender, EventArgs e)
        {
            this.reader = null;
            this.MaxLineCount = 0;
            this.CurrentPosCount = 0;
            this.CharacterCount = 0;
            this.HexElementCount = 0;
        }

        /// <summary>
        /// This method redirects the error message from the reader.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void RedirectErrorMessage(object sender, StringEventArgs e)
        {
            this.FireOnErrorMessageCreated(e);
        }

        /// <summary>
        /// This method calculates the length of the file.
        /// </summary>
        public void CalculateMaxHexNumberLength()
        {
            this.FireOnMaxLineCounted(new LongEventArgs(this.MaxLineCount));
        }

        /// <summary>
        /// This method fires the <see cref="OnPageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="PageEventArgs"/>. </param>> 
        protected virtual void FireOnPageCreated(PageEventArgs e)
        {
            this.OnPageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnMaxLineCounted"/> event.
        /// </summary>
        /// <param name="e">A <see cref="LongEventArgs"/>. </param>> 
        protected virtual void FireOnMaxLineCounted(LongEventArgs e)
        {
            this.OnMaxLineCounted?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnErrorMessageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnErrorMessageCreated(StringEventArgs e)
        {
            this.OnErrorMessageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnMessageCreated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnMessageCreated(StringEventArgs e)
        {
            this.OnMessageCreated?.Invoke(this, e);
        }

        /// <summary>
        /// This method reads the next page from the file.
        /// </summary>
        /// <returns> It returns true if the file has been read in. </returns>
        private bool ReadNextPage()
        {
            try
            {
                if (this.CharacterCount + (this.reader.PageSize * 100000) < this.MaxLineCount)
                {
                    this.FireOnMessageCreated(new StringEventArgs("Please wait."));
                    this.Buffer = this.reader.GetNextCharactersFromFile();
                    this.currentPosCount = this.LastPosOnCurrentPage;
                    this.CharacterCount = this.LastCharacterCountOnCurrrentPage;
                    this.CurrentPage++;
                    this.CurrentLocalPage = 0;
                    this.CountToLastPositionOnCurrentpage();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                this.FireOnErrorMessageCreated(new StringEventArgs("Error: " + e.Message));
                return false;
            }
        }

        /// <summary>
        /// This method counts to the last position on current page.
        /// </summary>
        private void CountToLastPositionOnCurrentpage()
        {
            foreach (var element in CustomEncoding.UTF8.GetChars(this.Buffer))
            {
                var hexElement = new HexElement(element);
                hexElement.CalculateHexNumber();

                foreach (var hex in hexElement.HexSegment)
                {
                    hex.Position = this.currentPosCount;
                    this.LastPosOnCurrentPage++;
                }

                this.LastCharacterCountOnCurrrentPage++;
            }
        }

        /// <summary>
        /// This method reads the previous page from the file.
        /// </summary>
        /// <returns> It returns true if the file has been read in. </returns>
        private bool ReadPreviousPage()
        {
            if (this.CurrentPage > 1)
            {
                var countToPageNumber = this.CurrentPage - 1;
                this.CurrentPage = 0;
                this.currentPosCount = 0;
                this.CurrentLocalPage = 0;
                this.CharacterCount = 0;
                this.HexElementCount = 0;
                this.LastPosOnCurrentPage = 0;
                this.LastCharacterCountOnCurrrentPage = 0;
                this.reader.ResetIndex();

                for (int i = 0; i < countToPageNumber; i++)
                {
                    this.FireOnMessageCreated(new StringEventArgs("Please wait."));
                    this.ReadNextPage();
                }

                return true;
            }

            return false;
        }
    }
}