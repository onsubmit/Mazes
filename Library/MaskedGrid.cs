//-----------------------------------------------------------------------
// <copyright file="MaskedGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    public class MaskedGrid : Grid
    {
        private readonly Mask mask;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaskedGrid"/> class.
        /// </summary>
        /// <param name="mask">The grid mask.</param>
        public MaskedGrid(Mask mask)
            : base(mask.Rows, mask.Columns)
        {
            this.mask = mask;

            this.InitializeElements((int r, int c, out Cell? initialValue) =>
            {
                if (!mask[r, c])
                {
                    initialValue = null;
                    return false;
                }

                initialValue = new Cell(r, c);
                return true;
            });
        }

        /// <summary>
        /// Gets the number of cells in the masked grid.
        /// </summary>
        public override int Size => this.mask.NumEnabledCells;

        /// <summary>
        /// Gets a random cell from the masked grid.
        /// </summary>
        /// <returns>A random cell from the masked grid.</returns>
        public Cell GetRandomCell()
        {
            (int r, int c) = this.mask.GetCoordinatesOfRandomEnabledCell();
            return this.Values[r, c];
        }
    }
}
