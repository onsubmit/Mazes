// -----------------------------------------------------------------------
// <copyright file="ImageGenerationMode.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Enum for the different maze image generation modes.
    /// </summary>
    public enum ImageGenerationMode
    {
        /// <summary>
        /// Pre-calculate things.
        /// </summary>
        PreCalculate,

        /// <summary>
        /// Draw the cell background.
        /// </summary>
        Backgrounds,

        /// <summary>
        /// Draw the cell walls.
        /// </summary>
        Walls,

        /// <summary>
        /// Draw some text on a cell.
        /// </summary>
        Text,
    }
}
