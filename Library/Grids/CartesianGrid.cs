//-----------------------------------------------------------------------
// <copyright file="CartesianGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Library.Cells;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Represents a Cartesian maze grid, effectively a collection of <see cref="CartesianCell"/> objects arranged in x-y coordinates.
    /// </summary>
    public class CartesianGrid : Grid<CartesianCell>
    {
        /// <summary>
        /// The collection of <see cref="CartesianCell"/> objects.
        /// </summary>
        private readonly TwoDimensionalArray<CartesianCell> cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public CartesianGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartesianGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public CartesianGrid(int rows, int columns)
        {
            this.cells = new(rows, columns);

            if (!this.GetType().IsSubclassOf(typeof(CartesianGrid)))
            {
                // Derived classes are responsible for calling the Initialize method themselves from their own constructors.
                // This is a code smell... fix this, doofus.
                this.Initialize();
            }
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
        /// Gets the size of the grid.
        /// </summary>
        public virtual int Size => this.cells.Size;

        /// <summary>
        /// Gets the collection of cells.
        /// </summary>
        public CartesianCell[,] Values => this.cells.Values;

        /// <summary>
        /// Gets the <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public CartesianCell? this[int row, int column]
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

        /// <inheritdoc />
        public override void ForEachRow(Func<CartesianCell[], IteratorResult> func)
        {
            this.cells.ForEachRow(func);
        }

        /// <inheritdoc />
        public override void ForEachCell(Func<CartesianCell, IteratorResult> func)
        {
            this.cells.ForEachElement(func);
        }

        /// <inheritdoc />
        public override CartesianCell GetRandomCell() => this.cells.GetRandomElement();

        /// <summary>
        /// Gets the cell's contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's contents.</returns>
        public virtual string GetCellContents(CartesianCell cell) => " ";

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public virtual Rgba32 GetCellBackgroundColor(CartesianCell cell) => Rgba32.ParseHex("#ffffff");

        /// <inheritdoc />
        public override Image GetImage(int cellSize = 10)
        {
            int width = cellSize * this.Columns;
            int height = cellSize * this.Rows;

            Image<Rgba32> image = new(width + 1, height + 1);
            image.Mutate(imageContext =>
            {
                Rgba32 backgroundColor = Rgba32.ParseHex("#ffffff");
                imageContext.BackgroundColor(backgroundColor);

                Rgba32 wallColor = Rgba32.ParseHex("#000000");
                Pen linePen = new(wallColor, 1);

                Rectangle border = new(0, 0, width + 1, height + 1);
                imageContext.Draw(linePen, border);

                foreach (ImageGenerationMode mode in new[] { ImageGenerationMode.Backgrounds, ImageGenerationMode.Walls })
                {
                    this.ForEachCell(cell =>
                    {
                        int x1 = cell.Column * cellSize;
                        int y1 = cell.Row * cellSize;

                        int x2 = x1 + cellSize;
                        int y2 = y1 + cellSize;

                        switch (mode)
                        {
                            case ImageGenerationMode.Backgrounds:
                                Rgba32 cellBackgroundColor = this.GetCellBackgroundColor(cell);
                                Rectangle cellSquare = new(x1, y1, cellSize, cellSize);
                                imageContext.Fill(cellBackgroundColor, cellSquare);
                                break;

                            case ImageGenerationMode.Walls:
                                if (cell.CardinalCells.North == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x1, y1), new(x2, y1) });
                                }

                                if (cell.CardinalCells.West == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x1, y1), new(x1, y2) });
                                }

                                if (!cell.IsLinkedTo(cell.CardinalCells.East))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x2, y1), new(x2, y2) });
                                }

                                if (!cell.IsLinkedTo(cell.CardinalCells.South))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x1, y2), new(x2, y2) });
                                }

                                break;
                        }
                    });
                }
            });

            return image;
        }

        /// <summary>
        /// Gets all of the dead-end cells in the grid.
        /// </summary>
        /// <returns>The dead-end cells in the grid.</returns>
        public List<CartesianCell> GetDeadEnds()
        {
            List<CartesianCell> cells = new();
            this.cells.ForEachElement((cell) =>
            {
                if (cell.Links.Length == 1)
                {
                    cells.Add(cell);
                }
            });

            return cells;
        }

        /// <summary>
        /// Generates a string repsentation of the grid.
        /// </summary>
        /// <returns>A string repsentation of the grid.</returns>
        public override string ToString()
        {
            const string HorizontalOpening = "   ";
            const string HorizontalWall = "---";
            const char VerticalWall = '|';
            const char VerticalOpening = ' ';
            const char Corner = '+';

            StringBuilder sb = new();
            sb.Append(Corner);
            sb.AppendLine(string.Concat(Enumerable.Repeat(HorizontalWall + Corner, this.Columns)));

            this.cells.ForEachRow(row =>
            {
                StringBuilder topBuilder = new();
                topBuilder.Append(VerticalWall);

                StringBuilder bottomBuilder = new();
                bottomBuilder.Append(Corner);

                foreach (CartesianCell c in row)
                {
                    CartesianCell cell = c ?? new CartesianCell(-1, -1);
                    char eastBoundary = cell.IsLinkedTo(cell.CardinalCells.East) ? VerticalOpening : VerticalWall;
                    string southBoundary = cell.IsLinkedTo(cell.CardinalCells.South) ? HorizontalOpening : HorizontalWall;

                    topBuilder.Append($" {this.GetCellContents(cell)} ");
                    topBuilder.Append(eastBoundary);

                    bottomBuilder.Append(southBoundary);
                    bottomBuilder.Append(Corner);
                }

                sb.AppendLine(topBuilder.ToString());
                sb.AppendLine(bottomBuilder.ToString());
            });

            return sb.ToString();
        }

        /// <inheritdoc />
        protected override void Initialize()
        {
            this.cells.InitializeElements(this.TryGetInitialElementValue);
            this.ConfigureCells();
        }

        /// <inheritdoc />
        protected override void ConfigureCells()
        {
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    CartesianCell cell = this.cells.Values[r, c];

                    if (cell == null)
                    {
                        continue;
                    }

                    cell.CardinalCells.North = this[r - 1, c];
                    cell.CardinalCells.South = this[r + 1, c];
                    cell.CardinalCells.West = this[r, c - 1];
                    cell.CardinalCells.East = this[r, c + 1];
                }
            }
        }

        /// <summary>
        /// Tries to get the initial value for each element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        protected virtual bool TryGetInitialElementValue(int row, int column, [NotNullWhen(returnValue: true)] out CartesianCell? initialValue)
        {
            initialValue = new(row, column);
            return true;
        }
    }
}
