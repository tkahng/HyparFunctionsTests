using Elements;
using Elements.Geometry;
using System.Collections.Generic;

namespace InputTest
{
    public static class InputTest
    {
        /// <summary>
        /// The InputTest function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A InputTestOutputs instance containing computed results and the model with any new elements.</returns>
        public static InputTestOutputs Execute(
            Dictionary<string, Model> inputModels,
            InputTestInputs input
        )
        {
            var output = new InputTestOutputs();
            var transform = new Transform();
            foreach (var s in input.Programs)
            {
                var program = new ProgramType(s.Program.Color, s.Program.Name);
                output.Model.AddElement(program);
                var material = new Material(s.Program.Name, program.ProgramColor);
                foreach (var spaces in s.Spaces)
                {
                    transform = transform.Moved(20, 0, 0);
                    var w = Math.Sqrt(spaces.Area);
                    var profile = new Profile(Polygon.Rectangle(w, w));
                    var space = new FloorProgramArea(
                        profile,
                        program,
                        spaces.Area,
                        new Transform(),
                        material,
                        new Representation(new Elements.Geometry.Solids.Lamina(profile.Perimeter))
                    );
                    space.IsElementDefinition = true;
                    output.Model.AddElement(space);
                    var t = new Transform(transform);
                    for (int i = 0; i < spaces.Count; i++)
                    {
                        t = t.Moved(0, 20, 0);
                        var instance = space.CreateInstance(t, program.Name);
                        output.Model.AddElement(instance);
                    }
                }
            }

            return output;
        }
    }
}
