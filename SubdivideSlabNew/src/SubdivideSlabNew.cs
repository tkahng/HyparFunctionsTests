using Elements;
using Elements.Spatial;
using Elements.Geometry;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Elements.Geometry.Solids;
using Newtonsoft.Json;

namespace SubdivideSlabNew
{
      public static class SubdivideSlabNew
    {
        /// <summary>
        /// The SubdivideSlabNew function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A SubdivideSlabNewOutputs instance containing computed results and the model with any new elements.</returns>
        public static SubdivideSlabNewOutputs Execute(Dictionary<string, Model> inputModels, SubdivideSlabNewInputs input)
        {
            var model = new Model();
            var allFloors = new List<Floor>();
            inputModels.TryGetValue("Floors", out var flrModel);
            if (flrModel == null || flrModel.AllElementsOfType<Floor>().Count() == 0)
            {
                throw new ArgumentException("No Floors found.");
            }
            allFloors.AddRange(flrModel.AllElementsOfType<Floor>());
            List<SlabSubdivision> subdivisions = new List<SlabSubdivision>();
            List<ModelCurve> modelCurves = new List<ModelCurve>();
            List<Column> columns = new List<Column>();

            for (int i = 0; i < allFloors.Count; i++)
            {
                Floor floor = allFloors[i];
                var floorId = StringExtensions.NumberToString(i);
                var profile = floor.Profile;
                var perimeter = profile.Perimeter;
                var voids = profile.Voids;
                var elevation = floor.Elevation;
                var boundaries = new List<Polygon>();
                boundaries.Add(perimeter);
                if (voids != null) boundaries.AddRange(voids);
                Transform transform = null;
                if (input.AlignToLongestEdge)
                {
                    var longestEdge = perimeter.Segments().OrderByDescending(p => p.Length()).First();
                    var xAxis = (longestEdge.End - longestEdge.Start).Unitized();
                    transform = new Transform(Vector3.Origin, xAxis, Vector3.ZAxis, 0);
                    transform.Invert();
                }
                var grid = new Grid2d(boundaries, transform);
                if (input.SubdivideAtVoidCorners && voids != null && voids.Count > 0)
                {
                    foreach (var voidCrv in voids)
                    {
                        grid.SplitAtPoints(voidCrv.Vertices);
                    }
                    foreach (var cell in grid.CellsFlat)
                    {
                        cell.U.DivideByApproximateLength(input.Length, EvenDivisionMode.RoundUp);
                        cell.V.DivideByApproximateLength(input.Width, EvenDivisionMode.RoundUp);
                    }
                }
                else
                {
                    grid.U.DivideByApproximateLength(input.Length, EvenDivisionMode.RoundUp);
                    grid.V.DivideByApproximateLength(input.Width, EvenDivisionMode.RoundUp);
                }

                var cells = grid.GetCells();

                // foreach(var pt in grid.U.GetCellSeparators())
                // {
                // var column = new Column(new Vector3(pt.X, pt.Y, elevation), 5, Polygon.Rectangle(1.0, 1.0));
                // columns.Add(column);
                // }

                for (int i1 = 0; i1 < cells.Count; i1++)
                {
                    var id = $"{floorId}-{i1:000}";
                    Grid2d cell = cells[i1];
                    var cellCrvs = cell.GetTrimmedCellGeometry();
                    var isTrimmed = cell.IsTrimmed();
                    if (cellCrvs != null && cellCrvs.Length > 0)
                    {
                        subdivisions.Add(CreateSlabSubdivision(id, cellCrvs, floor, isTrimmed));
                        // var column = new Column(pt, 5, Polygon.Rectangle(0.05, 0.05));
                        // output.Model.AddElement(column);
                        var outerBoundary = cellCrvs.First();
                        var polygon = (Polygon)outerBoundary;
                        var pt = polygon.Centroid();
                        var column = new Column(new Vector3(pt.X, pt.Y, elevation), 5, Polygon.Rectangle(1.0, 1.0));
                        columns.Add(column);
                    }
                    foreach(var crv in cellCrvs)
                    {
                        modelCurves.Add(ToModelCurve(crv, elevation+1.0));
                    }                    
                }
            }

            var output = new SubdivideSlabNewOutputs(subdivisions.Count, columns.Count);
            output.Model = model;
            // output.Model.AddElements(subdivisions);
            output.Model.AddElements(modelCurves);
            output.Model.AddElements(columns);
            return output;
        }

        private static ModelCurve ToModelCurve(Curve curve, double elevation)
        {
            if (curve == null) return null;
            return new ModelCurve(curve, null, new Transform(0, 0, elevation));
        }

        private static SlabSubdivision CreateSlabSubdivision(string ID, IList<Curve> boundaries, Floor floor, bool isTrimmed)
        {
            var outerBoundary = boundaries.First();
            var polygon = (Polygon)outerBoundary;
            var profile = new Profile(polygon);
            if (boundaries.Count > 1)
            {
                profile.Voids = new List<Polygon>();
                for (int i = 1; i < boundaries.Count; i++)
                {
                    profile.Voids.Add((Polygon)boundaries[i]);
                }
            }
            var depth = floor.Thickness;
            var transform = new Transform(0, 0, GetFloorElevation(floor) - depth);
            var extrude = new Extrude(profile, depth, new Vector3(0, 0, 1), false);
            var geomRep = new Representation(new[] { extrude });
            var material = BuiltInMaterials.Concrete;
            var volume = polygon.Area() * depth;
            return new SlabSubdivision(ID, profile, isTrimmed, depth, volume, transform, material, geomRep, false, Guid.NewGuid(), "");
        }
        private static double GetFloorElevation(Floor floor)
        {
            var profile = floor.ProfileTransformed();
            return profile.Perimeter.Vertices.First().Z + floor.Thickness + 0.1;

        }
      }
}