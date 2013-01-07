using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;
using FishingPoint.Web;
using FishingPoint.Web.Services;

namespace FishingPoint.Services
{
    public class PointDataService : IPointDataService
    {
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;
        public FishingPointContext Context { get; set; }
        private Action<EntityList<Point>> _getPointsCallback;

        private LoadOperation<Point> _pointsLoadOperation;
        private int _pageIndex;

        /// <summary>
        /// Initialize the PointDataContext
        /// </summary>
        public PointDataService()
        {
            Context = new FishingPointContext();
            Context.PropertyChanged += ContextPropertyChanged;
        }

        /// <summary>
        /// Saves changes to the Context if available.
        /// </summary>
        /// <param name="submitCallback">CallBack to be called after load complition</param>
        /// <param name="state"></param>
        public void Save(Action<SubmitOperation> submitCallback, object state)
        {
            if (Context.HasChanges)
            {
                Context.SubmitChanges(submitCallback, state);
            }
        }

        /// <summary>
        /// Load PointList
        /// </summary>
        /// <param name="getPointsCallback">CallBack to be called after load complition</param>
        /// <param name="pageSize"></param>
        public void GetPointsList(Action<EntityList<Point>> getPointsCallback, int pageSize)
        {
            ClearPoints();
            var query = Context.GetCurrentUserPointsQuery().Take(pageSize);
            RunPointsQuery(query, getPointsCallback);
        }

        /// <summary>
        /// Notifies for property changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (NotifyHasChanges != null)
            {
                NotifyHasChanges(this, new HasChangesEventArgs() { HasChanges = Context.HasChanges });
            }
        }

        /// <summary>
        /// Clear Point List
        /// </summary>
        private void ClearPoints()
        {
            _pageIndex = 0;
            Context.Points.Clear();
        }

        /// <summary>
        /// Run Points Query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="getPointsCallback">CallBack</param>
        private void RunPointsQuery(EntityQuery<Point> query, Action<EntityList<Point>> getPointsCallback)
        {
            _getPointsCallback = getPointsCallback;
            _pointsLoadOperation = Context.Load<Point>(query);
            _pointsLoadOperation.Completed += OnLoadPointsCompleted;
        }

        /// <summary>
        /// Points loading Completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoadPointsCompleted(object sender, EventArgs e)
        {
            _pointsLoadOperation.Completed -= OnLoadPointsCompleted;
            var points = new EntityList<Point>(Context.Points, _pointsLoadOperation.Entities);
            _getPointsCallback(points);
        }

        /// <summary>
        /// Get Point by Id
        /// </summary>
        /// <param name="pointId"></param>
        /// <param name="getPointsCallback"></param>
        public void GetPointById(int pointId, Action<EntityList<Point>> getPointCallback)
        {
            ClearPoints();
            var query = Context.GetCurrentUserPointsQuery().Where(mm => mm.PointId == pointId);
            RunPointsQuery(query, getPointCallback);
        }

        public void Save(Point point, Action<SubmitOperation> submitCallback, object state)
        {
            Context.Points.Add(point);
            if (Context.HasChanges)
            {
                Context.SubmitChanges(submitCallback, state);
            }
        }

        #region IPointDataService Members


        public void GetPointById(int pointId, Action<ObservableCollection<Point>> getPointCallback)
        {
            throw new NotImplementedException();
        }

        public void GetPointsList(Action<ObservableCollection<Point>> getPointsCallback, int pageSize)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
