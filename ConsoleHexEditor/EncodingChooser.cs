//-----------------------------------------------------------------------
// <copyright file="EncodingChooser.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="EncodingChooser"/> class.
    /// </summary>
    public class EncodingChooser
    {
        /// <summary>
        /// This event fires when a message should be printed.
        /// </summary>
        public event System.EventHandler<StringEventArgs> OnMessagePrint;

        /// <summary>
        /// This event fires when a encoding has been chosen.
        /// </summary>
        public event System.EventHandler<CustomEncodingEventArgs> OnEncodingChoosen;

        /// <summary>
        /// This method sets the encoding to utf-8.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SetConsoleEncodingToUTF8(object sender, EventArgs e)
        {
            this.FireOnEncodingChoosen(new CustomEncodingEventArgs(CustomEncoding.UTF8));
            this.FireOnMessagePrint(new StringEventArgs("UTF8 choosen"));
        }

        /// <summary>
        /// This method sets the encoding to ASCII.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SetConsoleEncodingToASCII(object sender, EventArgs e)
        {
            this.FireOnEncodingChoosen(new CustomEncodingEventArgs(CustomEncoding.ASCII));
            this.FireOnMessagePrint(new StringEventArgs("ASCII choosen"));
        }

        /// <summary>
        /// This method sets the encoding to windows-1250.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="EventArgs"/>. </param>
        public void SetConsoleEncodingToWindows(object sender, EventArgs e)
        {
            this.FireOnEncodingChoosen(new CustomEncodingEventArgs(CustomEncoding.WINDOWSENC));
            this.FireOnMessagePrint(new StringEventArgs("Windows-1250 choosen"));
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
        /// This method fires the <see cref="OnEncodingChoosen"/> event.
        /// </summary>
        /// <param name="e">A <see cref="CustomEncodingEventArgs"/>. </param>> 
        protected virtual void FireOnEncodingChoosen(CustomEncodingEventArgs e)
        {
            this.OnEncodingChoosen?.Invoke(this, e);
        }
    }
}