using System;
using CoreLocation;
using MapIntegration;
using MapIntegration.iOS;
using MapKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapIntegration.iOS
{
	public class CustomMapRenderer : MapRenderer
	{
		MKMapView nativeMap;
		MKPolyline route;
		MKPolylineRenderer polylineRenderer;


		public CustomMapRenderer()
		{
			Console.WriteLine("IOS Renderer is called");
		}

		//Initialize to draw custom map
		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				nativeMap = Control as MKMapView;

				//Showing location to the user
				nativeMap.ShowsUserLocation = true;

				//Requst user authorization
				CLLocationManager locationmanager = new CLLocationManager();
				locationmanager.RequestAlwaysAuthorization();
				//locationmanager.Delegate = ;

				//Redirect to user location
				nativeMap.DidUpdateUserLocation += (sender, ex) =>
				{
					if (nativeMap.UserLocation != null)
					{
						CLLocationCoordinate2D cords = ex.UserLocation.Coordinate;
						MKCoordinateSpan span = new MKCoordinateSpan();
						nativeMap.Region = new MKCoordinateRegion(cords, span);
					}
				};
			}
		}
		//This is simplifed as anonymous function
		//MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		//{
		//		var custommap = ((CustomMap)Element);
		//		var polylineonbject = ObjCRuntime.Runtime.GetNSObject(overlay.Handle) as MKPolyline;
		//		polylineRenderer = new MKPolylineRenderer(polylineonbject);
		//		polylineRenderer.FillColor = custommap.PolylineColor.ToUIColor();
		//		polylineRenderer.StrokeColor = custommap.PolylineColor.ToUIColor();
		//		polylineRenderer.LineWidth = 5;
		//	return polylineRenderer;
		//}


		//
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == CustomMap.PolylineCoordinatesProperty.PropertyName)
			{

				AddingPolyline();
			}
		}

		void AddingPolyline()
		{
			var customMapElement = ((CustomMap)Element);
			nativeMap = Control as MKMapView;
			if (customMapElement != null && customMapElement.PolylineCoordinates.Count != 0)
			{
				CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[customMapElement.PolylineCoordinates.Count];
				//MKPolyline line = new MKPolyline();
				//nativeMap.OverlayRenderer = GetOverlayRenderer;

				//get the polyline properties
				nativeMap.OverlayRenderer = (sender, EventArgs) =>
				{
					polylineRenderer = new MKPolylineRenderer(EventArgs as MKPolyline);
					polylineRenderer.StrokeColor = customMapElement.PolylineColor.ToUIColor();
					polylineRenderer.FillColor = customMapElement.PolylineColor.ToUIColor();
					polylineRenderer.LineWidth = 5;
					return polylineRenderer;
				};
				int intex = 0;

				//Remove added line
				if (route != null)
				{
					nativeMap.RemoveOverlay(route);
				}

				//addding each latandlong from polylinecoordinates to the CLLocationCoordinate2D(polylines)
				foreach (var position in customMapElement.PolylineCoordinates)
				{
					coords[intex] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
					intex++;
				}
				//Adding route
				route = MKPolyline.FromCoordinates(coords);
				nativeMap.AddOverlay(route);
			}
		}
	}
}
