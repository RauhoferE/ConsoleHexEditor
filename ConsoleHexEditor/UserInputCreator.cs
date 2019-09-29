//-----------------------------------------------------------------------
// <copyright file="UserInputCreator.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="UserInputCreator"/> class.
    /// </summary>
    public class UserInputCreator
    {
        /// <summary>
        /// Represents the user input.
        /// </summary>
        private string userInput;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInputCreator"/> class.
        /// </summary>
        public UserInputCreator()
        {
            this.userInput = string.Empty;
        }

        /// <summary>
        /// This event fires when user wants to send the input.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnSendInput;

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// This event fires when the user input should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnUserInputPrint;

        /// <summary>
        /// This event fires when the user input should be deleted.
        /// </summary>
        public event EventHandler OnUserInputDelete;

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
        /// This method lets the user enter a character.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void EnterCharacter(object sender, StringEventArgs e)
        {
            this.userInput = this.userInput + e.Text;
            this.FireOnUserInputPrint(new StringEventArgs(this.userInput));
        }

        /// <summary>
        /// This method sends the input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SendInput(object sender, EventArgs e)
        {
            if (!FileHelper.CheckIfFileExists(this.userInput))
            {
                this.FireOnErrorMessagePrint(new StringEventArgs("Error file not found."));
                this.FireOnUserInputPrint(new StringEventArgs(this.userInput));
            }
            else
            {
                this.FireOnSendInput(new StringEventArgs(this.userInput));
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnUserInputPrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnUserInputPrint(StringEventArgs e)
        {
            this.OnUserInputPrint?.Invoke(this, e);
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
        /// This method fires the <see cref="OnErrorMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnErrorMessagePrint(StringEventArgs e)
        {
            this.OnErrorMessagePrint?.Invoke(this, e);
        }
    }
}