/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:FishingPoint.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
namespace FishingPoint.ViewModels
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                _currentUserViewModel = new UserViewModel();
                //_fishingMapViewModel = new FishingMapViewModel();
                _mapViewModel = new MapViewModel();
            }
            else
            {
                //_fishingMapViewModel = new FishingMapViewModel();
                _mapViewModel = new MapViewModel();
                _currentUserViewModel = new UserViewModel();
            }
        }

        #region FishingMapViewModel
        private static FishingMapViewModel _fishingMapViewModel;

        /// <summary>
        /// Gets the FishingMapViewModel property.
        /// </summary>
        public static FishingMapViewModel FishingMapViewModelStatic
        {
            get
            {
                if (_fishingMapViewModel == null)
                {
                    CreateFishingMapViewModel();
                }

                return _fishingMapViewModel;
            }
        }

        /// <summary>
        /// Gets the FishingMapViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public FishingMapViewModel FishingMapViewModel
        {
            get
            {
                return FishingMapViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the FishingMapViewModel property.
        /// </summary>
        public static void ClearFishingMapViewModel()
        {
            _fishingMapViewModel.Cleanup();
            _fishingMapViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the FishingMapViewModel property.
        /// </summary>
        public static void CreateFishingMapViewModel()
        {
            if (_fishingMapViewModel == null)
            {
                _fishingMapViewModel = new FishingMapViewModel();
            }
        }


        #endregion

        #region CurrentUserViewModel
        private static UserViewModel _currentUserViewModel;

        /// <summary>
        /// Gets the CurrentUserViewModel property.
        /// </summary>
        public static UserViewModel CurrentUserViewModelStatic
        {
            get
            {
                if (_currentUserViewModel == null)
                {
                    CreateCurrentUserViewModel();
                }

                return _currentUserViewModel;
            }
        }

        /// <summary>
        /// Gets the CurrentUserViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public UserViewModel CurrentUserViewModel
        {
            get
            {
                return CurrentUserViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the CurrentUserViewModel property.
        /// </summary>
        public static void ClearCurrentUserViewModel()
        {
            _currentUserViewModel.Cleanup();
            _currentUserViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the CurrentUserViewModel property.
        /// </summary>
        public static void CreateCurrentUserViewModel()
        {
            if (_currentUserViewModel == null)
            {
                _currentUserViewModel = new UserViewModel();
            }
        }

        #endregion

        #region MapViewControl
        private static MapViewModel _mapViewModel;

        /// <summary>
        /// Gets the MapViewModel property.
        /// </summary>
        public static MapViewModel MapViewModelStatic
        {
            get
            {
                if (_mapViewModel == null)
                {
                    CreateMapViewModel();
                }

                return _mapViewModel;
            }
        }

        /// <summary>
        /// Gets the MapViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MapViewModel MapViewModel
        {
            get
            {
                return MapViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MapViewModel property.
        /// </summary>
        public static void ClearMapViewModel()
        {
            _mapViewModel.Cleanup();
            _mapViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the MapViewModel property.
        /// </summary>
        public static void CreateMapViewModel()
        {
            if (_mapViewModel == null)
            {
                _mapViewModel = new MapViewModel();
            }
        }

        #endregion

        public static void Cleanup()
        {
            ClearCurrentUserViewModel();
            ClearFishingMapViewModel();
            ClearMapViewModel();
        }
    }
}