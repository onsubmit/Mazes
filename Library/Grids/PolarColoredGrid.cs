//-----------------------------------------------------------------------
// <copyright file="PolarColoredGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Version of <see cref="PolarDistanceGrid"/> but distances are portrayed via color.
    /// </summary>
    public class PolarColoredGrid : PolarDistanceGrid
    {
        private int maximum;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarColoredGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        public PolarColoredGrid(int rows)
            : base(rows)
        {
            if (!this.GetType().IsSubclassOf(typeof(PolarColoredGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the distances.
        /// </summary>
        public override Distances<PolarCell>? Distances
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
        public override Rgba32 GetCellBackgroundColor(PolarCell cell)
        {
            if (this.Distances == null || !this.Distances.HasCell(cell))
            {
                return base.GetCellBackgroundColor(cell);
            }

            int distance = this.Distances[cell];
            float intensity = (float)(this.maximum - distance) / this.maximum;
            float dark = (float)Math.Round(255 * intensity) / 255;
            float bright = (128 + (float)Math.Round(127 * intensity)) / 255;

            return new Rgba32(bright / 4, bright / 2, bright);
        }
    }
}
