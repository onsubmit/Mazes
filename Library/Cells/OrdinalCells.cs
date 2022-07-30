//-----------------------------------------------------------------------
// <copyright file="OrdinalCells.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Cells
{
    /// <summary>
    /// Collection of cells in the ordinal directions.
    /// </summary>
    /// <typeparam name="TCell">The type of cell.</typeparam>
    public class OrdinalCells<TCell>
        where TCell : Cell
    {
        /// <summary>
        /// Gets or sets the northeast neighbor.
        /// </summary>
        public virtual TCell? NorthEast { get; set; }

        /// <summary>
        /// Gets or sets the southeast neighbor.
        /// </summary>
        public virtual TCell? SouthEast { get; set; }

        /// <summary>
        /// Gets or sets the southwest neighbor.
        /// </summary>
        public virtual TCell? SouthWest { get; set; }

        /// <summary>
        /// Gets or sets the northwest neighbor.
        /// </summary>
        public virtual TCell? NorthWest { get; set; }
    }
}
