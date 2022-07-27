//-----------------------------------------------------------------------
// <copyright file="DistanceGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;

    /// <summary>
    /// Version of <see cref="CartesianGrid"/> which can render the distance numbers for each cell.
    /// </summary>
    public abstract class DistanceGrid : CartesianGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public DistanceGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public DistanceGrid(int rows, int columns)
            : base(rows, columns)
        {
            if (!this.GetType().IsSubclassOf(typeof(DistanceGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
        }

        /// <summary>
        /// Gets or sets the distances.
        /// </summary>
        public virtual Distances<Cell>? Distances { get; set; }

        /// <summary>
        /// Sets the distances from the cell at the given coordinates.
        /// </summary>
        /// <param name="row">The cell row.</param>
        /// <param name="column">The cell column.</param>
        /// <returns>The distances from the cell at the given coordinates.</returns>
        public Distances<Cell> GetDistancesFromCell(int row, int column)
        {
            Cell? cell = this[row, column];
            if (cell == null)
            {
                throw new InvalidOperationException($"No cell found at ({row}, {column}).");
            }

            return cell.GetDistances();
        }

        /// <summary>
        /// Gets the cell contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell contents.</returns>
        public override string GetCellContents(CartesianCell cell)
        {
            if (this.Distances == null || !this.Distances.HasCell(cell))
            {
                return base.GetCellContents(cell);
            }

            int distance = this.Distances[cell];

            if (distance < 10)
            {
                return distance.ToString();
            }

            if (distance >= 36)
            {
                return "?";
            }

            // 10 + 87 = 97 == 'a'
            // 35 + 87 = 122 == 'z'
            const int AsciiW = 87;
            return Convert.ToChar(distance + AsciiW).ToString();
        }
    }
}
