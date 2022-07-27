//-----------------------------------------------------------------------
// <copyright file="PolarDistanceGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;

    /// <summary>
    /// Version of <see cref="PolarGrid"/> which can render the distance numbers for each cell.
    /// </summary>
    public class PolarDistanceGrid : PolarGrid, IDistanceGrid<PolarCell>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarDistanceGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        public PolarDistanceGrid(int rows)
            : base(rows)
        {
            if (!this.GetType().IsSubclassOf(typeof(PolarDistanceGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
        }

        /// <inheritdoc/>
        public virtual Distances<PolarCell>? Distances { get; set; }

        /// <inheritdoc/>
        public Distances<PolarCell> GetDistancesFromCell(PolarCell cell)
        {
            return cell.GetDistances<PolarCell>();
        }

        /// <summary>
        /// Gets the cell contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell contents.</returns>
        public override string GetCellContents(PolarCell cell)
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
