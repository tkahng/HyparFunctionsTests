using Elements;
using Elements.Geometry;
using System.Collections.Generic;

namespace SpaceSubdivide
{
      public static class SpaceSubdivide
    {
        /// <summary>
        /// partitions levels using various geometric operations
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A SpaceSubdivideOutputs instance containing computed results and the model with any new elements.</returns>
        public static SpaceSubdivideOutputs Execute(Dictionary<string, Model> inputModels, SpaceSubdivideInputs input)
        {
            var output = new SpaceSubdivideOutputs();
            return output;
        }
      }
}