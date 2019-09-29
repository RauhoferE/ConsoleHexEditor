//-----------------------------------------------------------------------
// <copyright file="GoToInputCreator.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="GoToInputCreator"/> class.
    /// </summary>
    public class GoToInputCreator
    {
        /// <summary>
        /// Represents the user input.
        /// </summary>
        private string userInput;

        /// <summary>
        /// Represents the number of bytes on the file.
        /// </summary>
        private long maxBytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoToInputCreator"/> class.
        /// </summary>
        public GoToInputCreator()
        {
            this.userInput = string.Empty;
        }

        /// <summary>
        /// This event fires when a error message should be printed..
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// This event fires when the input should be send.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnSendInput;

        /// <summary>
        /// This event fires when the user deletes the input.
        /// </summary>
        public event EventHandler OnUserInputDelete;

        /// <summary>
        /// This event fires when user input should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnUserInputPrint;

        /// <summary>
        /// Gets the user input.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string UserInput
        {
            get
            {
                return this.userInput;
            }
        }

        /// <summary>
        /// This method deletes the last character.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void DeleteLastCharacter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.userInput))
            {
                this.userInput = this.userInput.Substring(0, this.userInput.Length - 1);
                this.FireOnUserInputDelete();
            }
        }

        /// <summary>
        /// This method sets the max bytes of the file.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="LongEventArgs"/>. </param>
        public void SetMaxBytes(object sender, LongEventArgs e)
        {
            this.maxBytes = e.Value;
        }

        /// <summary>
        /// This method enters a character.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void EnterCharacter(object sender, StringEventArgs e)
        {
            this.userInput = this.userInput + e.Text;
            this.FireOnUserInputPrint(new StringEventArgs(this.userInput));
        }

        /// <summary>
        /// This method sends input to the page manager.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SendInput(object sender, EventArgs e)
        {
            if (this.maxBytes > long.Parse(this.userInput, System.Globalization.NumberStyles.HexNumber))
            {
                this.FireOnErrorMessagePrint(new StringEventArgs("Error offset to high"));
                this.FireOnUserInputPrint(new StringEventArgs(this.userInput));
            }
            else
            {
                this.FireOnSendInput(new StringEventArgs(this.userInput));
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnErrorMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnErrorMessagePrint(StringEventArgs e)
        {
            this.OnErrorMessagePrint?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnSendInput"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnSendInput(StringEventArgs e)
        {
            this.OnSendInput?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnUserInputDelete"/> event.
        /// </summary>
        protected virtual void FireOnUserInputDelete()
        {
            this.OnUserInputDelete?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnUserInputPrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnUserInputPrint(StringEventArgs e)
        {
            this.OnUserInputPrint?.Invoke(this, e);
        }
    }
}