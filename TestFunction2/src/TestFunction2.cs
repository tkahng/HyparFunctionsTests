using Elements;
using Elements.Geometry;
using System.Collections.Generic;

namespace TestFunction2
{
      public static class TestFunction2
    {
        /// <summary>
        /// The TestFunction2 function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A TestFunction2Outputs instance containing computed results and the model with any new elements.</returns>
        public static TestFunction2Outputs Execute(Dictionary<string, Model> inputModels, TestFunction2Inputs input)
        {
            /// Your code here.
            var height = 1.0;
            var volume = input.Length * input.Width * height;
            var output = new TestFunction2Outputs(volume);
            var rectangle = Polygon.Rectangle(input.Length, input.Width);
            var mass = new Mass(rectangle, height);
            output.Model.AddElement(mass);
            return output;
        }
      }
}