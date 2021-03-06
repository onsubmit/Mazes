//-----------------------------------------------------------------------
// <copyright file="CartesianCell.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    using Library.Extensions;

    /// <summary>
    /// Represents a maze cell in a Cartesian grid.
    /// </summary>
    public class CartesianCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianCell"/> class.
        /// </summary>
        /// <param name="row">The row at which the cell exists in the maze.</param>
        /// <param name="column">The column at which the cell exists in the maze.</param>
        public CartesianCell(int row, int column)
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
        /// Gets the potential neighboring cells in the cardinal directions.
        /// </summary>
        public CardinalCells<CartesianCell> CardinalCells { get; } = new();

        /// <inheritdoc/>
        public override CartesianCell[] Neighbors
        {
            get
            {
                List<CartesianCell> neighbors = new();

                neighbors.AddIfNotNull(this.CardinalCells.North);
                neighbors.AddIfNotNull(this.CardinalCells.South);
                neighbors.AddIfNotNull(this.CardinalCells.East);
                neighbors.AddIfNotNull(this.CardinalCells.West);

                return neighbors.ToArray();
            }
        }

        /// <summary>
        /// Generates a string repsentation of the cell.
        /// </summary>
        /// <returns>A string repsentation of the cell.</returns>
        public override string ToString()
        {
            return $"({this.Row}, {this.Column})";
        }
    }
}
