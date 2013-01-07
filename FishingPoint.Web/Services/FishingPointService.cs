
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


    // Implements application logic using the FishingPointEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class FishingPointService : BaseService
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'FishermanUsers' query.
        [Query(IsDefault = true)]
        public IQueryable<FishermanUser> GetFishermanUsers()
        {
            return this.ObjectContext.FishermanUsers;
        }

        public void InsertFishermanUser(FishermanUser fishermanUser)
        {
            if ((fishermanUser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fishermanUser, EntityState.Added);
            }
            else
            {
                this.ObjectContext.FishermanUsers.AddObject(fishermanUser);
            }
        }

        public void UpdateFishermanUser(FishermanUser currentFishermanUser)
        {
            this.ObjectContext.FishermanUsers.AttachAsModified(currentFishermanUser, this.ChangeSet.GetOriginal(currentFishermanUser));
        }

        public void DeleteFishermanUser(FishermanUser fishermanUser)
        {
            if ((fishermanUser.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fishermanUser, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.FishermanUsers.Attach(fishermanUser);
                this.ObjectContext.FishermanUsers.DeleteObject(fishermanUser);
            }
        }

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'Points' query.
        [Query(IsDefault = true)]
        public IQueryable<Point> GetAllPoints()
        {
            return this.ObjectContext.Points;
        }


        /// <summary>
        /// Gets only currently logged user points
        /// </summary>
        /// <returns></returns>
        public IQueryable<Point> GetCurrentUserPoints()
        {
            int currentUserId = this.GetCurrentFishermanUserId();
            var points = this.ObjectContext
                             .Points
                             .Where(p => p.UserId == currentUserId);
            return points;
        }

        [RequiresAuthentication(ErrorMessage = "Please, log in first!")]
        public void InsertPoint(Point point)
        {
            if ((point.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(point, EntityState.Added);
            }
            else
            {
                this.ObjectContext.Points.AddObject(point);
            }
        }

        [RequiresAuthentication(ErrorMessage = "Please, log in first!")]
        public void UpdatePoint(Point currentPoint)
        {
            this.ObjectContext.Points.AttachAsModified(currentPoint, this.ChangeSet.GetOriginal(currentPoint));
        }

        [RequiresAuthentication(ErrorMessage = "Please, log in first!")]
        public void DeletePoint(Point point)
        {
            if ((point.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(point, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.Points.Attach(point);
                this.ObjectContext.Points.DeleteObject(point);
            }
        }

        
    }
}


