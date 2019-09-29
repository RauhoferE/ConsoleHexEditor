//-----------------------------------------------------------------------
// <copyright file="KeyboardWatcher.cs" company="FH Wiener Neustadt">
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
    using System.Threading;

    /// <summary>
    /// The <see cref="KeyboardWatcher"/> class.
    /// </summary>
    public class KeyboardWatcher : IInputWatcher
    {
        /// <summary>
        /// Represents the watcher thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardWatcher"/> class.
        /// </summary>
        public KeyboardWatcher()
        {
            this.thread = new Thread(this.Worker);
            this.thread.IsBackground = true;
        }

        /// <summary>
        /// This event fires when an input is received.
        /// </summary>
        public event System.EventHandler<ConsoleKeyEventArgs> OnInputReceived;

        /// <summary>
        /// Gets a value indicating whether the thread is running or not.
        /// </summary>
        /// <value> The value is true if the thread is running. </value>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        public void Start()
        {
            if (this.thread.IsAlive && this.thread != null)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.thread.Start();
            this.IsRunning = true;
        }

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// This is the worker of the watcher.
        /// </summary>
        public void Worker()
        {
            while (this.IsRunning)
            {
                var key = Console.ReadKey(true);
                this.FireOnKeyReceived(new ConsoleKeyEventArgs(key.Key, key.Modifiers, key.KeyChar));
            }
        }

        /// <summary>
        /// This method fires the <see cref="OnInputReceived"/> event.
        /// </summary>
        /// <param name="e"> The <see cref="ConsoleKeyEventArgs"/>. </param>
        protected virtual void FireOnKeyReceived(ConsoleKeyEventArgs e)
        {
            this.OnInputReceived?.Invoke(this, e);
        }
    }
}