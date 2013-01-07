//IFishermanUserDataService.cs
using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using FishingPoint.Web;

namespace FishingPoint.Services
{
    public interface IFishermanUserDataService
    {
        event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        void Save(Action<SubmitOperation> submitCallback, object state);

        void GetFishermanUserById(int fishermanUserId, Action<ObservableCollection<FishermanUser>> getFishermanUserCallback);

        void GetFishermanUsersList(
            Action<ObservableCollection<FishermanUser>> getFishermanUsersCallback,
            int pageSize);

        void Save(FishermanUser fishermanUser, Action<SubmitOperation> submitCallback, object state);
    }
}
