using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using FishingPoint.Web;
using Microsoft.Windows.Data.DomainServices;

namespace FishingPoint.Services
{
    public interface IPointDataService
    {
        event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        void Save(Action<SubmitOperation> submitCallback, object state);

        void GetPointById(int pointId, Action<ObservableCollection<Point>> getPointCallback);

        void GetPointsList(
            Action<ObservableCollection<Point>> getPointsCallback,
            int pageSize);

        void GetPointsList(
            Action<EntityList<Point>> getPointsCallback,
            int pageSize);

        void Save(Point point, Action<SubmitOperation> submitCallback, object state);
    }
}
