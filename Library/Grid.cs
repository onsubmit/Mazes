//-----------------------------------------------------------------------
// <copyright file="Grid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library
{
    using System.Text;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Represents a maze grid, effectively a collection of <see cref="Cell"/> objects.
    /// </summary>
    public class Grid : TwoDimensionalClassArray<Cell>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public Grid(int size)
            : this(size, size)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows in the grid.</param>
        /// <param name="columns">The number of columns in the grid.</param>
        public Grid(int rows, int columns)
            : base(rows, columns)
        {
            this.InitializeElements((int row, int column, out Cell? initialValue) =>
            {
                initialValue = new(row, column);
                return true;
            });

            this.ConfigureCells();
        }

        /// <summary>
        /// Gets the collection of <see cref="Cell"/> objects.
        /// </summary>
        public Cell[,] Cells => this.Values;

        /// <summary>
        /// Gets the number of cells in the grid.
        /// </summary>
        public override int Size => base.Size;

        /// <summary>
        /// Gets the <see cref="Cell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="Cell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public Cell? this[int row, int column]
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

                return this.Cells[row, column];
            }
        }

        /// <summary>
        /// Gets the cell's contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's contents.</returns>
        public virtual string GetCellContents(Cell cell) => " ";

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public virtual Rgba32 GetCellBackgroundColor(Cell cell) => Rgba32.ParseHex("#ffffff");

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
        public Image GetImage(int cellSize = 10)
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
                    this.ForEachElement(cell =>
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
                                if (cell.North == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x1, y1), new(x2, y1) });
                                }

                                if (cell.West == null)
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x1, y1), new(x1, y2) });
                                }

                                if (!cell.IsLinkedTo(cell.East))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x2, y1), new(x2, y2) });
                                }

                                if (!cell.IsLinkedTo(cell.South))
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
        public List<Cell> GetDeadEnds()
        {
            List<Cell> cells = new();
            this.ForEachElement((cell) =>
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

            this.ForEachRow(row =>
            {
                StringBuilder topBuilder = new();
                topBuilder.Append(VerticalWall);

                StringBuilder bottomBuilder = new();
                bottomBuilder.Append(Corner);

                foreach (Cell c in row)
                {
                    Cell cell = c ?? new Cell(-1, -1);
                    char eastBoundary = cell.IsLinkedTo(cell.East) ? VerticalOpening : VerticalWall;
                    string southBoundary = cell.IsLinkedTo(cell.South) ? HorizontalOpening : HorizontalWall;

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

        /// <summary>
        /// Configures the cell neighbors.
        /// </summary>
        private void ConfigureCells()
        {
            for (int r = 0; r < this.Rows; r++)
            {
                for (int c = 0; c < this.Columns; c++)
                {
                    Cell cell = this.Cells[r, c];

                    cell.North = this[r - 1, c];
                    cell.South = this[r + 1, c];
                    cell.West = this[r, c - 1];
                    cell.East = this[r, c + 1];
                }
            }
        }
    }
}
