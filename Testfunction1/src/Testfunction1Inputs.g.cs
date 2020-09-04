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

namespace Testfunction1
{
    public class Testfunction1Inputs: S3Args
    {
		/// <summary>
		/// The Offset Distance.
		/// </summary>
		[JsonProperty("Offset Width")]
		public double OffsetWidth {get;}

		/// <summary>
		/// The width of the Bridge.
		/// </summary>
		[JsonProperty("Bridge Width")]
		public double BridgeWidth {get;}

		/// <summary>
		/// The height of the Bridge.
		/// </summary>
		[JsonProperty("Bridge Height")]
		public double BridgeHeight {get;}

		/// <summary>
		/// the bridge's material.
		/// </summary>
		[JsonProperty("Material")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Material Material {get;}

		/// <summary>
		/// The Center Line of the Bridge
		/// </summary>
		[JsonProperty("Center Line")]
		public Elements.Geometry.Polyline CenterLine {get;}



        /// <summary>
        /// Construct a Testfunction1Inputs with default inputs.
        /// This should be used for testing only.
        /// </summary>
        public Testfunction1Inputs() : base()
        {
			this.OffsetWidth = 10;
			this.BridgeWidth = 10;
			this.BridgeHeight = 10;
			this.Material = Material.Wood;
			this.CenterLine = new Elements.Geometry.Polyline(new []{new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,1,0)});

        }


        /// <summary>
        /// Construct a Testfunction1Inputs specifying all inputs.
        /// </summary>
        /// <returns></returns>
        [JsonConstructor]
        public Testfunction1Inputs(double offsetWidth, double bridgeWidth, double bridgeHeight, Material material, Elements.Geometry.Polyline centerLine, string bucketName, string uploadsBucket, Dictionary<string, string> modelInputKeys, string gltfKey, string elementsKey, string ifcKey): base(bucketName, uploadsBucket, modelInputKeys, gltfKey, elementsKey, ifcKey)
        {
			this.OffsetWidth = offsetWidth;
			this.BridgeWidth = bridgeWidth;
			this.BridgeHeight = bridgeHeight;
			this.Material = material;
			this.CenterLine = centerLine;

		}

		public override string ToString()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}
	}
}