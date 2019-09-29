//-----------------------------------------------------------------------
// <copyright file="InputvalidatorForFileSearching.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputvalidatorForFileSearching"/> class.
    /// </summary>
    public class InputvalidatorForFileSearching
    {
        /// <summary>
        /// This event fires when the escape button is pressed.
        /// </summary>
        public event System.EventHandler OnEscapeKeyPressed;

        /// <summary>
        /// This event fires when the enter button is pressed.
        /// </summary>
        public event System.EventHandler OnEnterPressed;

        /// <summary>
        /// This event fires when a valid input key has been pressed.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnStringInput;

        /// <summary>
        /// This event fires when the delete button is pressed.
        /// </summary>
        public event System.EventHandler OnDeletePressed;

        /// <summary>
        /// This method gets the input from the <see cref="KeyboardWatcher"/>.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            switch (e.Key)
            {
                case ConsoleKey.Escape:
                    this.FireOnEscapeKeyPressed();
                    break;
                case ConsoleKey.Enter:
                    this.FireOnEnterPressed();
                    break;
                case ConsoleKey.Backspace:
                    this.FireOnDeletePressed();
                    break;
                default:
                    this.FireOnStringInput(new StringEventArgs(e.ConsoleChar.ToString()));
                    break;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnEscapeKeyPressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapeKeyPressed()
        {
            this.OnEscapeKeyPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEnterPressed"/> event.
        /// </summary>
        protected virtual void FireOnEnterPressed()
        {
            this.OnEnterPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnStringInput"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnStringInput(StringEventArgs e)
        {
            this.OnStringInput?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnDeletePressed"/> event.
        /// </summary>
        protected virtual void FireOnDeletePressed()
        {
            this.OnDeletePressed?.Invoke(this, EventArgs.Empty);
        }
    }
}