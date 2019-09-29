//-----------------------------------------------------------------------
// <copyright file="InputValidatorForExitMenu.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForExitMenu"/> class.
    /// </summary>
    public class InputValidatorForExitMenu
    {
        /// <summary>
        /// This event fires when the user wants to save the changes.
        /// </summary>
        public event System.EventHandler OnSaveButtonPressed;

        /// <summary>
        /// This event fires when the user don't wants to save the changes.
        /// </summary>
        public event System.EventHandler OnDontSaveButtonPressed;

        /// <summary>
        /// This event fires when the user wants to save the changes.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessagePrint;

        /// <summary>
        /// This method print the choice.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void PrintChoice(object sender, EventArgs e)
        {
            this.FireOnMessagePrint(new StringEventArgs("Press y to save and n to not save."));
        }

        /// <summary>
        /// This method gets the input from the <see cref="KeyboardWatcher"/>.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            switch (e.Key)
            {
                case ConsoleKey.Y:
                    this.FireOnSaveButtonPressed();
                    break;
                case ConsoleKey.N:
                    this.FireOnDontSaveButtonPressed();
                    break;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnSaveButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnSaveButtonPressed()
        {
            this.OnSaveButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnDontSaveButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnDontSaveButtonPressed()
        {
            this.OnDontSaveButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>
        protected virtual void FireOnMessagePrint(StringEventArgs e)
        {
            this.OnMessagePrint?.Invoke(this, e);
        }
    }
}