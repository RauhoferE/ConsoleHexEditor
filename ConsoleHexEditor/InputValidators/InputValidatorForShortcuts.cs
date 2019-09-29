//-----------------------------------------------------------------------
// <copyright file="InputValidatorForShortcuts.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForShortcuts"/> class.
    /// </summary>
    public class InputValidatorForShortcuts
    {
        /// <summary>
        /// This event fires when the open new file button is pressed.
        /// </summary>
        public event System.EventHandler OnOpenNewFileButtonPressed;

        /// <summary>
        /// This event fires when the escape button is pressed.
        /// </summary>
        public event System.EventHandler OnEscapeButtonPressed;

        /// <summary>
        /// This method gets the input from the <see cref="KeyboardWatcher"/>.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void InterpretInput(object sender, ConsoleKeyEventArgs e)
        {
            if (e.Key == ConsoleKey.O && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnOpenNewFileButtonPressed();
            }
            else if (e.Key == ConsoleKey.Escape)
            {
                this.FireOnEscapeButtonPressed();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnOpenNewFileButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnOpenNewFileButtonPressed()
        {
            this.OnOpenNewFileButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEscapeButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapeButtonPressed()
        {
            this.OnEscapeButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}