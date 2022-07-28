//-----------------------------------------------------------------------
// <copyright file="PolarGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using Library.Cells;
    using Library.Extensions;
    using SixLabors.Fonts;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing;
    using SixLabors.ImageSharp.Drawing.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Represents a polar grid, allowing the creation of circular mazes.
    /// </summary>
    public class PolarGrid : Grid<PolarCell>
    {
        /// <summary>
        /// The collection of <see cref="CartesianCell"/> objects.
        /// </summary>
        private readonly List<PolarCell>[] rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolarGrid"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        public PolarGrid(int rows)
        {
            this.rows = new List<PolarCell>[rows];
            this.Initialize();
        }

        /// <summary>
        /// Gets the number of rows in the grid.
        /// </summary>
        public int Rows => this.rows.Length;

        /// <summary>
        /// Gets the collection of cells.
        /// </summary>
        public List<PolarCell>[] Values => this.rows;

        /// <summary>
        /// Gets the <see cref="PolarCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.
        /// </summary>
        /// <param name="row">The desired cell row.</param>
        /// <param name="column">The desired cell column.</param>
        /// <returns>The <see cref="PolarCell"/> at the given coordinates or <c>null</c> if the coordinates are out of range.</returns>
        public PolarCell? this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= this.Rows)
                {
                    return null;
                }

                // The cells on the clockwise boundary are adjacent to the cells on the counter-clockwise boundary.
                return this.rows[row][column.Modulo(this.rows[row].Count)];
            }
        }

        /// <summary>
        /// Gets the cell's contents.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's contents.</returns>
        public virtual string GetCellContents(PolarCell cell) => " ";

        /// <summary>
        /// Gets the cell's background color.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <returns>The cell's background color.</returns>
        public virtual Rgba32 GetCellBackgroundColor(PolarCell cell) => Rgba32.ParseHex("#ffffff");

        /// <inheritdoc />
        public override Image GetImage(int cellSize = 10)
        {
            Dictionary<PolarCell, int[]> cellCorners = new();

            int center = this.Rows * cellSize;
            PointF origin = new(center, center);
            int size = 2 * center;

            Rgba32 backgroundColor = Rgba32.ParseHex("#ffffff");
            Rgba32 wallColor = Rgba32.ParseHex("#000000");
            float lineWidth = cellSize / 25.0f;
            Pen linePen = new(wallColor, lineWidth);
            Font font = new(SystemFonts.Get("Arial"), cellSize / 4, FontStyle.Regular);

            Image<Rgba32> image = new(size + (int)Math.Ceiling(lineWidth), size + (int)Math.Ceiling(lineWidth));
            image.Mutate(imageContext =>
            {
                imageContext.BackgroundColor(backgroundColor);

                // Draw origin background.
                PolarCell originCell = this.Values[0][0];
                EllipsePolygon originBackgroundCircle = new(origin, cellSize);
                imageContext.Fill(this.GetCellBackgroundColor(originCell), originBackgroundCircle);

                foreach (ImageGenerationMode mode in new[] { ImageGenerationMode.PreCalculate, ImageGenerationMode.Backgrounds, ImageGenerationMode.Walls, ImageGenerationMode.Text })
                {
                    this.ForEachCell(cell =>
                    {
                        if (cell.Row == 0)
                        {
                            return IteratorResult.Continue;
                        }

                        int x1 = 0, y1 = 0, x2 = 0, y2 = 0, x3 = 0, y3 = 0, x4 = 0, y4 = 0;
                        if (cellCorners.TryGetValue(cell, out int[]? corners))
                        {
                            x1 = corners[0];
                            y1 = corners[1];
                            x2 = corners[2];
                            y2 = corners[3];
                            x3 = corners[4];
                            y3 = corners[5];
                            x4 = corners[6];
                            y4 = corners[7];
                        }
                        else if (mode != ImageGenerationMode.PreCalculate)
                        {
                            throw new InvalidOperationException("Cell corners not precalculated.");
                        }

                        int innerRadius, outerRadius;
                        switch (mode)
                        {
                            case ImageGenerationMode.PreCalculate:
                                double theta = 2 * Math.PI / this.rows[cell.Row].Count;
                                innerRadius = cell.Row * cellSize;
                                outerRadius = innerRadius + cellSize;
                                double thetaCounterClockwise = cell.Column * theta;
                                double thetaClockwise = thetaCounterClockwise + theta;

                                double cosThetaCounterClockwise = Math.Cos(thetaCounterClockwise);
                                double sinThetaCounterClockwise = Math.Sin(thetaCounterClockwise);
                                double cosThetaClockwise = Math.Cos(thetaClockwise);
                                double sinThetaClockwise = Math.Sin(thetaClockwise);

                                x1 = (int)Math.Round(center + (innerRadius * cosThetaCounterClockwise));
                                y1 = (int)Math.Round(center + (innerRadius * sinThetaCounterClockwise));
                                x2 = (int)Math.Round(center + (outerRadius * cosThetaCounterClockwise));
                                y2 = (int)Math.Round(center + (outerRadius * sinThetaCounterClockwise));
                                x3 = (int)Math.Round(center + (innerRadius * cosThetaClockwise));
                                y3 = (int)Math.Round(center + (innerRadius * sinThetaClockwise));
                                x4 = (int)Math.Round(center + (outerRadius * cosThetaClockwise));
                                y4 = (int)Math.Round(center + (outerRadius * sinThetaClockwise));

                                corners = new[] { x1, y1, x2, y2, x3, y3, x4, y4 };
                                cellCorners.Add(cell, corners);

                                break;
                            case ImageGenerationMode.Backgrounds:
                                innerRadius = cell.Row * cellSize;
                                outerRadius = innerRadius + cellSize;

                                Rgba32 cellBackgroundColor = this.GetCellBackgroundColor(cell);
                                Pen backgroundPen = new(cellBackgroundColor, 3);

                                List<ILineSegment> segments = new()
                                {
                                    new ArcLineSegment(new(x1, y1), new(x3, y3), new SizeF(innerRadius, innerRadius), 0, false, true),
                                    new LinearLineSegment(new PointF[] { new(x2, y2), new(x3, y3) }),
                                    new LinearLineSegment(new PointF[] { new(x3, y3), new(x4, y4) }),
                                    new ArcLineSegment(new(x4, y4), new(x2, y2), new SizeF(outerRadius, outerRadius), 0, false, false),
                                };

                                Polygon polygon = new(segments);
                                segments.ForEach(segment => imageContext.DrawLines(backgroundPen, segment.Flatten().ToArray()));
                                imageContext.Fill(cellBackgroundColor, polygon);

                                break;

                            case ImageGenerationMode.Walls:
                                if (!cell.IsLinkedTo(cell.Inward))
                                {
                                    innerRadius = cell.Row * cellSize;
                                    ArcLineSegment curvedWall = new(new(x1, y1), new(x3, y3), new SizeF(innerRadius, innerRadius), 0, false, true);
                                    imageContext.DrawLines(linePen, curvedWall.Flatten().ToArray());
                                }

                                if (!cell.IsLinkedTo(cell.Clockwise))
                                {
                                    imageContext.DrawLines(linePen, new PointF[] { new(x3, y3), new(x4, y4) });
                                }

                                break;

                            case ImageGenerationMode.Text:
                                PointF point = new(((x1 + x4) / 2f) - (font.Size / 2f), ((y1 + y4) / 2f) - (font.Size / 2f));
                                imageContext.DrawText(this.GetCellContents(cell), font, Color.Black, point);
                                break;
                        }

                        return IteratorResult.Continue;
                    });
                }

                EllipsePolygon outerWallCircle = new(origin, center);
                imageContext.DrawPolygon(linePen, outerWallCircle.Points.ToArray());
            });

            return image;
        }

        /// <inheritdoc/>
        public override void ForEachRow(Func<PolarCell[], IteratorResult> func)
        {
            for (int r = 0; r < this.rows.Length; r++)
            {
                PolarCell[] row = this.rows[r].ToArray();

                if (func(row) == IteratorResult.Stop)
                {
                    return;
                }
            }
        }

        /// <inheritdoc/>
        public override void ForEachCell(Func<PolarCell, IteratorResult> func)
        {
            this.ForEachRow((row) =>
            {
                for (int c = 0; c < row.Length; c++)
                {
                    if (row[c] == null)
                    {
                        continue;
                    }

                    if (func(row[c]) == IteratorResult.Stop)
                    {
                        return IteratorResult.Stop;
                    }
                }

                return IteratorResult.Continue;
            });
        }

        /// <inheritdoc/>
        public override PolarCell GetRandomCell()
        {
            int row = Rand.Instance.Next(this.Rows);
            int column = Rand.Instance.Next(this.rows[row].Count);

            return this.rows[row][column];
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            double rowHeight = 1d / this.Rows;

            // Special-case the origin. Force the innermost row to be a single cell.
            this.rows[0] = new() { new PolarCell(0, 0) };

            for (int row = 1; row < this.Rows; row++)
            {
                // Distance from the origin to the row's inner wall.
                double innerRadius = (double)row / this.Rows;
                double wallCircumference = 2 * Math.PI * innerRadius;

                int numCellsInPreviousRow = this.rows[row - 1].Count;
                double undividedCellWidth = wallCircumference / numCellsInPreviousRow;

                // Ratio of number of cells in current row to previous row.
                // Will always be 1 or 2, except for the row and index 1.
                int ratio = (int)Math.Round(undividedCellWidth / rowHeight);

                int numCellsInCurrentRow = numCellsInPreviousRow * ratio;
                this.rows[row] = new(numCellsInCurrentRow);
                for (int column = 0; column < numCellsInCurrentRow; column++)
                {
                    this.rows[row].Add(new PolarCell(row, column));
                }
            }

            this.ConfigureCells();
        }

        /// <inheritdoc/>
        protected override void ConfigureCells()
        {
            this.ForEachCell((cell) =>
            {
                int row = cell.Row;
                int column = cell.Column;

                if (row <= 0)
                {
                    // Ignore the cell at the origin, it only has outward neighbors.
                    return IteratorResult.Continue;
                }

                cell.Clockwise = this[row, column + 1];
                cell.CounterClockwise = this[row, column - 1];

                // Ratio of number of cells in current row to previous row.
                int ratio = this.rows[row].Count / this.rows[row - 1].Count;

                // Determine which cell in the previous row is the "parent",
                // which may or not have been subdivided to produce the current cell.
                PolarCell parent = this.rows[row - 1][column / ratio];

                // Add the current cell as one of the outward neighbors of the parent.
                parent.Outward.Add(cell);

                // Set the parent as the inward neighbo of the current cell.
                cell.Inward = parent;

                return IteratorResult.Continue;
            });
        }
    }
}
