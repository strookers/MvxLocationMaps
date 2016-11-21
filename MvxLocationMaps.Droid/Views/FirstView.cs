using System;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Views;
using MvxLocationMaps.Core.ViewModels;

namespace MvxLocationMaps.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        private GoogleMap map;
        private double _lat;
        private double _lng;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);

            var viewModel = (FirstViewModel)ViewModel;

            var mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map);
            map = mapFragment.Map;

            //var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            //set.Bind()
            //    .For(m => m._lat)
            //    .To(vm => vm.Lat);
            //set.Bind()
            //    .For(m => m._lng)
            //    .To(vm => vm.Lng);
            //set.Apply();

            MarkOnMap("Mig", new LatLng(viewModel.Lat, viewModel.Lng));
            UpdateCameraPosition(new LatLng(viewModel.Lat, viewModel.Lng));
        }

        void MarkOnMap(string title, LatLng pos)
        {
            RunOnUiThread(() =>
            {
                try
                {
                    var marker = new MarkerOptions();
                    marker.SetTitle(title);
                    marker.SetPosition(pos); //Resource.Drawable.BlueDot
                    map.AddMarker(marker);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            });
        }

        void UpdateCameraPosition(LatLng pos)
        {
            try
            {
                CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
                builder.Target(pos);
                builder.Zoom(12);
                builder.Bearing(45);
                builder.Tilt(10);
                CameraPosition cameraPosition = builder.Build();
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
                map.AnimateCamera(cameraUpdate);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
        }
    }
}
