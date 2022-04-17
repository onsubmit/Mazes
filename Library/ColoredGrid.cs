//-----------------------------------------------------------------------
// <copyright file="ColoredGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    ///  Version of <see cref="Grid"/> which can render the distance numbers for each cell.
    /// </summary>
    public class ColoredGrid : DistanceGrid
    {
        private int maximum;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public ColoredGrid(int rows, int columns)
            : base(rows, columns)
        {
        }

        /// <summary>
        /// Gets or sets the distances.
        /// </summary>
        public override Distances? Distances
        {
            get => base.Distances;
            set
            {
                base.Distances = value;
                value?.GetCellFurthestFromRoot(out this.maximum);
            }
        }

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public override Rgba32 GetCellBackgroundColor(Cell cell)
        {
            if (this.Distances == null || !this.Distances.HasCell(cell))
            {
                return base.GetCellBackgroundColor(cell);
            }

            int distance = this.Distances[cell];
            float intensity = (float)(this.maximum - distance) / this.maximum;
            float dark = (float)Math.Round(255 * intensity) / 255;
            float bright = (128 + (float)Math.Round(127 * intensity)) / 255;

            return new Rgba32(bright, dark, dark);
        }
    }
}
