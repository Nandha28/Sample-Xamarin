using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapIntegration
{
	public class CustomMap : Map
	{
		//This Property is for Adding the Coordinates to draw the route and to tigger the onelementpropertychanged
		public static readonly BindableProperty PolylineCoordinatesProperty = BindableProperty.Create("PolylineCoordinates", typeof(ObservableCollection<Position>), typeof(CustomMap), null, BindingMode.TwoWay, propertyChanged:
		(bindable, oldValue, newValue) =>
		{
			((CustomMap)bindable).PolylineCoordinates = (ObservableCollection<Position>)newValue;
		});

		public ObservableCollection<Position> PolylineCoordinates
		{
			get
			{
				return (ObservableCollection<Position>)GetValue(PolylineCoordinatesProperty);
			}
			set

			{
				SetValue(PolylineCoordinatesProperty, value);
			}
		}
		//This is for our user color for polylines
		public static readonly BindableProperty PolylineColorProperty = BindableProperty.Create("PolylineColor", typeof(Color), typeof(CustomMap), default(Color));
		public Color PolylineColor
		{
			get
			{
				return (Color)GetValue(PolylineColorProperty);
			}
			set
			{
				SetValue(PolylineColorProperty, value);
			}
		}
		//public static void OnChangedProperty(BindableObject binding,object oldValue,object newValue)
		//{
		//	((CustomMap)binding).OnPolyLineAddressPointsPropertyChanged();

		//	//this.OnPolyLineAddressPointsPropertyChanged((List<Position>)oldValue,(List<Position>)newValue);
		//}

		//public event PropertyChangedEventHandler PropertyChanged;
		//public void OnPolyLineAddressPointsPropertyChanged()
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PolylineCoordinates"));
		//}
		public CustomMap()
		{
			Debug.WriteLine("Custom Called");

		}

	}
}
