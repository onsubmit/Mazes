//-----------------------------------------------------------------------
// <copyright file="Cell.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using Library.Extensions;

    /// <summary>
    /// Represents a maze cell.
    /// </summary>
    public class Cell
    {
        private readonly Dictionary<Cell, bool> links = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">The row at which the cell exists in the maze.</param>
        /// <param name="column">The column at which the cell exists in the maze.</param>
        public Cell(int row, int column)
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
        /// Gets or sets the northern neighbor.
        /// </summary>
        public Cell? North { get; set; }

        /// <summary>
        /// Gets or sets the southern neighbor.
        /// </summary>
        public Cell? South { get; set; }

        /// <summary>
        /// Gets or sets the eastern neighbord.
        /// </summary>
        public Cell? East { get; set; }

        /// <summary>
        /// Gets or sets the western neighbor.
        /// </summary>
        public Cell? West { get; set; }

        /// <summary>
        /// Gets the linked cells.
        /// </summary>
        public Cell[] Links => this.links.Keys.ToArray();

        /// <summary>
        /// Gets all the neighboring cells.
        /// </summary>
        public Cell[] Neighbors
        {
            get
            {
                List<Cell> neighbors = new();

                neighbors.AddIfNotNull(this.North);
                neighbors.AddIfNotNull(this.South);
                neighbors.AddIfNotNull(this.East);
                neighbors.AddIfNotNull(this.West);

                return neighbors.ToArray();
            }
        }

        /// <summary>
        /// Links a cell.
        /// </summary>
        /// <param name="cell">The cell to link.</param>
        /// <param name="bidi"><c>true</c> to make the link bidirectional.</param>
        /// <returns><c>this</c>.</returns>
        public Cell Link(Cell cell, bool bidi = true)
        {
            this.links[cell] = true;
            return bidi ? cell.Link(this, false) : this;
        }

        /// <summary>
        /// Unlinks a cell.
        /// </summary>
        /// <param name="cell">The cell to unlink.</param>
        /// <param name="bidi"><c>true</c> to make the unlinking bidirectional.</param>
        /// <returns><c>this</c>.</returns>
        public Cell Unlink(Cell cell, bool bidi = true)
        {
            this.links.Remove(cell);
            return bidi ? cell.Unlink(this, false) : this;
        }

        /// <summary>
        /// Determines if the provided cell is linked to the current cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the provided cell is linked to the current cell, <c>false</c> otherwise.</returns>
        public bool IsLinkedTo(Cell? cell)
        {
            if (cell == null)
            {
                return false;
            }

            return this.links.ContainsKey(cell);
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