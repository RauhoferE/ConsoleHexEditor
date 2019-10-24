//-----------------------------------------------------------------------
// <copyright file="ConsoleRenderer.cs" company="FH Wiener Neustadt">
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
    using System.Linq;

    /// <summary>
    /// The <see cref="ConsoleRenderer"/> class.
    /// </summary>
    public class ConsoleRenderer : IRenderer
    {
        /// <summary>
        /// Represents the window width.
        /// </summary>
        private int windowWidth;

        /// <summary>
        /// Represents the window height.
        /// </summary>
        private int windowHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer"/> class.
        /// </summary>
        /// <param name="windowWith"> The window width. </param>
        /// <param name="windowHeight"> The window height. </param>
        public ConsoleRenderer(int windowWith, int windowHeight)
        {
            Console.OutputEncoding = CustomEncoding.UTF8;
            this.WindowHeight = windowHeight;
            this.windowWidth = windowWith;
        }

        /// <summary>
        /// Gets the window width.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        public int WindowWith
        {
            get
            {
                return this.windowWidth;
            }

            private set
            {
                if (value < 0 || value > Console.LargestWindowWidth)
                {
                    throw new ArgumentException("Error value cant be smaller than zero.");
                }

                this.windowWidth = value;
            }
        }

        /// <summary>
        /// Gets the window height.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        public int WindowHeight
        {
            get
            {
                return this.windowHeight;
            }

            private set
            {
                if (value < 0 || value > Console.LargestWindowHeight)
                {
                    throw new ArgumentException("Error value cant be smaller than zero.");
                }

                this.windowHeight = value;
            }
        }

        /// <summary>
        /// Gets the length of the offset.
        /// </summary>
        /// <value> A normal <see cref="int"/>. </value>
        public int LengthOfPositionvalue
        {
            get;
            private set;
        }

        /// <summary>
        /// Changes the console encoding.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="CustomEncodingEventArgs"/>. </param>
        public void ChangeEncoding(object sender, CustomEncodingEventArgs e)
        {
            Console.OutputEncoding = e.Current;
        }

        /// <summary>
        /// Changes the length of the offset.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="LongEventArgs"/>. </param>
        public void ChangeLengthOfPositionValue(object sender, LongEventArgs e)
        {
            this.LengthOfPositionvalue = e.Value.ToString("X").Length;
        }

        /// <summary>
        /// Renders the hex view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        public void RenderHexView(object sender, PageEventArgs page)
        {
            Console.Clear();
            int i = 0;
            foreach (var hexElement in page.Page.HexElements)
            {
                foreach (var hexSegment in hexElement.HexSegment)
                {
                    if (i % 32 == 0)
                    {
                        Console.WriteLine();
                        Console.Write(hexSegment.Position.ToString("x" + this.LengthOfPositionvalue.ToString()));
                    }

                    if (i % 16 == 0)
                    {
                        Console.Write("    ");
                    }

                    if (i % 2 == 0)
                    {
                        Console.Write(" ");
                    }

                    if (i % 8 == 0)
                    {
                        Console.Write("    ");
                    }

                    Console.Write(hexSegment.Number);
                    i++;
                }
            }
        }

        /// <summary>
        /// Renders the text view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        public void RenderTextView(object sender, PageEventArgs page)
        {
            Console.Clear();
            int i = 0;
            foreach (var textElement in page.Page.HexElements)
            {
                if (i % 100 == 0 && i != 0)
                {
                    Console.WriteLine();
                }

                Console.Write(textElement.TextSegment);
                i++;
            }
        }

        /// <summary>
        /// Renders the split view.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        public void RenderSplitView(object sender, PageEventArgs page)
        {
            Console.Clear();
            int i = 0;

            List<HexElement> temp = new List<HexElement>();
            foreach (var hexElement in page.Page.HexElements)
            {
                foreach (var hexSegment in hexElement.HexSegment)
                {
                    if (i % 16 == 0)
                    {
                        Console.Write("    ");

                        foreach (var hex in temp)
                        {
                            int convertedValue;
                            bool b = int.TryParse(hex.HexSegment.FirstOrDefault().Number, System.Globalization.NumberStyles.HexNumber, null, out convertedValue);

                            if (hex.HexSegment.Count == 1 && convertedValue <= 31)
                            {
                                Console.Write(".");
                            }
                            else
                            {
                                Console.Write(hex.TextSegment);
                            }
                        }

                        Console.WriteLine();
                        Console.Write(hexSegment.Position.ToString("x" + this.LengthOfPositionvalue.ToString()));
                        temp.Clear();
                    }

                    if (i % 2 == 0)
                    {
                        Console.Write(" ");
                    }

                    if (i % 8 == 0)
                    {
                        Console.Write("    ");
                    }

                    Console.Write(hexSegment.Number);
                    i++;
                }

                temp.Add(hexElement);
            }

            Console.Write("    ");

            foreach (var hex in temp)
            {
                int convertedValue;
                bool b = int.TryParse(hex.HexSegment.FirstOrDefault().Number, System.Globalization.NumberStyles.HexNumber, null, out convertedValue);

                if (hex.HexSegment.Count == 1 && convertedValue <= 31)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(hex.TextSegment);
                }
            }

            temp.Clear();
        }

        /// <summary>
        /// Prints a normal message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void PrintMessage(object sender, StringEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e.Text);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints an error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void PrintErrorMessage(object sender, StringEventArgs e)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Text);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints a page and colors the position.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        public void PrintSplitViewAndColorPosition(object sender, PageAndPositionEventArgs page)
        {
            Console.Clear();
            int i = 0;

            List<HexElement> temp = new List<HexElement>();
            foreach (var hexElement in page.Page.HexElements)
            {
                foreach (var hexSegment in hexElement.HexSegment)
                {
                    if (i % 16 == 0)
                    {
                        Console.Write("    ");

                        foreach (var hex in temp)
                        {
                            int convertedValue;
                            bool b = int.TryParse(hex.HexSegment.FirstOrDefault().Number, System.Globalization.NumberStyles.HexNumber, null, out convertedValue);

                            if (hex.HexSegment.Count == 1 && convertedValue <= 31)
                            {
                                Console.Write(".");
                            }
                            else
                            {
                                Console.Write(hex.TextSegment);
                            }
                        }

                        Console.WriteLine();
                        Console.Write(hexSegment.Position.ToString("x" + this.LengthOfPositionvalue.ToString()));
                        temp.Clear();
                    }

                    if (i % 2 == 0)
                    {
                        Console.Write(" ");
                    }

                    if (i % 8 == 0)
                    {
                        Console.Write("    ");
                    }

                    if (hexSegment.Position == page.PositionToMark)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(hexSegment.Number);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(hexSegment.Number);
                    }
                    
                    i++;
                }

                temp.Add(hexElement);
            }

            Console.Write("    ");

            foreach (var hex in temp)
            {
                int convertedValue;
                bool b = int.TryParse(hex.HexSegment.FirstOrDefault().Number, System.Globalization.NumberStyles.HexNumber, null, out convertedValue);

                if (hex.HexSegment.Count == 1 && convertedValue <= 31)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(hex.TextSegment);
                }
            }

            temp.Clear();
        }

        /// <summary>
        /// Prints the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void PrintUserInput(object sender, StringEventArgs e)
        {
            Console.Write(e.Text);
        }

        /// <summary>
        /// Prints a page an marks the current position of the cursor.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="page"> The <see cref="PageEventArgs"/>. </param>
        public void PrintPageToEdit(object sender, PageAndPositionEventArgs page)
        {
            Console.Clear();
            int i = 0;
            long positionTemp = 0;
            var cursorPosTop = Console.CursorTop;
            var cursorPosLeft = Console.CursorLeft;

            foreach (var hexElement in page.Page.HexElements)
            {
                foreach (var hexSegment in hexElement.HexSegment)
                {
                    if (i % 32 == 0)
                    {
                        Console.WriteLine();
                        Console.Write(hexSegment.Position.ToString("x" + this.LengthOfPositionvalue));
                    }

                    if (i % 16 == 0)
                    {
                        Console.Write("    ");
                    }

                    if (i % 2 == 0)
                    {
                        Console.Write(" ");
                    }

                    if (i % 8 == 0)
                    {
                        Console.Write("    ");
                    }

                    foreach (var character in hexSegment.Number)
                    {
                        if (positionTemp == page.PositionToMark)
                        {
                            cursorPosTop = Console.CursorTop;
                            cursorPosLeft = Console.CursorLeft;
                            Console.BackgroundColor = ConsoleColor.Green;
                        }

                        positionTemp++;
                        Console.Write(character);
                        Console.ResetColor();
                    }
                    
                    i++;
                }
            }

            Console.SetCursorPosition(cursorPosLeft, cursorPosTop);
        }

        /// <summary>
        /// Deletes the user input.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The <see cref="StringEventArgs"/>. </param>
        public void DeleteUserInput(object sender, EventArgs e)
        {
            if (Console.CursorLeft == 0 && Console.CursorTop > 1)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop - 1);
                Console.Write(" ");
                Console.SetCursorPosition(Console.WindowWidth - 1, Console.CursorTop - 1);
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write(" ");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }
}