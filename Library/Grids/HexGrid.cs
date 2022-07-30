//-----------------------------------------------------------------------
// <copyright file="HexGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using System.Diagnostics.CodeAnalysis;
    using Library.Cells;
    using Library.Extensions;
    using SixLabors.ImageSharp;

    /// <summary>
    /// Represents a maze grid made of hexagonal cells, effectively a collection of <see cref="HexCell"/> objects arranged in x-y coordinates.
    /// </summary>
    public sealed class HexGrid : Grid<HexCell>
    {
        /// <summary>
        /// The collection of <see cref="HexCell"/> objects.
        /// </summary>
        private readonly TwoDimensionalArray<HexCell> cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="HexGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public HexGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public HexGrid(int rows, int columns)
        {
            this.cells = new(rows, columns);
            this.Initialize();
        }

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        public int Rows => this.cells.Rows;

        /// <summary>
        /// Gets the number of columns in the grid.
        /// </summary>
        public int Columns => this.cells.Columns;

        /// <summary>
        /// Gets the collection of cells.
        /// </summary>
        public HexCell[,] Values => this.cells.Values;

        /// <summary>
        /// Gets the <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public HexCell? this[int row, int column]
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

                return this.cells.Values[row, column];
            }
        }

        /// <inheritdoc/>
        public override void ForEachCell(Func<HexCell, IteratorResult> func)
        {
            this.cells.ForEachElement(func);
        }

        /// <inheritdoc/>
        public override void ForEachRow(Func<HexCell[], IteratorResult> func)
        {
            this.cells.ForEachRow(func);
        }

        /// <inheritdoc/>
        public override Image GetImage(int cellSize = 10)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override HexCell GetRandomCell() => this.cells.GetRandomElement();

        /// <inheritdoc/>
        protected override void ConfigureCells()
        {
            this.ForEachCell(cell =>
            {
                int row = cell.Row;
                int column = cell.Column;

                int northDiagonal, southDiagonal;
                if (column.IsEven())
                {
                    northDiagonal = row - 1;
                    southDiagonal = row;
                }
                else
                {
                    northDiagonal = row;
                    southDiagonal = row + 1;
                }

                cell.OrdinalCells.NorthWest = this[northDiagonal, column - 1];
                cell.CardinalCells.North = this[row - 1, column];
                cell.OrdinalCells.NorthEast = this[northDiagonal, column + 1];
                cell.OrdinalCells.SouthWest = this[southDiagonal, column - 1];
                cell.CardinalCells.South = this[row + 1, column];
                cell.OrdinalCells.SouthEast = this[southDiagonal, column + 1];
            });
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            this.cells.InitializeElements(this.TryGetInitialElementValue);
            this.ConfigureCells();
        }

        /// <summary>
        /// Tries to get the initial value for each element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        private bool TryGetInitialElementValue(int row, int column, [NotNullWhen(returnValue: true)] out HexCell? initialValue)
        {
            initialValue = new(row, column);
            return true;
        }
    }
}
