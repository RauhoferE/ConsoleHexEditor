//-----------------------------------------------------------------------
// <copyright file="InputValidatorForPartialLoading.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForPartialLoading"/> class.
    /// </summary>
    public class InputValidatorForPartialLoading
    {
        /// <summary>
        /// This event fires when a valid input key has been pressed.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnNumberPressed;

        /// <summary>
        /// This event fires when the delete key has been pressed.
        /// </summary>
        public event System.EventHandler OnDeletePressed;

        /// <summary>
        /// This event fires when the escape has been pressed.
        /// </summary>
        public event System.EventHandler OnEscapePressed;

        /// <summary>
        /// This event fires when the enter button has been pressed.
        /// </summary>
        public event System.EventHandler OnEnterPressed;

        /// <summary>
        /// This event fires when the change code page button has been pressed.
        /// </summary>
        public event System.EventHandler OnChangeCodePageButtonpressed;

        /// <summary>
        /// This event fires when the go to button has been pressed.
        /// </summary>
        public event System.EventHandler OnGoToButtonPressed;

        /// <summary>
        /// This event fires when the hex view button has been pressed.
        /// </summary>
        public event System.EventHandler OnHexViewButtonPressed;

        /// <summary>
        /// This event fires when the open new file button has been pressed.
        /// </summary>
        public event System.EventHandler OnOpenNewFileButtonPressed;

        /// <summary>
        /// This event fires when the split view button has been pressed.
        /// </summary>
        public event System.EventHandler OnSplitViewButtonPressed;

        /// <summary>
        /// This event fires when the text view button has been pressed.
        /// </summary>
        public event System.EventHandler OnTextViewButtonPressed;

        /// <summary>
        /// This method gets the input from the <see cref="KeyboardWatcher"/>.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            if (e.Key == ConsoleKey.Escape)
            {
                this.FireOnEscapePressed();
            }
            else if (e.Key == ConsoleKey.Backspace)
            {
                this.FireOnDeletePressed();
            }
            else if (e.Key == ConsoleKey.Enter)
            {
                this.FireOnEnterPressed();
            }
            else if (e.Key == ConsoleKey.D0)
            {
                this.FireOnNumberPressed(new StringEventArgs("0"));
            }
            else if (e.Key == ConsoleKey.D1)
            {
                this.FireOnNumberPressed(new StringEventArgs("1"));
            }
            else if (e.Key == ConsoleKey.D2)
            {
                this.FireOnNumberPressed(new StringEventArgs("2"));
            }
            else if (e.Key == ConsoleKey.D3)
            {
                this.FireOnNumberPressed(new StringEventArgs("3"));
            }
            else if (e.Key == ConsoleKey.D4)
            {
                this.FireOnNumberPressed(new StringEventArgs("4"));
            }
            else if (e.Key == ConsoleKey.D5)
            {
                this.FireOnNumberPressed(new StringEventArgs("5"));
            }
            else if (e.Key == ConsoleKey.D6)
            {
                this.FireOnNumberPressed(new StringEventArgs("6"));
            }
            else if (e.Key == ConsoleKey.D7)
            {
                this.FireOnNumberPressed(new StringEventArgs("7"));
            }
            else if (e.Key == ConsoleKey.D8)
            {
                this.FireOnNumberPressed(new StringEventArgs("8"));
            }
            else if (e.Key == ConsoleKey.D9)
            {
                this.FireOnNumberPressed(new StringEventArgs("9"));
            }
            else if (e.Key == ConsoleKey.O && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnOpenNewFileButtonPressed();
            }
            else if (e.Key == ConsoleKey.E && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnChangeCodePageButtonpressed();
            }
            else if (e.Key == ConsoleKey.H && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnHexViewButtonPressed();
            }
            else if (e.Key == ConsoleKey.T && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnTextViewButtonPressed();
            }
            else if (e.Key == ConsoleKey.C && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnSplitViewButtonPressed();
            }
            else if (e.Key == ConsoleKey.G && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnGoToButtonPressed();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnNumberPressed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnNumberPressed(StringEventArgs e)
        {
            this.OnNumberPressed?.Invoke(this, e);
        }

        /// <summary>
        /// This method fires the <see cref="OnDeletePressed"/> event.
        /// </summary>
        protected virtual void FireOnDeletePressed()
        {
            this.OnDeletePressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEscapePressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapePressed()
        {
            this.OnEscapePressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEnterPressed"/> event.
        /// </summary>
        protected virtual void FireOnEnterPressed()
        {
            this.OnEnterPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnChangeCodePageButtonpressed"/> event.
        /// </summary>
        protected virtual void FireOnChangeCodePageButtonpressed()
        {
            this.OnChangeCodePageButtonpressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnGoToButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnGoToButtonPressed()
        {
            this.OnGoToButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnHexViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnHexViewButtonPressed()
        {
            this.OnHexViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnOpenNewFileButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnOpenNewFileButtonPressed()
        {
            this.OnOpenNewFileButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnSplitViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnSplitViewButtonPressed()
        {
            this.OnSplitViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnTextViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnTextViewButtonPressed()
        {
            this.OnTextViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}