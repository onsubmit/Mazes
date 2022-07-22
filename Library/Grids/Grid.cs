//-----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;
    using SixLabors.ImageSharp;

    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    /// <typeparam name="TCell">The type of cells in the grid.</typeparam>
    public abstract class Grid<TCell>
        where TCell : Cell
    {
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
        /// Saves an image of the grid.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="cellSize">The size of each cell.</param>
        public void SaveImage(string filename, int cellSize = 10)
        {
            Image image = this.GetImage(cellSize);
            image.Save(filename);
        }

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
        /// Configures the cell neighbors.
        /// </summary>
        protected abstract void ConfigureCells();
    }
}
