//-----------------------------------------------------------------------
// <copyright file="InputValidatorForNormalView.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="InputValidatorForNormalView"/> class.
    /// </summary>
    public class InputValidatorForNormalView
    {
        /// <summary>
        /// This event fires when the get next page button is pressed.
        /// </summary>
        public event System.EventHandler OnGetNextPageOutOfBufferKey;

        /// <summary>
        /// This event fires when the get previous page button is pressed.
        /// </summary>
        public event System.EventHandler OnGetPreviousPageOutOfBufferKey;

        /// <summary>
        /// This event fires when the change code page button is pressed.
        /// </summary>
        public event System.EventHandler OnChangeCodePageButtonpressed;

        /// <summary>
        /// This event fires when the edit page button is pressed.
        /// </summary>
        public event System.EventHandler OnEditButtonPressed;

        /// <summary>
        /// This event fires when the go to button is pressed.
        /// </summary>
        public event System.EventHandler OnGoToButtonPressed;

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
        /// This event fires when the escape button is pressed.
        /// </summary>
        public event System.EventHandler OnEscapeButtonPressed;

        /// <summary>
        /// This method gets the input from the <see cref="KeyboardWatcher"/>.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        public void GetInput(object sender, ConsoleKeyEventArgs e)
        {
            if (e.Key == ConsoleKey.Escape)
            {
                this.FireOnEscapeButtonPressed();
            }
            else if (e.Key == ConsoleKey.PageUp)
            {
                this.FireOnGetNextPageOutOfBufferKey();
            }
            else if (e.Key == ConsoleKey.PageDown)
            {
                this.FireOnGetPreviousPageOutOfBufferKey();
            }
            else if (e.Key == ConsoleKey.E && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnChangeCodePageButtonpressed();
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
            else if (e.Key == ConsoleKey.W && e.Modifier == ConsoleModifiers.Control)
            {
                this.FireOnEditButtonPressed();
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnGetNextPageOutOfBufferKey"/> event.
        /// </summary>
        protected virtual void FireOnGetNextPageOutOfBufferKey()
        {
            this.OnGetNextPageOutOfBufferKey?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnGetPreviousPageOutOfBufferKey"/> event.
        /// </summary>
        protected virtual void FireOnGetPreviousPageOutOfBufferKey()
        {
            this.OnGetPreviousPageOutOfBufferKey?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnChangeCodePageButtonpressed"/> event.
        /// </summary>
        protected virtual void FireOnChangeCodePageButtonpressed()
        {
            this.OnChangeCodePageButtonpressed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method fires the <see cref="OnEditButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnEditButtonPressed()
        {
            this.OnEditButtonPressed?.Invoke(this, EventArgs.Empty);
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
        /// This method fires the <see cref="OnPartialLoadingButtonpressed"/> event.
        /// </summary>
        protected virtual void FireOnPartialLoadingButtonpressed()
        {
            this.OnPartialLoadingButtonpressed?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// This method fires the <see cref="OnEscapeButtonPressed"/> event.
        /// </summary>
        protected virtual void FireOnEscapeButtonPressed()
        {
            this.OnEscapeButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}