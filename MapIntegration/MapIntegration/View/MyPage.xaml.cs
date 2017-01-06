using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MapIntegration
{
	public partial class MyPage : ContentPage
	{
		
		public MyPage()
		{
			InitializeComponent();
			BindingContext = new MapPageViewModal();
		}
	}
}
