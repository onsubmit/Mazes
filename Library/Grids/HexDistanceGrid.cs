//-----------------------------------------------------------------------
// <copyright file="HexDistanceGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;

    /// <summary>
    /// Version of <see cref="HexGrid"/> which can render the distance numbers for each cell.
    /// </summary>
    public class HexDistanceGrid : HexGrid, IDistanceGrid<HexCell>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HexDistanceGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public HexDistanceGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexDistanceGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public HexDistanceGrid(int rows, int columns)
            : base(rows, columns)
        {
            if (!this.GetType().IsSubclassOf(typeof(HexDistanceGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
        }

        /// <inheritdoc/>
        public virtual Distances<HexCell>? Distances { get; set; }

        /// <inheritdoc/>
        public Distances<HexCell> GetDistancesFromCell(HexCell cell)
        {
            return cell.GetDistances<HexCell>();
        }

        /// <summary>
        /// Gets the cell contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell contents.</returns>
        public override string GetCellContents(HexCell cell)
        {
            if (this.Distances == null || !this.Distances.HasCell(cell))
            {
                return base.GetCellContents(cell);
            }

            int distance = this.Distances[cell];
            return distance.ToString();
        }
    }
}
