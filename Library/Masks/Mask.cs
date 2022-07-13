//-----------------------------------------------------------------------
// <copyright file="Mask.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Masks
{
    /// <summary>
    /// Represents a grid mask.
    /// </summary>
    public class Mask : TwoDimensionalArray<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mask"/> class.
        /// </summary>
        public Mask()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mask"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public Mask(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mask"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the mask.</param>
        /// <param name="columns">The number of columns in the mask.</param>
        public Mask(int rows, int columns)
            : base(rows, columns)
        {
            if (!this.GetType().IsSubclassOf(typeof(Mask)))
            {
                // Derived classes are responsible for calling the InitializeElements method themselves from their own constructors.
                this.InitializeElements(true);
            }
        }

        /// <summary>
        /// Gets the collection of cell states, indicating which cells are enabled or disabled.
        /// </summary>
        public bool[,] CellStates => this.Values;

        /// <summary>
        /// Gets the number of enabled cells in the mask.
        /// </summary>
        public int NumEnabledCells
        {
            get
            {
                int count = 0;
                this.ForEachElement(element =>
                {
                    if (element)
                    {
                        count++;
                    }
                });

                return count;
            }
        }

        /// <summary>
        /// Gets or sets the state at the given coordinates.
        /// </summary>
        /// <param name="row">The desired row.</param>
        /// <param name="column">The desired column.</param>
        public bool this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.Rows)
                {
                    return false;
                }

                if (column < 0 || column >= this.Columns)
                {
                    return false;
                }

                return this.CellStates[row, column];
            }

            set
            {
                this.CellStates[row, column] = value;
            }
        }

        /// <summary>
        /// Gets the coordinates of a random, enabled cell.
        /// </summary>
        /// <returns>The coordinates of a random, enabled cell.</returns>
        public (int Row, int Column) GetCoordinatesOfRandomEnabledCell()
        {
            while (true)
            {
                int row = Rand.Instance.Next(this.Rows);
                int column = Rand.Instance.Next(this.Columns);

                if (this[row, column])
                {
                    return (row, column);
                }
            }
        }
    }
}
