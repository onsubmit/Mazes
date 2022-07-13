//-----------------------------------------------------------------------
// <copyright file="ImageMask.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Masks
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Represents a grid mask defined by an image mask file.
    /// </summary>
    public class ImageMask : Mask
    {
        private static readonly Rgba32 Black = new(0, 0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMask"/> class.
        /// </summary>
        private ImageMask(Image<Rgba32> image)
            : base(image.Height, image.Width)
        {
            bool GetInitialValues(int r, int c, out bool initialValue)
            {
                initialValue = image[c, r] != Black;
                return true;
            }

            this.InitializeElements(GetInitialValues);
        }

        /// <summary>
        /// Gets the mask defined by the image mask in <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the image mask file.</param>
        /// <returns>The new mask.</returns>
        public static ImageMask FromFile(string path)
        {
            using Image<Rgba32> image = Image.Load<Rgba32>(path);
            return new(image);
        }
    }
}
