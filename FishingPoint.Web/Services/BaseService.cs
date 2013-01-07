
namespace FishingPoint.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using FishingPoint.Web;


    // Implements application logic using the KindergratenManagerDataEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class BaseService : LinqToEntitiesDomainService<FishingPointEntities>
    {
        private FishermanUser currentUser;
        /// <summary>
        /// Loggen user
        /// </summary>
        protected FishermanUser CurrentUser 
        {
            get
            {
                if (currentUser == null)
                {
                    var username = this.ServiceContext.User.Identity.Name;
                    this.CurrentUser = GetCurrentFishermanUser(username);
                }

                return currentUser;
            } 

            private set
            {
                currentUser = value;
            }
        }

        public override void Initialize(DomainServiceContext context)
        {
            base.Initialize(context);

            if (this.ServiceContext.User.Identity.IsAuthenticated)
            {
                var username = this.ServiceContext.User.Identity.Name;
                this.CurrentUser = GetCurrentFishermanUser(username);
            }
        }

        protected FishermanUser GetCurrentFishermanUser(string username)
        {
            var currentUser =
               this.ObjectContext
                   .FishermanUsers
                   .FirstOrDefault(su => su.Username == username);

            return currentUser;
        }

        protected int GetCurrentFishermanUserId(string username)
        {
            var currentUser =
               this.ObjectContext
                   .FishermanUsers
                   .FirstOrDefault(su => su.Username == username);

            if(currentUser == null)
            {
                return 0;
            }

            int currentUserId = currentUser.FishermanUserId;
            return currentUserId;
        }

        protected int GetCurrentFishermanUserId()
        {
            var currentUser = this.CurrentUser;
            if (currentUser == null)
            {
                return 0;
            }

            if (currentUser.FishermanUserId == null)
            {
                return 0;
            }

            int currentUserId = currentUser.FishermanUserId;
            return currentUserId;
        }
    }
}


