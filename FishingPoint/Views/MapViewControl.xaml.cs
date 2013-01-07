using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Messages;

namespace FishingPoint.Views
{
    public partial class MapViewControl : UserControl
    {
        public MapViewControl()
        {
            InitializeComponent();
        }

        private void AgentsMap_MouseClick(object sender, Microsoft.Maps.MapControl.MapMouseEventArgs e)
        {
            Location location = AgentsMap.ViewportPointToLocation(e.ViewportPoint);
            double longitude = location.Longitude;
            double latitude = location.Latitude;

            var newPoint = new FishingPoint.Web.Point()
            {
                Longitude = longitude,
                Latitude = latitude,
            };

            var addNewPointMessage = new AddNewPointMessage()
            {
                Point = newPoint,
            };

            Messenger.Default.Send<AddNewPointMessage>(addNewPointMessage);
        }
    }
}
