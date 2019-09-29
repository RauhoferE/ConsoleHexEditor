//-----------------------------------------------------------------------
// <copyright file="WindowWatcher.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="WindowWatcher"/> class.
    /// </summary>
    public class WindowWatcher
    {
        /// <summary>
        /// Represents the thread.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// The window width.
        /// </summary>
        private int windowWidth;

        /// <summary>
        /// The window height.
        /// </summary>
        private int windowHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowWatcher"/> class.
        /// </summary>
        /// <param name="windowHeight"> The window height. </param>
        /// <param name="windowWidth"> The window width. </param>
        public WindowWatcher(int windowHeight, int windowWidth)
        {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;

            this.thread = new Thread(this.Worker);
            this.thread.IsBackground = true;
        }

        /// <summary>
        /// This method starts the window watcher.
        /// </summary>
        public void StartWindowWatcher()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already running.");
            }

            this.thread.Start();
        }

        /// <summary>
        /// This method starts the window watcher.
        /// </summary>
        public void StopWindowWatcher()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error thread is already dead.");
            }

            this.thread.Join();
        }

        /// <summary>
        /// The worker of the thread.
        /// </summary>
        private void Worker()
        {
            while (true)
            {
                lock (new object())
                {
                    if (Console.WindowWidth < this.windowWidth || Console.WindowHeight < this.windowHeight || Console.WindowWidth > this.windowWidth || Console.WindowHeight > this.windowHeight)
                    {
                        try
                        {
                            Console.SetWindowSize(120, 30);
                        }
                        catch
                        {
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}