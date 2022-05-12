//-----------------------------------------------------------------------
// <copyright file="AsciiMask.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Represents a grid mask defined by an ASCII mask file.
    /// </summary>
    public class AsciiMask : Mask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsciiMask"/> class.
        /// </summary>
        /// <param name="lines">The lines from the path to the ASCII mask file.</param>
        private AsciiMask(string[] lines)
            : base(lines.Length, lines.First().Length)
        {
            bool GetInitialValues(int r, int c, out bool initialValue)
            {
                initialValue = lines[r][c] != 'X';
                return true;
            }

            this.InitializeElements(GetInitialValues);
        }

        /// <summary>
        /// Gets the mask defined by the ASCII mask in <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the ASCII mask file.</param>
        /// <returns>The new mask.</returns>
        public static AsciiMask FromFile(string path)
        {
            string[] lines = File.ReadAllLines(path).Select(l => l.Trim()).ToArray();
            return new(lines);
        }
    }
}
