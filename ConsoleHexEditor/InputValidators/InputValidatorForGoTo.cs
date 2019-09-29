//-----------------------------------------------------------------------
// <copyright file="InputValidatorForGoTo.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForGoTo"/> class.
    /// </summary>
    public class InputValidatorForGoTo
    {
        /// <summary>
        /// This event fires when the goto menu is pressed.
        /// </summary>
        public event System.EventHandler OnGoToPressed;

        /// <summary>
        /// This event fires when a valid hex button is pressed.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnHexKeyPressed;

        /// <summary>
        /// This event fires when the delete button is pressed.
        /// </summary>
        public event System.EventHandler OnDeletePressed;

        /// <summary>
        /// This event fires when the escape button is pressed.
        /// </summary>
        public event System.EventHandler OnEscapePressed;

        /// <summary>
        /// This event fires when the change code page button is pressed.
        /// </summary>
        public event System.EventHandler OnChangeCodePageButtonpressed;

        /// <summary>
        /// This event fires when the hex view button is pressed.
        /// </summary>
        public event System.EventHandler OnHexViewButtonPressed;

        /// <summary>
        /// This event fires when the open new file button is pressed.
        /// </summary>
        public event System.EventHandler OnOpenNewFileButtonPressed;

        /// <summary>
        /// This event fires when the partial loading button is pressed.
        /// </summary>
        public event System.EventHandler OnPartialLoadingButtonpressed;

        /// <summary>
        /// This event fires when the split view button is pressed.
        /// </summary>
        public event System.EventHandler OnSplitViewButtonPressed;

        /// <summary>
        /// This event fires when the text view button is pressed.
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
                this.FireOnGoToPressed();
            }
            else if (e.Key == ConsoleKey.D0)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("0"));
            }
            else if (e.Key == ConsoleKey.D1)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("1"));
            }
            else if (e.Key == ConsoleKey.D2)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("2"));
            }
            else if (e.Key == ConsoleKey.D3)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("3"));
            }
            else if (e.Key == ConsoleKey.D4)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("4"));
            }
            else if (e.Key == ConsoleKey.D5)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("5"));
            }
            else if (e.Key == ConsoleKey.D6)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("6"));
            }
            else if (e.Key == ConsoleKey.D7)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("7"));
            }
            else if (e.Key == ConsoleKey.D8)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("8"));
            }
            else if (e.Key == ConsoleKey.D9)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("9"));
            }
            else if (e.Key == ConsoleKey.A)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("A"));
            }
            else if (e.Key == ConsoleKey.B)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("B"));
            }
            else if (e.Key == ConsoleKey.C)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("C"));
            }
            else if (e.Key == ConsoleKey.D)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("D"));
            }
            else if (e.Key == ConsoleKey.E)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("E"));
            }
            else if (e.Key == ConsoleKey.F)
            {
                this.FireOnHexKeyPressed(new StringEventArgs("F"));
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
            else if (e.Key == ConsoleKey.P && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnPartialLoadingButtonpressed();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnGoToPressed"/> event.
        /// </summary>
        protected virtual void FireOnGoToPressed()
        {
            this.OnGoToPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnHexViewButtonPressed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnHexKeyPressed(StringEventArgs e)
        {
            this.OnHexKeyPressed?.Invoke(this, e);
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
        /// This method fires the <see cref="OnChangeCodePageButtonpressed"/> event.
        /// </summary>
        protected virtual void FireOnChangeCodePageButtonpressed()
        {
            this.OnChangeCodePageButtonpressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnTextViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnTextViewButtonPressed()
        {
            this.OnTextViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnSplitViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnSplitViewButtonPressed()
        {
            this.OnSplitViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnPartialLoadingButtonpressed"/> event.
        /// </summary>
        protected virtual void FireOnPartialLoadingButtonpressed()
        {
            this.OnPartialLoadingButtonpressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnOpenNewFileButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnOpenNewFileButtonPressed()
        {
            this.OnOpenNewFileButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnHexViewButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnHexViewButtonPressed()
        {
            this.OnHexViewButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}