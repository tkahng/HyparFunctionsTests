// This code was generated by Hypar.
// Edits to this code will be overwritten the next time you run 'hypar init'.
// DO NOT EDIT THIS FILE.

using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Hypar.Functions;
using Hypar.Functions.Execution;
using Hypar.Functions.Execution.AWS;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace EnvelopBySketchNew
{
    public class EnvelopBySketchNewOutputs: ResultsBase
    {
		/// <summary>
		/// Height of the building.
		/// </summary>
		[JsonProperty("Height")]
		public double Height {get;}

		/// <summary>
		/// Depth of the subgrade section.
		/// </summary>
		[JsonProperty("Subgrade")]
		public double Subgrade {get;}


        
        /// <summary>
        /// Construct a EnvelopBySketchNewOutputs with default inputs.
        /// This should be used for testing only.
        /// </summary>
        public EnvelopBySketchNewOutputs() : base()
        {

        }


        /// <summary>
        /// Construct a EnvelopBySketchNewOutputs specifying all inputs.
        /// </summary>
        /// <returns></returns>
        [JsonConstructor]
        public EnvelopBySketchNewOutputs(double height, double subgrade): base()
        {
			this.Height = height;
			this.Subgrade = subgrade;

		}

		public override string ToString()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}
	}
}