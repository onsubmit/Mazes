//-----------------------------------------------------------------------
// <copyright file="CartesianDistanceGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;

    /// <summary>
    /// Version of <see cref="CartesianGrid"/> which can render the distance numbers for each cell.
    /// </summary>
    public class CartesianDistanceGrid : CartesianGrid, IDistanceGrid<CartesianCell>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianDistanceGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public CartesianDistanceGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianDistanceGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public CartesianDistanceGrid(int rows, int columns)
            : base(rows, columns)
        {
            if (!this.GetType().IsSubclassOf(typeof(CartesianDistanceGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
        }

        /// <inheritdoc/>
        public virtual Distances<CartesianCell>? Distances { get; set; }

        /// <inheritdoc/>
        public Distances<CartesianCell> GetDistancesFromCell(CartesianCell cell)
        {
            return cell.GetDistances<CartesianCell>();
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
