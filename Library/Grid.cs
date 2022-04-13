//-----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public Grid(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;

            this.PrepareGrid();
            this.ConfigureCells();

            this.Cells = new Cell[0, 0];
        }

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets the number of columns in the grid.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets the collection of <see cref="Cell"/> objects.
        /// </summary>
        public Cell[,] Cells { get; private set; }

        /// <summary>
        /// Gets the <see cref="Cell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="Cell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public Cell? this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.Rows)
                {
                    return null;
                }

                if (column < 0 || column >= this.Columns)
                {
                    return null;
                }

                return this.Cells[row, column];
            }
        }

        /// <summary>
        /// Prepares the initial state of the grid.
        /// </summary>
        private void PrepareGrid()
        {
            this.Cells = new Cell[this.Rows, this.Columns];
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    this.Cells[r, c] = new Cell(r, c);
                }
            }
        }

        /// <summary>
        /// Configures the cell neighbors.
        /// </summary>
        private void ConfigureCells()
        {
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    Cell cell = this.Cells[r, c];

                    cell.North = this[r - 1, c];
                    cell.South = this[r + 1, c];
                    cell.West = this[r, c - 1];
                    cell.East = this[r, c + 1];
                }
            }
        }
    }
}
