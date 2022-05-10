//-----------------------------------------------------------------------
// <copyright file="TwoDimensionalClassArray.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a two dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the array's values.</typeparam>
    public abstract class TwoDimensionalClassArray<T> : TwoDimensionalArray<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoDimensionalClassArray{T}"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the array.</param>
        public TwoDimensionalClassArray(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoDimensionalClassArray{T}"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the array.</param>
        /// <param name="columns">The number of columns in the array.</param>
        public TwoDimensionalClassArray(int rows, int columns)
            : base(rows, columns)
        {
        }

        /// <summary>
        /// Delegate that tries to get the initial value for the element located at the given row and column.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        public delegate bool TryGetInitialElementValueDelegate(
            int row,
            int column,
            [NotNullWhen(returnValue: true)] out T? initialValue);

        /// <summary>
        /// Initializes the array.
        /// </summary>
        /// <param name="tryGetInitialElementValue">Function that attempts to get the initial value of each element.</param>
        public void InitializeElements(TryGetInitialElementValueDelegate tryGetInitialElementValue)
        {
            this.Values = new T[this.Rows, this.Columns];

            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    if (tryGetInitialElementValue(r, c, out T? initialValue))
                    {
                        this.Values[r, c] = initialValue;
                    }
                }
            }
        }
    }
}
