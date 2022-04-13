//-----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using System.Text;

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
        /// Gets the number of cells in the grid.
        /// </summary>
        public int Size => this.Rows * this.Columns;

        /// <summary>
        /// Gets the collection of <see cref="Cell"/> objects.
        /// </summary>
        public Cell[,] Cells { get; private set; } = new Cell[0, 0];

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
        /// Performs the given action for each row of cells in the grid.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachRow(Action<Cell[]> action)
        {
            for (int r = 0; r < this.Rows; r++)
            {
                Cell[] row = Enumerable.Range(0, this.Columns)
                    .Select(c => this.Cells[r, c])
                    .ToArray();

                action(row);
            }
        }

        /// <summary>
        /// Gets a random cell.
        /// </summary>
        /// <returns>A random cell.</returns>
        public Cell GetRandomCell()
        {
            int row = Rand.Instance.Next(this.Rows);
            int column = Rand.Instance.Next(this.Columns);

            return this.Cells[row, column];
        }

        /// <summary>
        /// Performs the given action for each cell in the grid.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachCell(Action<Cell> action)
        {
            this.ForEachRow((row) =>
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    if (row[c] == null)
                    {
                        continue;
                    }

                    action(row[c]);
                }
            });
        }

        /// <summary>
        /// Generates a string repsentation of the grid.
        /// </summary>
        /// <returns>A string repsentation of the grid.</returns>
        public override string ToString()
        {
            const string HorizontalOpening = "   ";
            const string HorizontalWall = "---";
            const char VerticalWall = '|';
            const char VerticalOpening = ' ';
            const char Corner = '+';

            StringBuilder sb = new();
            sb.Append(Corner);
            sb.AppendLine(string.Concat(Enumerable.Repeat(HorizontalWall + Corner, this.Columns)));

            this.ForEachRow(row =>
            {
                StringBuilder topBuilder = new();
                topBuilder.Append(VerticalWall);

                StringBuilder bottomBuilder = new();
                bottomBuilder.Append(Corner);

                foreach (Cell c in row)
                {
                    Cell cell = c ?? new Cell(-1, -1);
                    char eastBoundary = cell.IsLinkedTo(cell.East) ? VerticalOpening : VerticalWall;
                    string southBoundary = cell.IsLinkedTo(cell.South) ? HorizontalOpening : HorizontalWall;

                    topBuilder.Append(HorizontalOpening);
                    topBuilder.Append(eastBoundary);

                    bottomBuilder.Append(southBoundary);
                    bottomBuilder.Append(Corner);
                }

                sb.AppendLine(topBuilder.ToString());
                sb.AppendLine(bottomBuilder.ToString());
            });

            return sb.ToString();
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
