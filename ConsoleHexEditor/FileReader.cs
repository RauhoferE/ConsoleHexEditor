//-----------------------------------------------------------------------
// <copyright file="FileReader.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The <see cref="FileReader"/> class.
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// The current index.
        /// </summary>
        private long index;

        /// <summary>
        /// The first index of the current page.
        /// </summary>
        private long lastIndex;

        /// <summary>
        /// The page size.
        /// </summary>
        private int pagesize;

        /// <summary>
        /// The path of the folder.
        /// </summary>
        private string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReader"/> class.
        /// </summary>
        /// <param name="path"> The file path. </param>
        public FileReader(string path)
        {
            this.path = path;
            this.index = 0;
            this.pagesize = 10;
            this.lastIndex = 0;
        }

        /// <summary>
        /// This event fires when a error message should be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// Gets the page size.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        public int PageSize
        {
            get
            {
                return this.pagesize;
            }

            private set
            {
                this.pagesize = value;
            }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value> A normal <see cref="string"/>. </value>
        public string Path
        {
            get
            {
                return this.path;
            }

            private set
            {
                this.path = value;
            }
        }

        /// <summary>
        /// This method reads in the next bytes from the file.
        /// </summary>
        /// <returns> It returns a byte array. </returns>
        public byte[] GetNextCharactersFromFile()
        {
            try
            {
                byte[] readIn = new byte[this.pagesize * 1000000];

                using (FileStream sr = new FileStream(this.Path, FileMode.Open))
                {
                    sr.Seek(this.index, SeekOrigin.Begin);
                    sr.Read(readIn, 0, readIn.Length);
                }

                this.lastIndex = this.index;
                this.index = this.index + readIn.Length;

                List<byte> list = readIn.ToList();
                list.RemoveAll(x => x.Equals(0));
                return list.ToArray();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: " + e.Message);
            }
        }

        /// <summary>
        /// This method writes the buffer in the file..
        /// </summary>
        /// <param name="buffer"> The buffer to be written in. </param>
        /// <returns> It returns a byte array. </returns>
        public byte[] SaveChangesToFileAndReturnNewBuffer(byte[] buffer)
        {
            try
            {
                byte[] readIn = new byte[this.pagesize * 1000000];

                using (FileStream sr = new FileStream(this.Path, FileMode.Open))
                {
                    sr.Seek(this.lastIndex, SeekOrigin.Begin);
                    sr.Write(buffer, 0, buffer.Length);
                    sr.Seek(this.lastIndex, SeekOrigin.Begin);
                    sr.Read(readIn, 0, readIn.Length);
                }

                List<byte> list = readIn.ToList();
                list.RemoveAll(x => x.Equals(0));
                return list.ToArray();
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: " + e.Message);
            }
        }

        /// <summary>
        /// This method sets the page size.
        /// </summary>
        /// <param name="size"> The size of the page. </param>
        public void SetPageSize(int size)
        {
            if (size < 1 || size > 50)
            {
                this.FireOnErrorMessagePrint(new StringEventArgs("Error value has to be between 1 and 50."));
            }

            this.pagesize = size;
        }

        /// <summary>
        /// This method resets the index.
        /// </summary>
        public void ResetIndex()
        {
            this.index = 0;
        }

        /// <summary>
        /// This method fires the <see cref="OnErrorMessagePrint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="StringEventArgs"/>. </param>> 
        protected virtual void FireOnErrorMessagePrint(StringEventArgs e)
        {
            this.OnErrorMessagePrint?.Invoke(this, e);
        }
    }
}