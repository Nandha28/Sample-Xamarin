using System.Collections.Generic;
using Newtonsoft.Json;

namespace MapIntegration
{

public class GeocodedWaypoint
	{
		[JsonProperty(PropertyName = "geocoder_status", NullValueHandling = NullValueHandling.Ignore)]
		public string Geocoder_status { get; set; }
		[JsonProperty(PropertyName = "place_id", NullValueHandling = NullValueHandling.Ignore)]
		public string Place_id { get; set; }
		[JsonProperty(PropertyName = "types", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Types { get; set; }
	}

	public class Northeast
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class Southwest
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class Bounds
	{
		[JsonProperty(PropertyName = "northeast", NullValueHandling = NullValueHandling.Ignore)]
		public Northeast Northeast { get; set; }
		[JsonProperty(PropertyName = "southwest", NullValueHandling = NullValueHandling.Ignore)]
		public Southwest Southwest { get; set; }
	}

	public class Distance
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }
		[JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
		public int Value { get; set; }
	}

	public class Duration
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }
		[JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
		public int Value { get; set; }
	}

	public class EndLocation
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class StartLocation
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class Distance2
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }
		[JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
		public int Value { get; set; }
	}

	public class Duration2
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }
		[JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
		public int Value { get; set; }
	}

	public class EndLocation2
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class Polyline
	{
		[JsonProperty(PropertyName = "points", NullValueHandling = NullValueHandling.Ignore)]
		public string Points { get; set; }
	}

	public class StartLocation2
	{
		[JsonProperty(PropertyName = "lat", NullValueHandling = NullValueHandling.Ignore)]
		public double Lat { get; set; }
		[JsonProperty(PropertyName = "lng", NullValueHandling = NullValueHandling.Ignore)]
		public double Lng { get; set; }
	}

	public class Step
	{
		[JsonProperty(PropertyName = "distance", NullValueHandling = NullValueHandling.Ignore)]
		public Distance2 Distance { get; set; }
		[JsonProperty(PropertyName = "duration", NullValueHandling = NullValueHandling.Ignore)]
		public Duration2 duration { get; set; }
		[JsonProperty(PropertyName = "end_location", NullValueHandling = NullValueHandling.Ignore)]
		public EndLocation2 end_location { get; set; }
		[JsonProperty(PropertyName = "html_instructions", NullValueHandling = NullValueHandling.Ignore)]
		public string html_instructions { get; set; }
		[JsonProperty(PropertyName = "polyline", NullValueHandling = NullValueHandling.Ignore)]
		public Polyline polyline { get; set; }
		[JsonProperty(PropertyName = "start_location", NullValueHandling = NullValueHandling.Ignore)]
		public StartLocation2 start_location { get; set; }
		[JsonProperty(PropertyName = "travel_mode", NullValueHandling = NullValueHandling.Ignore)]
		public string Travel_mode { get; set; }
		[JsonProperty(PropertyName = "maneuver", NullValueHandling = NullValueHandling.Ignore)]
		public string Maneuver { get; set; }
	}

	public class Leg
	{
		[JsonProperty(PropertyName = "distance", NullValueHandling = NullValueHandling.Ignore)]
		public Distance Distance { get; set; }
		[JsonProperty(PropertyName = "duration", NullValueHandling = NullValueHandling.Ignore)]
		public Duration Duration { get; set; }
		[JsonProperty(PropertyName = "end_address", NullValueHandling = NullValueHandling.Ignore)]
		public string End_address { get; set; }
		[JsonProperty(PropertyName = "end_location", NullValueHandling = NullValueHandling.Ignore)]
		public EndLocation End_location { get; set; }
		[JsonProperty(PropertyName = "start_address", NullValueHandling = NullValueHandling.Ignore)]
		public string Start_address { get; set; }
		[JsonProperty(PropertyName = "start_location", NullValueHandling = NullValueHandling.Ignore)]
		public StartLocation Start_location { get; set; }
		[JsonProperty(PropertyName = "steps", NullValueHandling = NullValueHandling.Ignore)]
		public List<Step> Steps { get; set; }
		[JsonProperty(PropertyName = "traffic_speed_entry", NullValueHandling = NullValueHandling.Ignore)]
		public List<object> Traffic_speed_entry { get; set; }
		[JsonProperty(PropertyName = "via_waypoint", NullValueHandling = NullValueHandling.Ignore)]
		public List<object> Via_waypoint { get; set; }
	}

	public class OverviewPolyline
	{
		[JsonProperty(PropertyName = "points", NullValueHandling = NullValueHandling.Ignore)]
		public string Points { get; set; }
	}

	public class Route
	{
		[JsonProperty(PropertyName = "bounds", NullValueHandling = NullValueHandling.Ignore)]
		public Bounds Bounds { get; set; }
		[JsonProperty(PropertyName = "copyrights", NullValueHandling = NullValueHandling.Ignore)]
		public string Copyrights { get; set; }
		[JsonProperty(PropertyName = "legs", NullValueHandling = NullValueHandling.Ignore)]
		public List<Leg> Legs { get; set; }
		[JsonProperty(PropertyName = "overview_polyline", NullValueHandling = NullValueHandling.Ignore)]
		public OverviewPolyline Overview_polyline { get; set; }
		[JsonProperty(PropertyName = "summary", NullValueHandling = NullValueHandling.Ignore)]
		public string Summary { get; set; }
		[JsonProperty(PropertyName = "warnings", NullValueHandling = NullValueHandling.Ignore)]
		public List<object> Warnings { get; set; }
		[JsonProperty(PropertyName = "waypoint_order", NullValueHandling = NullValueHandling.Ignore)]
		public List<object> Waypoint_order { get; set; }
	}

	public class RequsetHandler
	{
		[JsonProperty(PropertyName="geocoded_waypoints",NullValueHandling = NullValueHandling.Ignore)]
		public List<GeocodedWaypoint> Geocoded_waypoints { get; set; }
		[JsonProperty(PropertyName = "routes", NullValueHandling = NullValueHandling.Ignore)]
		public List<Route> Routes { get; set; }
		[JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
		public string Status { get; set; }
	}

}