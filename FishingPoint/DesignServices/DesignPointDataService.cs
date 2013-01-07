using System;
using FishingPoint.Web;
using FishingPoint.DesignModel;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;

namespace FishingPoint.Services
{
    public class DesignPointDataService : IPointDataService
    {
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        public void Save(Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        public void GetPointsList(Action<System.Collections.ObjectModel.ObservableCollection<Point>> getPointsCallback, int pageSize)
        {
            getPointsCallback(new DesignPoints());
        }

        public void GetPointById(int pointId, Action<System.Collections.ObjectModel.ObservableCollection<Point>> getPointCallback)
        {
            getPointCallback(new DesignPoints());
        }


        public void Save(Point point, Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        #region IPointDataService Members


        public void GetPointsList(Action<Microsoft.Windows.Data.DomainServices.EntityList<Point>> getPointsCallback, int pageSize)
        {
            EntitySet<Point> es = new EntitySet<Point>();
            DesignPoints designPoints = new DesignPoints();
            foreach (var dp in designPoints)
            {
                es.Add(dp);
            }

            var pointsEntityList = new EntityList<Point>(es);
            getPointsCallback(pointsEntityList);
        }

        #endregion
    }
}


