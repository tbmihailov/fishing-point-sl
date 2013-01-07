#region  FishermanUserDataService.cs
using System;
using System.Collections.ObjectModel;
using System.ServiceModel.DomainServices.Client;
using Microsoft.Windows.Data.DomainServices;
using FishingPoint.Web;
using FishingPoint.Web.Services;

namespace FishingPoint.Services
{
    //ServiceProviderBase
    //public virtual IFishermanUserDataService FishermanUserDataService { get; protected set; }
    //
    //ServiceProvider
    //    public override IFishermanUserDataService FishermanUserDataService
    //    {
    //        get { return new FishermanUserDataService(); }
    //    }

    public class FishermanUserDataService : IFishermanUserDataService
    {
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;
        public FishingPointContext Context { get; set; }
        private Action<ObservableCollection<FishermanUser>> _getFishermanUsersCallback;

        private LoadOperation<FishermanUser> _fishermanUsersLoadOperation;
        private int _pageIndex;

        /// <summary>
        /// Initialize the FishermanUserDataContext
        /// </summary>
        public FishermanUserDataService()
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
        /// Load FishermanUserList
        /// </summary>
        /// <param name="getFishermanUsersCallback">CallBack to be called after load complition</param>
        /// <param name="pageSize"></param>
        public void GetFishermanUsersList(Action<ObservableCollection<FishermanUser>> getFishermanUsersCallback, int pageSize)
        {
            ClearFishermanUsers();
            var query = Context.GetFishermanUsersQuery().Take(pageSize);
            RunFishermanUsersQuery(query, getFishermanUsersCallback);
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
        /// Clear FishermanUser List
        /// </summary>
        private void ClearFishermanUsers()
        {
            _pageIndex = 0;
            Context.FishermanUsers.Clear();
        }

        /// <summary>
        /// Run FishermanUsers Query
        /// </summary>
        /// <param name="query">Query</param>
        /// <param name="getFishermanUsersCallback">CallBack</param>
        private void RunFishermanUsersQuery(EntityQuery<FishermanUser> query, Action<ObservableCollection<FishermanUser>> getFishermanUsersCallback)
        {
            _getFishermanUsersCallback = getFishermanUsersCallback;
            _fishermanUsersLoadOperation = Context.Load<FishermanUser>(query);
            _fishermanUsersLoadOperation.Completed += OnLoadFishermanUsersCompleted;
        }

        /// <summary>
        /// FishermanUsers loading Completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoadFishermanUsersCompleted(object sender, EventArgs e)
        {
            _fishermanUsersLoadOperation.Completed -= OnLoadFishermanUsersCompleted;
            var fishermanUsers = new EntityList<FishermanUser>(Context.FishermanUsers, _fishermanUsersLoadOperation.Entities);
            _getFishermanUsersCallback(fishermanUsers);
        }

        /// <summary>
        /// Get FishermanUser by Id
        /// </summary>
        /// <param name="fishermanUserId"></param>
        /// <param name="getFishermanUsersCallback"></param>
        public void GetFishermanUserById(int fishermanUserId, Action<ObservableCollection<FishermanUser>> getFishermanUserCallback)
        {
            ClearFishermanUsers();
            var query = Context.GetFishermanUsersQuery().Where(mm => mm.FishermanUserId == fishermanUserId);
            RunFishermanUsersQuery(query, getFishermanUserCallback);
        }


        public void Save(FishermanUser fishermanUser, Action<SubmitOperation> submitCallback, object state)
        {
            Context.FishermanUsers.Add(fishermanUser);
            if (Context.HasChanges)
            {
                Context.SubmitChanges(submitCallback, state);
            }
        }
    }
}
#endregion 