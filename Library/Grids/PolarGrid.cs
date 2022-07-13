//-----------------------------------------------------------------------
// <copyright file="PolarGrid.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Grids
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Drawing;
    using SixLabors.ImageSharp.Drawing.Processing;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// Represents a polar grid, allowing the creation of circular mazes.
    /// </summary>
    public class PolarGrid : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarGrid"/> class.
        /// </summary>
        /// <param name="size">The number of rows and columns in the grid.</param>
        public PolarGrid(int size)
            : base(size)
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets an image representation of the grid.
        /// </summary>
        /// <param name="cellSize">The size of each cell.</param>
        /// <returns>The image.</returns>
        public override Image GetImage(int cellSize = 10)
        {
            int center = this.Rows * cellSize;
            int size = 2 * center;

            Rgba32 backgroundColor = Rgba32.ParseHex("#ffffff");
            Rgba32 wallColor = Rgba32.ParseHex("#000000");
            Pen linePen = new(wallColor, 1);

            Image<Rgba32> image = new(size + 1, size + 1);
            image.Mutate(imageContext =>
            {
                imageContext.BackgroundColor(backgroundColor);

                PointF origin = new(center, center);
                int radius = center;
                EllipsePolygon circle = new(origin, radius);
                imageContext.DrawPolygon(linePen, circle.Points.ToArray());

                this.ForEachElement(cell =>
                {
                    double theta = 2 * Math.PI / this.Columns;
                    int innerRadius = cell.Row * cellSize;
                    int outerRadius = innerRadius + cellSize;
                    double thetaCounterClockwise = cell.Column * theta;
                    double thetaClockwise = thetaCounterClockwise + theta;

                    double cosThetaCounterClockwise = Math.Cos(thetaCounterClockwise);
                    double sinThetaCounterClockwise = Math.Sin(thetaCounterClockwise);
                    double cosThetaClockwise = Math.Cos(thetaClockwise);
                    double sinThetaClockwise = Math.Sin(thetaClockwise);

                    int x1 = (int)Math.Round(center + (innerRadius * cosThetaCounterClockwise));
                    int y1 = (int)Math.Round(center + (innerRadius * sinThetaCounterClockwise));
                    int x2 = (int)Math.Round(center + (outerRadius * cosThetaCounterClockwise));
                    int y2 = (int)Math.Round(center + (outerRadius * sinThetaCounterClockwise));
                    int x3 = (int)Math.Round(center + (innerRadius * cosThetaClockwise));
                    int y3 = (int)Math.Round(center + (innerRadius * sinThetaClockwise));
                    int x4 = (int)Math.Round(center + (outerRadius * cosThetaClockwise));
                    int y4 = (int)Math.Round(center + (outerRadius * sinThetaClockwise));

                    if (!cell.IsLinkedTo(cell.North))
                    {
                        imageContext.DrawLines(linePen, new PointF[] { new(x1, y1), new(x3, y3) });
                    }

                    if (!cell.IsLinkedTo(cell.East))
                    {
                        imageContext.DrawLines(linePen, new PointF[] { new(x3, y3), new(x4, y4) });
                    }
                });
            });

            return image;
        }
    }
}
