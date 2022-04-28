//-----------------------------------------------------------------------
// <copyright file="GenerationAlgorithm.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Library.Algorithms.Generation
{
    /// <summary>
    /// Base abstract implementation of <see cref="IGenerationAlgorithm"/>.
    /// </summary>
    public abstract class GenerationAlgorithm : IGenerationAlgorithm
    {
        /// <summary>
        /// Gets the algorithm's name.
        /// </summary>
        public string Name => this.GetType().Name;

        /// <summary>
        /// Executes the algorithm.
        /// </summary>
        /// <param name="grid">The maze grid.</param>
        public abstract void Execute(Grid grid);
    }
}
