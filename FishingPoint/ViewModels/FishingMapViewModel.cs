using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using FishingPoint.Web;
using FishingPoint.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Messages;
using Microsoft.Windows.Data.DomainServices;

namespace FishingPoint.ViewModels
{
    public class FishingMapViewModel : ViewModelBase
    {
        public FishingMapViewModel()
        {
            if (IsInDesignMode)
            {
                //pointDataService = ServiceProvider.Instance.PointDataService;

            }
            else
            {
                pointDataService = ServiceProvider.Instance.PointDataService;
            }

            this.LoadPoints();
            this.RegisterMessages();
        }

        void RegisterMessages()
        {
            Messenger.Default.Register<AddNewPointMessage>(this, OnAddNewPoint);
        }

        public void OnAddNewPoint(AddNewPointMessage msg)
        {
            var  newPoint = msg.Point;
            this.Points.Add(newPoint);
        }

        #region Points
        /// <summary>
        /// The <see cref="Points" /> property's name.
        /// </summary>
        public const string PointsPropertyName = "Points";
        private EntityList<Point> _points;
        public EntityList<Point> Points
        {
            get
            {
                return _points;
            }

            set
            {
                if (_points == value)
                {
                    return;
                }

                var oldValue = _points;
                _points = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PointsPropertyName);
            }
        }

        IPointDataService pointDataService;

        /// <summary>
        /// Load Points 
        /// </summary>
        private void LoadPoints()
        {
            Points = null;
            pointDataService.GetPointsList(GetPointsCallback, int.MaxValue);
        }

        /// <summary>
        /// Fires when Points are  loaded
        /// </summary>
        /// <param name="mealMenu"></param>
        private void GetPointsCallback(EntityList<Point> points)
        {
            //if (points == null)
            //{
            //    return;
            //}

            //if (points.Count <= 0)
            //{
            //    return;
            //}

            this.Points = points;

        }
        #endregion

        #region IsPushingMode
        private bool _isPushingMode;
        public bool IsPushingMode
        {
            get
            {
                return _isPushingMode;
            }

            set
            {
                if (_isPushingMode == value)
                {
                    return;
                }
                _isPushingMode = value;
                RaisePropertyChanged("IsPushingMode");
            }
        }
        #endregion

        #region StartPushingCommand
        private RelayCommand _startPushingCommand;
        /// <summary>
        /// The <see cref="StartPushingCommand" />.
        /// </summary>

        public RelayCommand StartPushingCommand
        {
            get
            {
                if (_startPushingCommand == null)
                {
                    _startPushingCommand = new RelayCommand(
                            () =>
                            {
                                StartPushingExecute();
                            },
                            () => CanStartPushing
                        );
                }
                return _startPushingCommand;
            }
            set
            {
                _startPushingCommand = value;
            }
        }

        /// <summary>
        /// Executes when StartPushingCommand is called
        /// </summary>
        public void StartPushingExecute()
        {
            IsPushingMode = true;
        }

        /// <summary>
        /// The <see cref="CanStartPushing" /> property's name.
        /// </summary>
        public const string CanStartPushingPropertyName = "CanStartPushing";

        private bool _canStartPushing = false;

        /// <summary>
        /// Gets the CanStartPushing property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool CanStartPushing
        {
            get
            {
                return _canStartPushing;
            }

            set
            {
                if (_canStartPushing == value)
                {
                    return;
                }

                var oldValue = _canStartPushing;
                _canStartPushing = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanStartPushingPropertyName);

                //tells the controls that are binded to the Command if it can execute
                StartPushingCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region EndPushingCommand

        private RelayCommand _endPushingCommand;
        /// <summary>
        /// The <see cref="EndPushingCommand" />.
        /// </summary>

        public RelayCommand EndPushingCommand
        {
            get
            {
                if (_endPushingCommand == null)
                {
                    _endPushingCommand = new RelayCommand(
                            () =>
                            {
                                EndPushingExecute();
                            },
                            () => CanEndPushing
                        );
                }
                return _endPushingCommand;
            }
            set
            {
                _endPushingCommand = value;
            }
        }

        /// <summary>
        /// Executes when EndPushingCommand is called
        /// </summary>
        public void EndPushingExecute()
        {
            IsPushingMode = false;
        }


        /// <summary>
        /// The <see cref="CanEndPushing" /> property's name.
        /// </summary>
        public const string CanEndPushingPropertyName = "CanEndPushing";

        private bool _canEndPushing = false;

        /// <summary>
        /// Gets the CanEndPushing property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool CanEndPushing
        {
            get
            {
                return _canEndPushing;
            }

            set
            {
                if (_canEndPushing == value)
                {
                    return;
                }

                var oldValue = _canEndPushing;
                _canEndPushing = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanEndPushingPropertyName);

                //tells the controls that are binded to the Command if it can execute
                EndPushingCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        /// <summary>
        /// Cleans up the resources if needed
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}