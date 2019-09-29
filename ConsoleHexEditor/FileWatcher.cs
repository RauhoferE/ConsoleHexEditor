//-----------------------------------------------------------------------
// <copyright file="FileWatcher.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="FileWatcher"/> class.
    /// </summary>
    public class FileWatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileWatcher"/> class.
        /// </summary>
        /// <param name="path"> The file path. </param>
        public FileWatcher(string path)
        {
            this.FilePath = path;
        }

        /// <summary>
        /// This event fires when the file has changed.
        /// </summary>
        public event EventHandler OnFileChanged;

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string FilePath
        {
            get;
        }

        /// <summary>
        /// This method starts the <see cref="FileWatcher"/>.
        /// </summary>
        public void Run()
        {
            FileSystemWatcher fs = new FileSystemWatcher(Path.GetDirectoryName(this.FilePath));
            fs.Filter = Path.GetFileName(this.FilePath);

                    fs.NotifyFilter = NotifyFilters.LastWrite
                                      | NotifyFilters.FileName
                                      | NotifyFilters.DirectoryName;

                    fs.Changed += this.FileChanged;
                    fs.Created += this.FileChanged;
                    fs.Deleted += this.FileChanged;
                    fs.Renamed += this.FileChanged;
                    fs.EnableRaisingEvents = true;
        }

        /// <summary>
        /// This method fires the <see cref="OnFileChanged"/> event.
        /// </summary>
        protected virtual void FireOnFileChanged()
        {
            this.OnFileChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// This method is called when the file has been changed.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="FileSystemWatcher"/>. </param>
        private void FileChanged(object sender, FileSystemEventArgs e)
        {
            this.FireOnFileChanged();
        }
    }
}