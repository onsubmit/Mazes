//-----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using System.Diagnostics.CodeAnalysis;
    using Library.Cells;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    /// <typeparam name="TCell">The type of cells in the grid.</typeparam>
    public abstract class Grid<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Gets the size of the grid.
        /// </summary>
        public abstract int Size { get; }

        /// <summary>
        /// Performs the given action for each row of cells in the grid.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachRow(Action<TCell[]> action)
        {
            this.ForEachRow((element) =>
            {
                action(element);
                return IteratorResult.Continue;
            });
        }

        /// <summary>
        /// Performs the given function for each row of cells in the grid.
        /// </summary>
        /// <param name="func">The function to perform.</param>
        public abstract void ForEachRow(Func<TCell[], IteratorResult> func);

        /// <summary>
        /// Performs the given action for each cell in the grid.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachCell(Action<TCell> action)
        {
            this.ForEachCell(element =>
            {
                action(element);
                return IteratorResult.Continue;
            });
        }

        /// <summary>
        /// Performs the given function for each cell in the grid.
        /// </summary>
        /// <param name="func">The function to perform.</param>
        public abstract void ForEachCell(Func<TCell, IteratorResult> func);

        /// <summary>
        /// Gets a random cell.
        /// </summary>
        /// <returns>A random cell.</returns>
        public abstract TCell GetRandomCell();

        /// <summary>
        /// Gets the cell's contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's contents.</returns>
        public abstract string GetCellContents(TCell cell);

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public abstract Rgba32 GetCellBackgroundColor(TCell cell);

        /// <summary>
        /// Saves an image of the grid.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="cellSize">The size of each cell.</param>
        public abstract void SaveImage(string filename, int cellSize = 10);

        /// <summary>
        /// Gets an image representation of the grid.
        /// </summary>
        /// <param name="cellSize">The size of each cell.</param>
        /// <returns>The image.</returns>
        public abstract Image GetImage(int cellSize = 10);

        /// <summary>
        /// Initializes the grid.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Tries to get the initial value for each element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        protected abstract bool TryGetInitialElementValue(int row, int column, [NotNullWhen(returnValue: true)] out TCell? initialValue);

        /// <summary>
        /// Configures the cell neighbors.
        /// </summary>
        protected abstract void ConfigureCells();
    }
}
