//-----------------------------------------------------------------------
// <copyright file="InputValidatorForChangingCodePage.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForChangingCodePage"/> class.
    /// </summary>
    public class InputValidatorForChangingCodePage
    {
        /// <summary>
        /// This event fires when the key for utf-8 is pressed.
        /// </summary>
        public event System.EventHandler OnKeyForUTF8Pressed;

        /// <summary>
        /// This event fires when the key for ASCII is pressed.
        /// </summary>
        public event System.EventHandler OnKeyForAsciiPressed;

        /// <summary>
        /// This event fires when the key for windows-1250 is pressed.
        /// </summary>
        public event System.EventHandler OnKeyForWindowsPressed;

        /// <summary>
        /// This event fires when the key for escape is pressed.
        /// </summary>
        public event System.EventHandler OnEscapePressed;

        /// <summary>
        /// This event fires when the key for go to menu is pressed.
        /// </summary>
        public event System.EventHandler OnGoToButtonPressed;

        /// <summary>
        /// This event fires when the key for get current hex view is pressed.
        /// </summary>
        public event System.EventHandler OnHexViewButtonPressed;

        /// <summary>
        /// This event fires when the key for open new file menu is pressed.
        /// </summary>
        public event System.EventHandler OnOpenNewFileButtonPressed;

        /// <summary>
        /// This event fires when the key for partial loading menu is pressed.
        /// </summary>
        public event System.EventHandler OnPartialLoadingButtonpressed;

        /// <summary>
        /// This event fires when the key for get current split view is pressed.
        /// </summary>
        public event System.EventHandler OnSplitViewButtonPressed;

        /// <summary>
        /// This event fires when the key for get current text view is pressed.
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
            else if (e.Key == ConsoleKey.D1)
            {
                this.FireOnKeyForUtf8Pressed(); 
            }
            else if (e.Key == ConsoleKey.D2)
            {
                this.FireOnKeyForAsciiPressed();
            }
            else if (e.Key == ConsoleKey.D3)
            {
                this.FireOnKeyForWindowsPressed();
            }
            else if (e.Key == ConsoleKey.O && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnOpenNewFileButtonPressed();
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
            else if (e.Key == ConsoleKey.G && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnGoToButtonPressed();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyForUTF8Pressed"/> event.
        /// </summary>
        protected virtual void FireOnKeyForUtf8Pressed()
        {
            this.OnKeyForUTF8Pressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyForAsciiPressed"/> event.
        /// </summary>
        protected virtual void FireOnKeyForAsciiPressed()
        {
            this.OnKeyForAsciiPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnKeyForWindowsPressed"/> event.
        /// </summary>
        protected virtual void FireOnKeyForWindowsPressed()
        {
            this.OnKeyForWindowsPressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEscapePressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapePressed()
        {
            this.OnEscapePressed?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// This method fires the <see cref="OnGoToButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnGoToButtonPressed()
        {
            this.OnGoToButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}