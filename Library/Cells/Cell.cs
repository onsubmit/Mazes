//-----------------------------------------------------------------------
// <copyright file="Cell.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    /// <summary>
    /// Represents a maze cell.
    /// </summary>
    public abstract class Cell
    {
        private readonly Dictionary<Cell, bool> links = new();

        /// <summary>
        /// Gets a value indicating whether the cell has any linked cells.
        /// </summary>
        public bool HasLink => this.links.Any();

        /// <summary>
        /// Gets the linked cells.
        /// </summary>
        public Cell[] Links => this.links.Keys.ToArray();

        /// <summary>
        /// Gets all the neighboring cells.
        /// </summary>
        public abstract Cell[] Neighbors { get; }

        /// <summary>
        /// Links a cell.
        /// </summary>
        /// <param name="cell">The cell to link.</param>
        /// <param name="bidi"><c>true</c> to make the link bidirectional.</param>
        /// <returns><c>this</c>.</returns>
        public Cell Link(Cell cell, bool bidi = true)
        {
            this.links[cell] = true;
            return bidi ? cell.Link(this, false) : this;
        }

        /// <summary>
        /// Unlinks a cell.
        /// </summary>
        /// <param name="cell">The cell to unlink.</param>
        /// <param name="bidi"><c>true</c> to make the unlinking bidirectional.</param>
        /// <returns><c>this</c>.</returns>
        public Cell Unlink(Cell cell, bool bidi = true)
        {
            this.links.Remove(cell);
            return bidi ? cell.Unlink(this, false) : this;
        }

        /// <summary>
        /// Determines if the provided cell is linked to the current cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the provided cell is linked to the current cell, <c>false</c> otherwise.</returns>
        public bool IsLinkedTo(Cell? cell)
        {
            if (cell == null)
            {
                return false;
            }

            return this.links.ContainsKey(cell);
        }

        /// <summary>
        /// Gets the distances between this cell and all the other cells.
        /// </summary>
        /// <typeparam name="TCell">The type of cell.</typeparam>
        /// <returns>The distances between this cell and all the other cells.</returns>
        public Distances<TCell> GetDistances<TCell>()
            where TCell : Cell
        {
            if (this is not TCell thisCell)
            {
                throw new InvalidOperationException($"This cell is not of type {typeof(TCell)}. Found: {this.GetType()}");
            }

            Distances<TCell> distances = new(thisCell);
            List<TCell> frontier = new() { thisCell };

            while (frontier.Any())
            {
                List<TCell> newFrontier = new();

                foreach (TCell cell in frontier)
                {
                    foreach (TCell linked in cell.Links)
                    {
                        if (distances.HasCell(linked))
                        {
                            continue;
                        }

                        distances[linked] = distances[cell] + 1;
                        newFrontier.Add(linked);
                    }
                }

                frontier = newFrontier;
            }

            return distances;
        }
    }
}