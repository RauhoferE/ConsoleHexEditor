//-----------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="FH Wiener Neustadt">
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
    using System.IO;

    /// <summary>
    /// The <see cref="FileHelper"/> class.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// This method checks if the file exists.
        /// </summary>
        /// <param name="path"> The file path. </param>
        /// <returns> It returns true if the file exists. </returns>
        public static bool CheckIfFileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// This method gets the byte count of the file.
        /// </summary>
        /// <param name="path"> The file path. </param>
        /// <returns> It returns the number of bytes of the file. </returns>
        public static int GetMaxCharacterOfFile(string path)
        {
            try
            {
                int count = 0;

                using (FileStream br = new FileStream(path, FileMode.Open))
                {
                    while (br.ReadByte() > 0)
                    {
                        count++;
                    }
                }

                return count;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: " + e.Message);
            }
        }

        /// <summary>
        /// This method returns the absolute path of the file.
        /// </summary>
        /// <param name="path"> The file path. </param>
        /// <returns> It returns the absolute path. </returns>
        public static string ReturnAbsolutePath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                return path;
            }

            return Directory.GetCurrentDirectory() + @"\" + path;
        }
    }
}