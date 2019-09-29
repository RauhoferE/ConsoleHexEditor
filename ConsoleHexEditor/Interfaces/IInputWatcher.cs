//-----------------------------------------------------------------------
// <copyright file="IInputWatcher.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    /// <summary>
    /// The <see cref="IInputWatcher"/> interface.
    /// </summary>
    public interface IInputWatcher
    {
        /// <summary>
        /// This event fires when an input has been received.
        /// </summary>
        event System.EventHandler<ConsoleKeyEventArgs> OnInputReceived;

        /// <summary>
        /// Gets a value indicating whether the watcher is running or not.
        /// </summary>
        /// <value> Is true if the thread is running. </value>
        bool IsRunning { get; }

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        void Start();

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        void Stop();

        /// <summary>
        /// This is the main worker.
        /// </summary>
        void Worker();
    }
}