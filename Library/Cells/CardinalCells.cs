//-----------------------------------------------------------------------
// <copyright file="CardinalCells.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    /// <summary>
    /// Collection of cells in the cardinal directions.
    /// </summary>
    /// <typeparam name="TCell">The type of cell.</typeparam>
    public class CardinalCells<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Gets or sets the northern neighbor.
        /// </summary>
        public virtual TCell? North { get; set; }

        /// <summary>
        /// Gets or sets the southern neighbor.
        /// </summary>
        public virtual TCell? South { get; set; }

        /// <summary>
        /// Gets or sets the eastern neighbor.
        /// </summary>
        public virtual TCell? East { get; set; }

        /// <summary>
        /// Gets or sets the western neighbor.
        /// </summary>
        public virtual TCell? West { get; set; }
    }
}
