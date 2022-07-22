//-----------------------------------------------------------------------
// <copyright file="PolarCell.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    using Library.Extensions;

    /// <summary>
    /// Represents a cell in a polar grid.
    /// </summary>
    public class PolarCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarCell"/> class.
        /// </summary>
        /// <param name="row">The row at which the cell exists in the maze.</param>
        /// <param name="column">The column at which the cell exists in the maze.</param>
        public PolarCell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// Gets the row at which the cell exists in the maze.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column at which the cell exists in the maze.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets or sets the cell in the clockwise direction.
        /// </summary>
        public PolarCell? Clockwise { get; set; }

        /// <summary>
        /// Gets or sets the cell in the counter-clockwise direction.
        /// </summary>
        public PolarCell? CounterClockwise { get; set; }

        /// <summary>
        /// Gets or sets the cell in the inward direction.
        /// </summary>
        public PolarCell? Inward { get; set; }

        /// <summary>
        /// Gets the cells in the outward direction.
        /// </summary>
        public List<PolarCell> Outward { get; } = new();

        /// <inheritdoc/>
        public override PolarCell[] Neighbors
        {
            get
            {
                List<PolarCell> neighbors = new();
                neighbors.AddIfNotNull(this.Clockwise);
                neighbors.AddIfNotNull(this.CounterClockwise);
                neighbors.AddIfNotNull(this.Inward);
                neighbors.AddRange(this.Outward);

                return neighbors.ToArray();
            }
        }
    }
}
