using System;
using System.Linq;
using System.Collections.Generic;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using GeometryEx;
using RoomKit;


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
            List<ModelCurve> modelCurves = new List<ModelCurve>();
            List<Curve> Curves = new List<Curve>();
            List<Polygon> Polygons = new List<Polygon>();
            
            var bndry = input.Outline;
            var spine = bndry.Spine();
            var jig = bndry.Jigsaw();
            var skel = bndry.Skeleton();
            var height = 1.0;
            var volume = input.Length * input.Width * height;
            var output = new TestFunction2Outputs(1.0);
            var rectangle = Polygon.Rectangle(input.Length, input.Width);
            var mass = new Mass(bndry, height);
            Polygons.AddRange(new[] { bndry });
            var bbox = new List<Polygon>();
            foreach (var item in jig)
            {
                bbox.Add(item.AlignedBox());
            }
            var clips = new List<Polygon>();
            for (int i = 0; i < bbox.Count - 1; i++)
            {
                clips.AddRange(Shaper.Differences(bbox[i].ToList(), bbox[i+1].ToList()));
            }

            var masses = new List<Mass>();

            for (var i = 0; i < clips.Count; i++)
            {
                masses.Add(new Mass(clips[i], 1+i, name: i.ToString()));
            }
            
            // Curves.AddRange(spine);
            // Curves.AddRange(jig);
            Curves.AddRange(clips);
            // Curves.AddRange(bbox);
            
            foreach(var crv in Curves){
              modelCurves.Add(new ModelCurve(crv));
            }

            foreach(var crv in Polygons){
              modelCurves.Add(new ModelCurve(crv));
            }
            // modelCurves[0].
            output.Model.AddElements(modelCurves);
            output.Model.AddElements(masses);
            return output;
        }
      }
}