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
using Microsoft.Phone.Controls;
using System.Device;
using System.Device.Location;
using System.Diagnostics;


namespace BingMaps
{


    public partial class MainPage : PhoneApplicationPage
    {
        GeoCoordinateWatcher watcher;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            map1.Mode = new Microsoft.Phone.Controls.Maps.AerialMode();
            if (watcher == null)
            {
                //---get the highest accuracy---
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High)
                {
                    //---the minimum distance (in meters) to travel before the next 
                    // location update---
                    MovementThreshold = 10
                };

                //---event to fire when a new position is obtained---
                watcher.PositionChanged += new
                EventHandler<GeoPositionChangedEventArgs
                <GeoCoordinate>>(watcher_PositionChanged);

                //---event to fire when there is a status change in the location 
                // service API---
                watcher.StatusChanged += new
                EventHandler<GeoPositionStatusChangedEventArgs>
                (watcher_StatusChanged);
                watcher.Start();
            }
        }
        

void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
{
  switch (e.Status)
  {
    case GeoPositionStatus.Disabled:
    Debug.WriteLine("disabled");
    break;
    case GeoPositionStatus.Initializing:
    Debug.WriteLine("initializing");
    break;
    case GeoPositionStatus.NoData:
    Debug.WriteLine("nodata");
    break;
    case GeoPositionStatus.Ready:
    Debug.WriteLine("ready");
    break;
   }
}



void watcher_PositionChanged(object sender, 
GeoPositionChangedEventArgs<GeoCoordinate> e)
{
  Debug.WriteLine("({0},{1})", 
  e.Position.Location.Latitude, e.Position.Location.Longitude);
 
  map1.Center = new GeoCoordinate(
  e.Position.Location.Latitude, e.Position.Location.Longitude);
} 
    
    
    }
}