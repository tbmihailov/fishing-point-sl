using System;
using FishingPoint.Web;
using FishingPoint.DesignModel;

namespace FishingPoint.Services
{
    public class DesignFishermanUserDataService : IFishermanUserDataService
    {
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        public void Save(Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }

        public void GetFishermanUsersList(Action<System.Collections.ObjectModel.ObservableCollection<FishermanUser>> getFishermanUsersCallback, int pageSize)
        {
            getFishermanUsersCallback(new DesignFishermanUsers());
        }

        public void GetFishermanUserById(int fishermanUserId, Action<System.Collections.ObjectModel.ObservableCollection<FishermanUser>> getFishermanUserCallback)
        {
            getFishermanUserCallback(new DesignFishermanUsers());
        }


        public void Save(FishermanUser fishermanUser, Action<System.ServiceModel.DomainServices.Client.SubmitOperation> submitCallback, object state)
        {
            submitCallback(null);
        }
    }
}
