//-----------------------------------------------------------------------
// <copyright file="InputValidatorForTextMode.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForTextMode"/> class.
    /// </summary>
    public class InputValidatorForTextMode
    {
        /// <summary>
        /// This event fires when a valid input button is pressed.
        /// </summary>
        public event System.EventHandler<ConsoleKeyEventArgs> OnHexKeyReceived;

        /// <summary>
        /// This event fires when the escape button is pressed.
        /// </summary>
        public event System.EventHandler OnEscapeButtonPressed;

        /// <summary>
        /// This event fires when the next position button is pressed.
        /// </summary>
        public event System.EventHandler OnKeyForNextPositionPressed;

        /// <summary>
        /// This event fires when the previous position button is pressed.
        /// </summary>
        public event System.EventHandler OnKeyForPreviousPositionPressed;

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
                    this.FireOnEscapeButtonPressed();
                    break;
                case ConsoleKey.RightArrow:
                    this.FireOnKeyForNextPositionPressed();
                    break;
                case ConsoleKey.LeftArrow:
                    this.FireOnKeyForPreviousPositionPressed();
                    break;
                case ConsoleKey.D0:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D1:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D2:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D3:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D4:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D5:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D6:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D7:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D8:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D9:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.A:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.B:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.C:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.D:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.E:
                    this.FireOnHexKeyReceived(e);
                    break;
                case ConsoleKey.F:
                    this.FireOnHexKeyReceived(e);
                    break;
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnHexKeyReceived"/> event.
        /// </summary>
        /// <param name="e">A <see cref="ConsoleKeyEventArgs"/>. </param>> 
        protected virtual void FireOnHexKeyReceived(ConsoleKeyEventArgs e)
        {
            this.OnHexKeyReceived?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnEscapeButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapeButtonPressed()
        {
            this.OnEscapeButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyForNextPositionPressed"/> event.
        /// </summary>
        protected virtual void FireOnKeyForNextPositionPressed()
        {
            this.OnKeyForNextPositionPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyForPreviousPositionPressed"/> event.
        /// </summary>
        protected virtual void FireOnKeyForPreviousPositionPressed()
        {
            this.OnKeyForPreviousPositionPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}