using Elements;
using Elements.Geometry;
using System.Linq;
using System;
using System.Collections.Generic;
using GeometryEx;

namespace SpaceSubdivide2
{
      public static class SpaceSubdivide2
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A SpaceSubdivide2Outputs instance containing computed results and the model with any new elements.</returns>
        public static SpaceSubdivide2Outputs Execute(Dictionary<string, Model> inputModels, SpaceSubdivide2Inputs input)
        {
            var output = new SpaceSubdivide2Outputs();

            Elements.Geometry.Solids.Extrude extrude;
            Representation geomRep;
            var envelopes = new List<Envelope>();
            var envMatl = new Material("envelope", new Color(0.3, 0.7, 0.7, 0.6), 0.0f, 0.0f);



            List<ModelCurve> modelCurves = new List<ModelCurve>();
            List<Polygon> offsetcrvs = new List<Polygon>();
            List<Polygon> jigs = new List<Polygon>();
            var centerlines = input.CenterLines;
            var sketch = input.Sketch;
            // var outlines = new List<Polyline>();
            // var stuff = new 
            foreach (var item in centerlines)
            {
                offsetcrvs.Add(item.Offset(2, EndType.Square).First());
            }
            offsetcrvs.Add(sketch);

            foreach (var item in offsetcrvs)
            {
                jigs.AddRange(item.Jigsaw());
            }

            foreach(var crv in offsetcrvs){
              modelCurves.Add(new ModelCurve(crv));
            }

            foreach(var crv in jigs){
              modelCurves.Add(new ModelCurve(crv));
            }



            var BuildingHeight = 30;
            foreach (var item in offsetcrvs)
            {
              extrude = new Elements.Geometry.Solids.Extrude(item, BuildingHeight, Vector3.ZAxis, false);
              geomRep = new Representation(new List<Elements.Geometry.Solids.SolidOperation>() { extrude });
              envelopes.Add(new Envelope(item, 0.0, BuildingHeight, Vector3.ZAxis, 0.0,
                            new Transform(), envMatl, geomRep, false, Guid.NewGuid(), ""));
                
            }





            output.Model.AddElements(envelopes);
            output.Model.AddElements(modelCurves);




            return output;
        }
      }
}