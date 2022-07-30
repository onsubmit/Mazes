//-----------------------------------------------------------------------
// <copyright file="HexGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using System.Diagnostics.CodeAnalysis;
    using Library.Cells;
    using Library.Extensions;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing;
    using SixLabors.ImageSharp.Drawing.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Represents a maze grid made of hexagonal cells, effectively a collection of <see cref="HexCell"/> objects arranged in x-y coordinates.
    /// </summary>
    public class HexGrid : Grid<HexCell>
    {
        /// <summary>
        /// The collection of <see cref="HexCell"/> objects.
        /// </summary>
        private readonly TwoDimensionalArray<HexCell> cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="HexGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public HexGrid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HexGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public HexGrid(int rows, int columns)
        {
            this.cells = new(rows, columns);
            this.Initialize();
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
        /// Gets the collection of cells.
        /// </summary>
        public HexCell[,] Values => this.cells.Values;

        /// <summary>
        /// Gets the <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="CartesianCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public HexCell? this[int row, int column]
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

        /// <inheritdoc/>
        public override void ForEachCell(Func<HexCell, IteratorResult> func)
        {
            this.cells.ForEachElement(func);
        }

        /// <inheritdoc/>
        public override void ForEachRow(Func<HexCell[], IteratorResult> func)
        {
            this.cells.ForEachRow(func);
        }

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public virtual Rgba32 GetCellBackgroundColor(HexCell cell) => Rgba32.ParseHex("#ffffff");

        /// <inheritdoc/>
        public override Image GetImage(int cellSize = 10)
        {
            double sizeA = cellSize / 2d;
            double sizeB = cellSize * Math.Sqrt(3d) / 2d;
            double width = cellSize * 2d;
            double height = sizeB * 2d;

            int imgWidth = (int)Math.Ceiling((3 * sizeA * this.Columns) + sizeA);
            int imgHeight = (int)Math.Ceiling((height * this.Rows) + sizeB);

            Image<Rgba32> image = new(imgWidth + 1, imgHeight + 1);
            image.Mutate(imageContext =>
            {
                Rgba32 backgroundColor = Rgba32.ParseHex("#ffffff");
                imageContext.BackgroundColor(backgroundColor);

                Rgba32 wallColor = Rgba32.ParseHex("#000000");
                Pen linePen = new(wallColor, 1);

                foreach (ImageGenerationMode mode in new[] { ImageGenerationMode.Backgrounds, ImageGenerationMode.Walls })
                {
                    this.ForEachCell(cell =>
                    {
                        double centerX = cellSize + (3 * cell.Column * sizeA);
                        double centerY = sizeB + (cell.Row * height);
                        if (cell.Column.IsOdd())
                        {
                            centerY += sizeB;
                        }

                        int farWestX = (int)(centerX - cellSize);
                        int nearWestX = (int)(centerX - sizeA);
                        int nearEastX = (int)(centerX + sizeA);
                        int farEastX = (int)(centerX + cellSize);

                        int northY = (int)(centerY - sizeB);
                        int middleY = (int)centerY;
                        int southY = (int)(centerY + sizeB);

                        switch (mode)
                        {
                            case ImageGenerationMode.Backgrounds:
                                Rgba32 cellBackgroundColor = this.GetCellBackgroundColor(cell);

                                Polygon polygon = new(
                                    new LinearLineSegment(
                                        new Point(farWestX, middleY),
                                        new Point(nearWestX, northY),
                                        new Point(nearEastX, northY),
                                        new Point(farEastX, middleY),
                                        new Point(nearEastX, southY),
                                        new Point(nearWestX, southY)));

                                imageContext.Fill(cellBackgroundColor, polygon);
                                break;

                            case ImageGenerationMode.Walls:
                                if (cell.OrdinalCells.SouthWest == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(farWestX, middleY), new(nearWestX, southY) });
                                }

                                if (cell.OrdinalCells.NorthWest == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(farWestX, middleY), new(nearWestX, northY) });
                                }

                                if (cell.CardinalCells.North == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(nearWestX, northY), new(nearEastX, northY) });
                                }

                                if (!cell.IsLinkedTo(cell.OrdinalCells.NorthEast))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(nearEastX, northY), new(farEastX, middleY) });
                                }

                                if (!cell.IsLinkedTo(cell.OrdinalCells.SouthEast))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(farEastX, middleY), new(nearEastX, southY) });
                                }

                                if (!cell.IsLinkedTo(cell.CardinalCells.South))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(nearEastX, southY), new(nearWestX, southY) });
                                }

                                break;
                        }
                    });
                }
            });

            return image;
        }

        /// <inheritdoc />
        public override HexCell GetRandomCell() => this.cells.GetRandomElement();

        /// <inheritdoc/>
        protected override void ConfigureCells()
        {
            this.ForEachCell(cell =>
            {
                int row = cell.Row;
                int column = cell.Column;

                int northDiagonal, southDiagonal;
                if (column.IsEven())
                {
                    northDiagonal = row - 1;
                    southDiagonal = row;
                }
                else
                {
                    northDiagonal = row;
                    southDiagonal = row + 1;
                }

                cell.OrdinalCells.NorthWest = this[northDiagonal, column - 1];
                cell.CardinalCells.North = this[row - 1, column];
                cell.OrdinalCells.NorthEast = this[northDiagonal, column + 1];
                cell.OrdinalCells.SouthWest = this[southDiagonal, column - 1];
                cell.CardinalCells.South = this[row + 1, column];
                cell.OrdinalCells.SouthEast = this[southDiagonal, column + 1];
            });
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            this.cells.InitializeElements(this.TryGetInitialElementValue);
            this.ConfigureCells();
        }

        /// <summary>
        /// Tries to get the initial value for each element.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="initialValue">The initial value.</param>
        /// <returns><c>true</c> if the initial value was successfully determined, <c>false</c> otherwise.</returns>
        private bool TryGetInitialElementValue(int row, int column, [NotNullWhen(returnValue: true)] out HexCell? initialValue)
        {
            initialValue = new(row, column);
            return true;
        }
    }
}
