//-----------------------------------------------------------------------
// <copyright file="ApplicationParamsParser.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="ApplicationParamsParser"/> class.
    /// </summary>
    public class ApplicationParamsParser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationParamsParser"/> class.
        /// </summary>
        public ApplicationParamsParser()
        {
            this.FileName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        /// <value> A normal string value. </value>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the file name is set or not.
        /// </summary>
        /// <value> A  normal <see cref="bool"/>. </value>
        public bool IsFileNameSet
        {
            get;
            set;
        }

        /// <summary>
        /// This method Parses the arguments into the parser class.
        /// </summary>
        /// <param name="args"> The string arguments. </param>
        /// <returns> It returns a class where the values have been parsed. </returns>
        public static ApplicationParamsParser Parse(string[] args)
        {
            ApplicationParamsParser applicationSettings = new ApplicationParamsParser();

            if (args.Length == 0)
            {
                return applicationSettings;
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-file":
                        applicationSettings.FileName = applicationSettings.CheckFile(args[i + 1]);
                        i++;
                        break;
                    default:
                        throw new ArgumentException("Error parameter is unknown: " + args[i]);
                }
            }

            if (applicationSettings.FileName == string.Empty)
            {
                applicationSettings.IsFileNameSet = false;
            }

            return applicationSettings;
        }

        /// <summary>
        /// This method checks if the file exists.
        /// </summary>
        /// <param name="filename"> The file name. </param>
        /// <returns> It returns the file name. </returns>
        public string CheckFile(string filename)
        {
            if (!FileHelper.CheckIfFileExists(filename))
            {
                throw new ArgumentException("Error path is wrong.");
            }

            this.IsFileNameSet = true;
            return filename;
        }
    }
}