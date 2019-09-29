//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="Program"/> class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry of the application.
        /// </summary>
        /// <param name="args"> Console arguments. </param>
        public static void Main(string[] args)
        {
            ApplicationParamsParser application;
            IRenderer renderer = new ConsoleRenderer(120, 30);

            try
            {
                application = ApplicationParamsParser.Parse(args);
                Application app = new Application(application, renderer, new KeyboardWatcher());
                app.Start();
            }
            catch (Exception e)
            {
                renderer.PrintErrorMessage(null, new StringEventArgs(e.Message));
                Environment.Exit(1);
            }
        }
    }
}
