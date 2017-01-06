using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapIntegration
{
	public class MapPageViewModal : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		CustomMap custommap;
		
		public MapPageViewModal()
		{
			Debug.WriteLine("ViewModal Called");
			//Initializing th list
			ListCoordinate = new ObservableCollection<Position>();

			//move the map to current location
			MoveMap();

		}

		//String EncodedPoints;
		ObservableCollection<Position> listCoordinate;
		public ObservableCollection<Position> ListCoordinate
		{
			get
			{
				return listCoordinate;
			}
			set
			{
				listCoordinate = value;
				OnPropertyChanged("ListCoordinate");
			}
		}

		//Hacking

		//void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		//{
		//	if (e.Action == NotifyCollectionChangedAction.Add)
		//	{
		//		foreach (Position item in e.NewItems)
		//		{
		//			OnPropertyChanged("ListCoordinate");
		//		}
		//	}
		//}

		//Travelling mode
		string mode;
		public string Mode
		{
			get
			{
				return mode;
			}
			set
			{
				mode = value;

			}
		}

		//GET Route Command
		ICommand getRouteCommand;
		public ICommand GetRouteCommand
		{
			get
			{
				return getRouteCommand = new Command(async () =>
				{
					await Createreq();
					//await DependencyService.Get<IRequest>().CreateReq(EncodedPoints);
					Debug.WriteLine("Request Completed");
				});
			}
		}
		string sourcePlace;
		string destinationPlace;

		//Say that particular property is changed
		void OnPropertyChanged(string property)
		{
			//PropertyChangedEventHandler handler =  PropertyChanged;
			//if (PropertyChanged != null)
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
			//else
			//	Debug.WriteLine("Property is null");
		}

		public string SourcePlace
		{
			get
			{
				return sourcePlace;
			}
			set
			{
				sourcePlace = value;
				//OnPropertyChanged("SourcePlace");
			}
		}
		public string DestinationPlace
		{
			get
			{
				return destinationPlace;
			}
			set
			{
				destinationPlace = value;
				//OnPropertyChanged("DestinationPlace");
			}
		}

		//Move the map to user current location
		async void MoveMap()
		{
			try
			{
				var loc = CrossGeolocator.Current;
				var position = await loc.GetPositionAsync(10000);
				loc.DesiredAccuracy = 50;
				custommap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(1)));
			}
			catch(Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		//Create the requset
		public async Task Createreq()
		{
			if (SourcePlace != null && DestinationPlace != null)
			{
				var req = WebRequest.Create("https://maps.googleapis.com/maps/api/directions/json?origin=" + SourcePlace + "&mode=" + Mode + "&destination=" + DestinationPlace + "&key=AIzaSyAJo_ypk9r7qRrgD4BgNSBfqRmgmglsnOY");
				req.ContentType = "application/json";
				req.Method = "GET";
				using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
				{
					if (res.StatusCode == HttpStatusCode.OK)
					{
						using (var reader = new StreamReader(res.GetResponseStream()))
						{
							RequsetHandler latlong = JsonConvert.DeserializeObject<RequsetHandler>(reader.ReadToEnd());
							if (latlong.Routes.Count > 0)
							{
								//The response as strings
								var EncodedPoints = latlong.Routes[0].Overview_polyline.Points;

								//to decode the strings to latandlong
								var lstDecodedPoints = FnDecodePolylinePoints(EncodedPoints);
								//new coolection
								var coordinatePoints = new ObservableCollection<Position>();

								foreach (Location loc in lstDecodedPoints)
								{

									coordinatePoints.Add(new Position(loc.lat, loc.lng));
									//LatLngPoints[index++] = new LatLng(loc.lat, loc.lng);
								}

								//adding the local collection(coordinatePoints) to ListCoordinate so the propertychanged get triggred
								ListCoordinate = coordinatePoints;
							}
						}
					}
					else
					{
						Debug.WriteLine("Invalid Request");
					}
				
				}
			}
		}

		//Decoding the overlaypolylines from the response
		ObservableCollection<Location> FnDecodePolylinePoints(string encodePoints)
		{
			if (string.IsNullOrEmpty(encodePoints))
				return null;
			var polylist = new ObservableCollection<Location>();
			char[] polyline = encodePoints.ToCharArray();
			int intex = 0, curntlat = 0, curntlong = 0;
			int nxt5bit, sum, shifter;
			try
			{
				while (intex < polyline.Length)
				{
					//Calculate latitude
					sum = 0; shifter = 0;
					do
					{
						nxt5bit = (int)polyline[intex++] - 63;
						sum |= (nxt5bit & 31) << shifter;
						shifter += 5;
					} while (nxt5bit >= 32 && intex < polyline.Length);
					if (intex >= polyline.Length)
						break;
					curntlat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

					//Calculate longitude
					sum = 0;
					shifter = 0;
					do
					{
						nxt5bit = (int)polyline[intex++] - 63;
						sum |= (nxt5bit & 31) << shifter;
						shifter += 5;
					} while (nxt5bit >= 32 && intex < polyline.Length);

					if (intex >= polyline.Length && nxt5bit >= 32)
						break;

					curntlong += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
					Location pos = new Location();
					pos.lat = Convert.ToDouble(curntlat) / 100000.0;
					pos.lng = Convert.ToDouble(curntlong) / 100000.0;
					polylist.Add(pos);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			return polylist;
		}
	}
}
