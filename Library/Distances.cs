//-----------------------------------------------------------------------
// <copyright file="Distances.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Used to record the distance of each cell from the start point.
    /// </summary>
    public class Distances
    {
        private readonly Cell root;
        private readonly Dictionary<Cell, int> cells = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Distances"/> class.
        /// </summary>
        /// <param name="root">The root cell used to record the distance of each cell from it.</param>
        public Distances(Cell root)
        {
            this.root = root;
            this.cells[root] = 0;
        }

        /// <summary>
        /// Gets the cells for which the distance between them and the root is known.
        /// </summary>
        public Cell[] Cells => this.cells.Keys.ToArray();

        /// <summary>
        /// Gets or sets the distance between the root and the given cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public int this[Cell cell]
        {
            get
            {
                return this.cells[cell];
            }

            set
            {
                this.cells[cell] = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the distance from the cell and the root is known yet.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the distance between the cell and the root is known, <c>false</c> otherwise.</returns>
        public bool HasCell(Cell cell) => this.cells.ContainsKey(cell);

        /// <summary>
        /// Determines the shortest path from the original starting point to the provided goal cell.
        /// </summary>
        /// <param name="goal">The goal cell.</param>
        /// <returns>The distances from the starting point to the goal cell.</returns>
        public Distances GetShortestPathTo(Cell goal)
        {
            Cell current = goal;

            Distances breadcrumbs = new(this.root);
            breadcrumbs[current] = this.cells[current];

            while (current != this.root)
            {
                foreach (Cell neighbor in current.Links)
                {
                    if (this.cells[neighbor] < this.cells[current])
                    {
                        breadcrumbs[neighbor] = this.cells[neighbor];
                        current = neighbor;
                        break;
                    }
                }
            }

            return breadcrumbs;
        }
    }
}