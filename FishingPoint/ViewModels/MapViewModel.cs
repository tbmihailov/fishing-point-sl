using GalaSoft.MvvmLight;
using FishingPoint.Web.Services;
using Microsoft.Windows.Data.DomainServices;
using FishingPoint.Web;
using FishingPoint.DesignServices;
using System.Collections.Specialized;
using System.ServiceModel.DomainServices.Client;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Messages;

namespace FishingPoint.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MapViewModel : ViewModelBase
    {
        private readonly FishingPointContext context = new FishingPointContext();
        private readonly DomainCollectionView<Point> view;
        public DomainCollectionView<Point> View
        {
            get { return view; }
        } 

        private readonly DomainCollectionViewLoader<Point> loader;
        private readonly EntityList<Point> source;
        
        /// <summary>
        /// Initializes a new instance of the PointsViewModelNew class.
        /// </summary>
        public MapViewModel()
        {
            source = new EntityList<Point>(context.Points);
            loader = new DomainCollectionViewLoader<Point>(LoadPoints, OnLoadPointsCompleted);
            view = new DomainCollectionView<Point>(loader, source);

            #region DesignerProperties.IsInDesignTool
            // Swap out the loader for design-time scenarios
            if (IsInDesignMode)
            {
                DesignPointsLoader designLoader = new DesignPointsLoader();
                this.view = new DomainCollectionView<Point>(designLoader, designLoader.Source);
            }
            #endregion

            //Move to first page when SortDescriptions is changed
            INotifyCollectionChanged notifySortDescriptions =
                (INotifyCollectionChanged)this.view.SortDescriptions;
            notifySortDescriptions.CollectionChanged +=
                (s, e) => view.MoveToFirstPage();

            using (this.View.DeferRefresh())
            {
                this.View.PageSize = 10;
                this.View.MoveToFirstPage();
            }

            this.RegisterMessages();
        }

        void RegisterMessages()
        {
            Messenger.Default.Register<AddNewPointMessage>(this, OnAddNewPoint);
        }

        public void OnAddNewPoint(AddNewPointMessage msg)
        {
            var newPoint = msg.Point;
            this.source.Add(newPoint);
        }

        public LoadOperation<Point> LoadPoints()
        {
            this.IsGridEnabled = false;
            var pointsQuery = context.GetCurrentUserPointsQuery().SortAndPageBy(this.view);
            var pointsLoadOperation = this.context.Load(pointsQuery);
            return pointsLoadOperation;
        }

        public void OnLoadPointsCompleted(LoadOperation<Point> op)
        {
            this.IsGridEnabled = true;
            if (op.HasError)
            {
                // TODO: handle errors
                op.MarkErrorAsHandled();
            }
            else if (!op.IsCanceled)
            {
                this.source.Source = op.Entities;

                if (op.TotalEntityCount != -1)
                {
                    this.view.SetTotalItemCount(op.TotalEntityCount);
                }
            }
            if (source.Count >= 0)
            {
                View.MoveCurrentToFirst();
            }
        }


        #region IsGridEnabled Property
        /// <summary>
        /// The <see cref="IsGridEnabled" /> property's name.
        /// </summary>
        public const string IsGridEnabledPropertyName = "IsGridEnabled";

        private bool _isGridEnabled = false;
        public bool IsGridEnabled
        {
            get
            {
                return _isGridEnabled;
            }

            set
            {
                if (_isGridEnabled == value)
                {
                    return;
                }

                var oldValue = _isGridEnabled;
                _isGridEnabled = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsGridEnabledPropertyName);

                //// Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                //RaisePropertyChanged(IsGridEnabledPropertyName, oldValue, value, true);
            }
        }
        #endregion

        #region SelectedPoint
        /// <summary>
        /// The <see cref="SelectedPoint" /> property's name.
        /// </summary>
        public const string SelectedPointPropertyName = "SelectedPoint";

        private Point _selectedPoint;
        public Point SelectedPoint
        {
            get
            {
                return _selectedPoint;
            }

            set
            {
                if (_selectedPoint == value)
                {
                    return;
                }

                var oldValue = _selectedPoint;
                _selectedPoint = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedPointPropertyName);
            }
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

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}