//-----------------------------------------------------------------------
// <copyright file="CustomEncoding.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This program is a hex editor. 
// </summary>
//-----------------------------------------------------------------------
namespace ConsoleHexEditor
{
    using System.Text;

    /// <summary>
    /// The <see cref="CustomEncoding"/> class.
    /// </summary>
    public static class CustomEncoding
    {
        /// <summary>
        /// Represents the ASCII encoding.
        /// </summary>
        public static readonly Encoding ASCII = Encoding.GetEncoding(
            "us-ascii",
            new EncoderReplacementFallback("."),
            new DecoderReplacementFallback("."));

        /// <summary>
        /// Represents the utf-8 encoding.
        /// </summary>
        public static readonly Encoding UTF8 = Encoding.GetEncoding(
            "utf-8",
            new EncoderReplacementFallback("."),
            new DecoderReplacementFallback("."));

        /// <summary>
        /// Represents the windows-1250 encoding.
        /// </summary>
        public static readonly Encoding WINDOWSENC = Encoding.GetEncoding(
            "windows-1250",
            new EncoderReplacementFallback("."),
            new DecoderReplacementFallback("."));
    }
}