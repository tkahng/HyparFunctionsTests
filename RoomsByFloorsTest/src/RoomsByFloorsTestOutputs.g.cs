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

namespace RoomsByFloorsTest
{
    public class RoomsByFloorsTestOutputs: ResultsBase
    {
		/// <summary>
		/// Quantity of rooms on the ground floor.
		/// </summary>
		[JsonProperty("Ground Floor Room Quantity")]
		public double GroundFloorRoomQuantity {get;}

		/// <summary>
		/// Area for the ground floor.
		/// </summary>
		[JsonProperty("Ground Floor Area")]
		public double GroundFloorArea {get;}

		/// <summary>
		/// Quantity of rooms on the ground floor.
		/// </summary>
		[JsonProperty("Typical Floor Room Quantity")]
		public double TypicalFloorRoomQuantity {get;}

		/// <summary>
		/// Quantity of rooms on all upper floors.
		/// </summary>
		[JsonProperty("Upper Floors Room Quantity")]
		public double UpperFloorsRoomQuantity {get;}

		/// <summary>
		/// Total area for all upper floors.
		/// </summary>
		[JsonProperty("Upper Floor Total Area")]
		public double UpperFloorTotalArea {get;}


        
        /// <summary>
        /// Construct a RoomsByFloorsTestOutputs with default inputs.
        /// This should be used for testing only.
        /// </summary>
        public RoomsByFloorsTestOutputs() : base()
        {

        }


        /// <summary>
        /// Construct a RoomsByFloorsTestOutputs specifying all inputs.
        /// </summary>
        /// <returns></returns>
        [JsonConstructor]
        public RoomsByFloorsTestOutputs(double groundfloorroomquantity, double groundfloorarea, double typicalfloorroomquantity, double upperfloorsroomquantity, double upperfloortotalarea): base()
        {
			this.GroundFloorRoomQuantity = groundfloorroomquantity;
			this.GroundFloorArea = groundfloorarea;
			this.TypicalFloorRoomQuantity = typicalfloorroomquantity;
			this.UpperFloorsRoomQuantity = upperfloorsroomquantity;
			this.UpperFloorTotalArea = upperfloortotalarea;

		}

		public override string ToString()
		{
			var json = JsonConvert.SerializeObject(this);
			return json;
		}
	}
}