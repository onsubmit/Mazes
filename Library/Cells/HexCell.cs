//-----------------------------------------------------------------------
// <copyright file="HexCell.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    using Library.Extensions;

    /// <summary>
    /// Represents a hexagonal cell.
    /// </summary>
    public class HexCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HexCell"/> class.
        /// </summary>
        /// <param name="row">The row at which the cell exists in the maze.</param>
        /// <param name="column">The column at which the cell exists in the maze.</param>
        public HexCell(int row, int column)
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
        public CardinalCells<HexCell> CardinalCells { get; } = new();

        /// <summary>
        /// Gets the potential neighboring cells in the ordinal directions.
        /// </summary>
        public OrdinalCells<HexCell> OrdinalCells { get; } = new();

        /// <inheritdoc/>
        public override HexCell[] Neighbors
        {
            get
            {
                List<HexCell> neighbors = new();
                neighbors.AddIfNotNull(this.OrdinalCells.NorthWest);
                neighbors.AddIfNotNull(this.CardinalCells.North);
                neighbors.AddIfNotNull(this.OrdinalCells.NorthEast);
                neighbors.AddIfNotNull(this.OrdinalCells.SouthWest);
                neighbors.AddIfNotNull(this.CardinalCells.South);
                neighbors.AddIfNotNull(this.OrdinalCells.SouthEast);

                return neighbors.ToArray();
            }
        }
    }
}
