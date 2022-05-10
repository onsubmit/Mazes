﻿//-----------------------------------------------------------------------
// <copyright file="TwoDimensionalArray.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    /// <summary>
    /// Represents a two dimensional array.
    /// </summary>
    /// <typeparam name="T">The type of the array's values.</typeparam>
    public abstract class TwoDimensionalArray<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoDimensionalArray{T}"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the array.</param>
        public TwoDimensionalArray(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwoDimensionalArray{T}"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the array.</param>
        /// <param name="columns">The number of columns in the array.</param>
        public TwoDimensionalArray(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            this.Rows = rows;
            this.Columns = columns;
        }

        /// <summary>
        /// Gets or sets the collection of values.
        /// </summary>
        public T[,] Values { get; protected set; } = new T[0, 0];

        /// <summary>
        /// Gets the number of rows in the array.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets the number of columns in the array.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Gets the number of elements in the array.
        /// </summary>
        public virtual int Size => this.Rows * this.Columns;

        /// <summary>
        /// Initializes the array.
        /// </summary>
        /// <param name="initialValue">Initial value of each element.</param>
        public void InitializeElements(T initialValue)
        {
            this.Values = new T[this.Rows, this.Columns];
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    this.Values[r, c] = initialValue;
                }
            }
        }

        /// <summary>
        /// Performs the given action for each row of elements in the array.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachRow(Action<T[]> action)
        {
            this.ForEachRow((element) =>
            {
                action(element);
                return TwoDimensionalArrayIteratorResult.Continue;
            });
        }

        /// <summary>
        /// Performs the given function for each row of elements in the array.
        /// </summary>
        /// <param name="func">The function to perform.</param>
        public void ForEachRow(Func<T[], TwoDimensionalArrayIteratorResult> func)
        {
            for (int r = 0; r < this.Rows; r++)
            {
                T[] row = Enumerable.Range(0, this.Columns)
                    .Select(c => this.Values[r, c])
                    .ToArray();

                if (func(row) == TwoDimensionalArrayIteratorResult.Stop)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Gets a random element.
        /// </summary>
        /// <returns>A random element.</returns>
        public T GetRandomElement()
        {
            int row = Rand.Instance.Next(this.Rows);
            int column = Rand.Instance.Next(this.Columns);

            return this.Values[row, column];
        }

        /// <summary>
        /// Performs the given action for each element in the array.
        /// </summary>
        /// <param name="action">The action to perform.</param>
        public void ForEachElement(Action<T> action)
        {
            this.ForEachElement(element =>
            {
                action(element);
                return TwoDimensionalArrayIteratorResult.Continue;
            });
        }

        /// <summary>
        /// Performs the given function for each element in the array.
        /// </summary>
        /// <param name="func">The function to perform.</param>
        public void ForEachElement(Func<T, TwoDimensionalArrayIteratorResult> func)
        {
            this.ForEachRow((row) =>
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    if (func(row[c]) == TwoDimensionalArrayIteratorResult.Stop)
                    {
                        return TwoDimensionalArrayIteratorResult.Stop;
                    }
                }

                return TwoDimensionalArrayIteratorResult.Continue;
            });
        }
    }
}
