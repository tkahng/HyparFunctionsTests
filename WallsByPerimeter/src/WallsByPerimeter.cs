using Elements;
using Elements.Geometry;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WallsByPerimeter
{
      public static class WallsByPerimeter
    {
        /// <summary>
        /// The WallsByPerimeter function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A WallsByPerimeterOutputs instance containing computed results and the model with any new elements.</returns>
        public static WallsByPerimeterOutputs Execute(Dictionary<string, Model> inputModels, WallsByPerimeterInputs input)
        {
            /// Your code here.
            var height = 1.0;
            var volume = input.Length * input.Width * height;
            var output = new WallsByPerimeterOutputs(volume);
            var rectangle = Polygon.Rectangle(input.Length, input.Width);
            var mass = new Mass(rectangle, height);
            output.Model.AddElement(mass);
            return output;
        }
      }
}