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

namespace EnvelopeByCenterlineTest
{
    public class EnvelopeByCenterlineTestInputs: S3Args
    {
		/// <summary>
		/// Centerline of the building envelope.
		/// </summary>
		[JsonProperty("Centerline")]
		public Elements.Geometry.Polyline Centerline {get;}

		/// <summary>
		/// Overall height of the building from grade.
		/// </summary>
		[JsonProperty("Building Height")]
		public double BuildingHeight {get;}

		/// <summary>
		/// Width of the mass perpendicular to the Centerline.
		/// </summary>
		[JsonProperty("Bar Width")]
		public double BarWidth {get;}

		/// <summary>
		/// Depth of the building envelope below grade.
		/// </summary>
		[JsonProperty("Foundation Depth")]
		public double FoundationDepth {get;}


        
        /// <summary>
        /// Construct a EnvelopeByCenterlineTestInputs with default inputs.
        /// This should be used for testing only.
        /// </summary>
        public EnvelopeByCenterlineTestInputs() : base()
        {
			this.Centerline = new Elements.Geometry.Polyline(new []{new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,1,0)});
			this.BuildingHeight = 100;
			this.BarWidth = 30;
			this.FoundationDepth = 20;

        }


        /// <summary>
        /// Construct a EnvelopeByCenterlineTestInputs specifying all inputs.
        /// </summary>
        /// <returns></returns>
        [JsonConstructor]
        public EnvelopeByCenterlineTestInputs(Elements.Geometry.Polyline centerline, double buildingheight, double barwidth, double foundationdepth, string bucketName, string uploadsBucket, Dictionary<string, string> modelInputKeys, string gltfKey, string elementsKey, string ifcKey): base(bucketName, uploadsBucket, modelInputKeys, gltfKey, elementsKey, ifcKey)
        {
			this.Centerline = centerline;
			this.BuildingHeight = buildingheight;
			this.BarWidth = barwidth;
			this.FoundationDepth = foundationdepth;

		}

		public override string ToString()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}
	}
}