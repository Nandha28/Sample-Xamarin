using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MapIntegration;
using MapIntegration.Droid;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapIntegration.Droid
{
	public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
	{
		public GoogleMap formMap;
		//ctor
		public CustomMapRenderer()
		{
			Console.WriteLine("Wow Sucesss");
		}
		// Called when getmapasyn() is called
		public void OnMapReady(GoogleMap googleMap)
		{
			formMap = googleMap;
			formMap.UiSettings.ZoomControlsEnabled = true;
			//to move to user location
			MoveMap();
		}

		async void MoveMap()
		{
			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;
				var postin = await locator.GetPositionAsync(10000);
				Console.WriteLine(postin.Latitude + " " + postin.Longitude);
				LatLng latlng = new LatLng(postin.Latitude, postin.Longitude);
				CameraUpdate camera = CameraUpdateFactory.NewLatLng(latlng);
				formMap.MoveCamera(camera);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		//Initialize the custom map
		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				var map = Element as CustomMap;

				map.IsShowingUser = true;

				((MapView)Control).GetMapAsync(this);
			}
		}
		//To draw the route when coordinates are added to the polylinecoordinates property
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == CustomMap.PolylineCoordinatesProperty.PropertyName)
			{
				Addline();
			}
			//Console.WriteLine(e.PropertyName);
		}

		void Addline()
		{
			var formsMap = ((CustomMap)Element);
			if (formsMap != null && formsMap.PolylineCoordinates.Count != 0)
			{
				//new polylineoption
				var polylineOptions = new PolylineOptions();

				//get the polyline color from xaml file
				polylineOptions.InvokeColor(((CustomMap)Element).PolylineColor.ToAndroid());

				//Adding the coordinates from viewmodal to the native polylines
				foreach (var position in formsMap.PolylineCoordinates)
				{
					polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
				}

				//Remove alreadry added lines
				if (formMap != null)
				{
					formMap.Clear();
				}
				//"Hero off the code to add polylines to map"
				formMap.AddPolyline(polylineOptions);
			}
		}

	}
}