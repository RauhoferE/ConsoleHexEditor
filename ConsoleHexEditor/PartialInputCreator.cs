//-----------------------------------------------------------------------
// <copyright file="PartialInputCreator.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="PartialInputCreator"/> class.
    /// </summary>
    public class PartialInputCreator
    {
        /// <summary>
        /// Represents the user input.
        /// </summary>
        private string userInput;

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// This event fires when a message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessagePrint;

        /// <summary>
        /// This event fires when the user input should be send.
        /// </summary>
        public event System.EventHandler<IntEventArgs> OnSendInput;

        /// <summary>
        /// This event fires when the deletes the input.
        /// </summary>
        public event EventHandler OnUserInputDelete;

        /// <summary>
        /// This event fires when the user input should be printed.
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
        /// This method send the input to the partial manager.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SendInput(object sender, EventArgs e)
        {
            if (int.Parse(this.userInput) < 1 || int.Parse(this.userInput) > 50)
            {
                this.FireOnErrorMessagePrint(new StringEventArgs("Allowed is 1 - 50, Only full numbers"));
                this.FireOnUserInputPrint(new StringEventArgs(this.userInput.ToString()));
            }
            else
            {
                this.FireOnMessagePrint(new StringEventArgs("Partial Loading size successfully changed"));
                this.FireOnSendInput(new IntEventArgs(int.Parse(this.userInput)));
            }
        }

        /// <summary>
        /// This method enters the given character and adds it to the string.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void EnterCharacter(object sender, StringEventArgs e)
        {
            this.userInput = this.userInput + e.Text;
            this.FireOnUserInputPrint(new StringEventArgs(this.userInput));
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
        /// This method fires the <see cref="OnUserInputPrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnUserInputPrint(StringEventArgs e)
        {
            this.OnUserInputPrint?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnUserInputDelete"/> event.
        /// </summary>
        protected virtual void FireOnUserInputDelete()
        {
            this.OnUserInputDelete?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnSendInput"/> event.
        /// </summary>
        /// <param name="e">A <see cref="IntEventArgs"/>. </param>> 
        protected virtual void FireOnSendInput(IntEventArgs e)
        {
            this.OnSendInput?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnMessagePrint(StringEventArgs e)
        {
            this.OnMessagePrint?.Invoke(this, e);
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