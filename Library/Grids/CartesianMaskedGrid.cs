//-----------------------------------------------------------------------
// <copyright file="CartesianMaskedGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using System.Diagnostics.CodeAnalysis;
    using Library.Cells;
    using Library.Masks;

    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    public sealed class CartesianMaskedGrid : CartesianColoredGrid
    {
        private readonly Mask mask;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianMaskedGrid"/> class.
        /// </summary>
        /// <param name="mask">The grid mask.</param>
        public CartesianMaskedGrid(Mask mask)
            : base(mask.Rows, mask.Columns)
        {
            this.mask = mask;

            this.Initialize();
        }

        /// <summary>
        /// Gets the number of cells in the masked grid.
        /// </summary>
        public override int Size => this.mask.NumEnabledCells;

        /// <summary>
        /// Gets a random cell from the masked grid.
        /// </summary>
        /// <returns>A random cell from the masked grid.</returns>
        public override CartesianCell GetRandomCell()
        {
            (int r, int c) = this.mask.GetCoordinatesOfRandomEnabledCell();
            return this.Values[r, c];
        }

        /// <summary>
        /// Tries to get the initial value for each element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        protected override bool TryGetInitialElementValue(int row, int column, [NotNullWhen(returnValue: true)] out CartesianCell? initialValue)
        {
            if (!this.mask[row, column])
            {
                initialValue = null;
                return false;
            }

            initialValue = new CartesianCell(row, column);
            return true;
        }
    }
}
