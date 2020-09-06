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
            var levels = new List<LevelPerimeter>();
            var walls = new List <StandardWall>();
            List<ModelCurve> modelCurves = new List<ModelCurve>();
            inputModels.TryGetValue("Levels", out var lvlModel);
            if (lvlModel != null)
            {
                levels.AddRange(lvlModel.AllElementsOfType<LevelPerimeter>());
            }
            if (levels.Count == 0)
            {
                throw new ArgumentException("No LevelPerimeters found.");
            }
            levels = levels.OrderBy(l => l.Elevation).ToList();
            for (var i = 0; i < levels.Count - 1; i++)
            {
                List<Line> segs = new List <Line>();
                var perimeter = levels[i].Perimeter;
                segs.AddRange(perimeter.Segments());
                
                var elevation = levels[i].Elevation;
                var height = (levels[i + 1].Elevation - levels[i].Elevation); // replace this number with a plenum height
                foreach (var line in segs)
                {
                    modelCurves.Add(new ModelCurve(line));
                    walls.Add(new StandardWall(line, 0.2, 3.0, transform: new Transform(0, 0, elevation)));
                }
            }
            // List<Line> Segments = new List <Line>();
            // var Segments = new List<Line>();
            // var Segments = new List<Line>();
            // Segments.AddRange(input.Sketch.Segments());
            // foreach (var ln in Segments)
            // {
            //     walls.Add(new StandardWall(ln, 0.2, 10.0));
            // }
            // Segments.
            // walls.Add(new StandardWall(input.Sketch, 0.2, 10.0));
            var volume = walls.Count;
            var output = new WallsByPerimeterOutputs(volume);
            // var rectangle = Polygon.Rectangle(10.0, 10.0);
            // var mass = new Mass(rectangle, 1.0);
            // output.Model.AddElement(mass);
            output.Model.AddElements(modelCurves);
            output.Model.AddElements(walls);
            return output;
        }
      }
}