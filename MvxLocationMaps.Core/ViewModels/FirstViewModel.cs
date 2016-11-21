using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.Location;

namespace MvxLocationMaps.Core.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        private readonly IMvxLocationWatcher _locationWatcher;

        public FirstViewModel(IMvxLocationWatcher locationWatcher)
        {
            _locationWatcher = locationWatcher;

            _locationWatcher.Start(new MvxLocationOptions { Accuracy = MvxLocationAccuracy.Fine }, OnLocation, OnError);
            _lat = _locationWatcher.CurrentLocation.Coordinates.Latitude;
            _lng = _locationWatcher.CurrentLocation.Coordinates.Longitude;
            _locationWatcher.Stop();
            System.Diagnostics.Debug.WriteLine($"Lat: {Lat} Lng: {Lng}");
        }

        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { if (_lat == value) return; _lat = value; RaisePropertyChanged(() => Lat); }
        }
        private double _lng;
        public double Lng
        {
            get { return _lng; }
            set { if (_lng == value) return; _lng = value; RaisePropertyChanged(() => Lng); }
        }


        public void OnError(MvxLocationError obj)
        {
            Mvx.Trace(MvxTraceLevel.Error, "Failed to get location: {0}", obj.Code);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            Lat = location.Coordinates.Latitude;
            Lng = location.Coordinates.Longitude;
        }

        private MvxCommand _nearMeCommand;

        public MvxCommand NearMeCommand
        {
            get
            {
                _nearMeCommand = _nearMeCommand ?? new MvxCommand(DoNearMeCommandCommand);
                return _nearMeCommand;
            }
        }

        private void DoNearMeCommandCommand()
        {
            System.Diagnostics.Debug.WriteLine($"Lat: {Lat} Lng: {Lng}");
        }


    }
}
