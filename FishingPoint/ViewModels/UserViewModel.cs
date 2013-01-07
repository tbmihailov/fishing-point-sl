using GalaSoft.MvvmLight;
using System;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using FishingPoint.Web.Services;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Web;

namespace FishingPoint.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        FishingPointContext context;

        /// <summary>
        /// Initializes a new instance of the UserViewModel class.
        /// </summary>
        public UserViewModel()
        {
            if (IsInDesignMode)
            {
                this.User =
                    new Web.FishermanUser()
                    {
                        Username = "Todor",
                        Name = "Тодор Михайлов",
                    };
            }
            else
            {
                context = new FishingPointContext();
                // Code runs "for real": Connect to service, etc...
                this.User = new FishermanUser();
                LoadCurrentUser();
            }
        }

        /// <summary>
        /// The <see cref="User" /> property's name.
        /// </summary>
        public const string UserPropertyName = "User";

        private FishermanUser user = new FishermanUser();
        public FishermanUser User
        {
            get
            {
                return user;
            }
            set
            {
                if (user == value)
                {
                    return;
                }

                var oldValue = user;
                user = value;

                RaisePropertyChanged(UserPropertyName, oldValue, value, true);
            }
        }

        public void LoadCurrentUser()
        {
            var load = context.Load<FishermanUser>(context.GetFishermanUsersQuery(),false);
            load.Completed += new EventHandler(OnUserLoaded);
        }

        public void OnUserLoaded(object sender, EventArgs e)
        {
            LoadOperation lop = sender as LoadOperation;
            if (lop.Error != null)
            {
                throw lop.Error;
            }

            FishermanUser currentUser = lop.Entities.FirstOrDefault() as FishermanUser;
            this.User = currentUser;

        }

    }
}