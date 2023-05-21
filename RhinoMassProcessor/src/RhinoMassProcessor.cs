using Elements;
using DeepFormCore.Rhino;

namespace RhinoMassProcessor
{
    public static class RhinoMassProcessor
    {
        /// <summary>
        /// The RhinoMassProcessor function.
        /// </summary>
        /// <param name="model">The input model.</param>
        /// <param name="input">The arguments to the execution.</param>
        /// <returns>A RhinoMassProcessorOutputs instance containing computed results and the model with any new elements.</returns>
        public static RhinoMassProcessorOutputs Execute(
            Dictionary<string, Model> inputModels,
            RhinoMassProcessorInputs input
        )
        {
            /// Your code here.
            var key = "Resources/mass-test.3dm";
            var rhinogen = new RhinoSpaceElementFunction();
            rhinogen.GenerateProjectFromFile(key);
            var output = new RhinoMassProcessorOutputs((double)rhinogen.SpaceElements.Count);
            output.Model.AddElements(rhinogen.LayersDict.Select(s => s.Value).ToList());
            output.Model.AddElements(rhinogen.LevelsDict.Select(s => s.Value).ToList());
            output.Model.AddElements(rhinogen.SpaceElements.ToList());
            return output;
        }
    }
}
