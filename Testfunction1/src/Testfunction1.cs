using System;
using System.Linq;
using System.Collections.Generic;
using Elements;
using Elements.Geometry;
using Elements.Serialization.glTF;
using GeometryEx;
using RoomKit;
using Elements.Geometry.Profiles;
using Elements.Spatial;



namespace Testfunction1
{
      public static class Testfunction1
    {
        /// <summary>
        /// The Testfunction1 function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A Testfunction1Outputs instance containing computed results and the model with any new elements.</returns>
        public static Testfunction1Outputs Execute(Dictionary<string, Model> inputModels, Testfunction1Inputs input)
        {
            /// Your code here.
            var output = new Testfunction1Outputs(1.0);
            List<ModelCurve> modelCurves = new List<ModelCurve>();
            List<Curve> Curves = new List<Curve>();
            Elements.Material brdigeMaterial = null;
            switch(input.Material)
            {
              case Material.Red:
                brdigeMaterial = new Elements.Material("Red", Colors.Red, 0.0f, 0.0f);
                break;
              case Material.Steel:
                brdigeMaterial = BuiltInMaterials.Steel;
                break;
              case Material.Wood:
                brdigeMaterial = BuiltInMaterials.Wood;
                break;              
            }
            var profile = WideFlangeProfileServer.Instance.GetProfileByType(WideFlangeProfileType.W10x100);
            var pts = new List<Vector3>();
            var centerCrv = input.CenterLine;
            var offsetCrv = input.CenterLine.Offset(input.OffsetWidth, EndType.Square)[0];
            var offsetCrv2 = input.CenterLine.Offset(input.OffsetWidth/2, EndType.Square)[0];
            var rebuildOffsetCrv = new Polygon(offsetCrv.Vertices);
            var LGon = Polygon.L(15, 15, 5);
            var ltransform = new Transform(15, 0, 0);
            List<Polygon> testPoly = new List<Polygon>();
            testPoly.AddRange(new[] {rebuildOffsetCrv, Polygon.Rectangle(15,15), ltransform.OfPolygon(LGon)});

            foreach(var poly in testPoly){
              var beam = new Beam(poly, Polygon.Rectangle(input.BridgeWidth, input.BridgeHeight), brdigeMaterial);
              // var beam = new Beam(poly, profile, brdigeMaterial);
              output.Model.AddElement(beam);
              var grid = new Grid1d(poly);

              grid.DivideByApproximateLength(3);

              foreach(var pt in grid.GetCellSeparators())
              {
                var column = new Column(pt, 5, Polygon.Rectangle(0.1, 0.1));
                output.Model.AddElement(column);
              }              
            }
            var secLine = new Line(new Vector3(-input.OffsetWidth*2, 0, 0), new Vector3(input.OffsetWidth*2, 0, 0));
            // List<Plane> planes = new List<Plane>();
            List<Line> secLines = new List<Line>();


            var crvSegments = centerCrv.Segments();
            foreach(var crv in crvSegments){
              var grid = new Grid1d(crv);

              grid.DivideByApproximateLength(3);

              foreach(var pt in grid.GetCellSeparators())
              {
                // planes.Add(new Plane(pt, Vector3.XAxis(pt)))
                // pts.Add(pt);
                var transform = new Transform(pt, crv.Direction());
                secLines.Add(transform.OfLine(secLine));
                // planes.Add(new Plane(pt, crv.Direction()));
                var column = new Column(pt, 5, Polygon.Rectangle(0.1, 0.1));
                output.Model.AddElement(column);
              }
            }
            
            Curves.AddRange(new[] {centerCrv, offsetCrv, (Curve)secLine});
            Curves.AddRange(secLines);
            foreach(var crv in Curves){
              modelCurves.Add(new ModelCurve(crv));
            }
            // var pink = new Elements.Material("pink", Colors.Pink);
            // var modelPoints = new ModelPoints(pts, pink);


            // var crvs = new List<Curve>{Polygon.Rectangle(10,10), offsetCrv[0]};

            // foreach(var crv in crvs) {
            //   modelCurves.Add(new ModelCurve(crv));
            //   var grid = new Grid1d(crv);
            //   grid.DivideByApproximateLength(1.0);
            //   foreach(var pt in grid.GetCellSeparators())
            //   {
            //     var column = new Column(pt, 5, Polygon.Rectangle(0.05, 0.05));
            //     output.Model.AddElement(column);
            //   }

            // }
            // output.Model.AddElements(modelPoints);
            output.Model.AddElements(modelCurves);
            // output.Model.AddElement(beam);
            return output;
        }
      }
}