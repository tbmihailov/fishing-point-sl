using System;
using System.Collections.ObjectModel;
using FishingPoint.Web;
using System.Collections.Generic;

namespace FishingPoint.DesignModel
{
    public class DesignFishermanUsers : ObservableCollection<FishermanUser>
    {
        private const int entitiesCount = 10;
        public DesignFishermanUsers()
            : this(entitiesCount)
        {
        }

        public DesignFishermanUsers(int entitiesCount)
        {
            var fishermanUsersList = GenerateDesignFishermanUsersList(entitiesCount);
            foreach (var fishermanUser in fishermanUsersList)
            {
                this.Add(fishermanUser);
            }
        }

        public IList<FishermanUser> GenerateDesignFishermanUsersList(int entitiesCount)
        {
            IList<FishermanUser> generatedSource = new List<FishermanUser>();

            for (int i = 2; i < entitiesCount; i++)
            {
                var fishermanUser =
                    new FishermanUser()
                    {
                        FishermanUserId = i,
                        Name = "FishermanUser " + i,
                        Description = "FishermanUser " + i,
                        Registered = DateTime.Now,
                        Country = "Bulgaria",
                        City = "Sofia",
                        Username = "FIshermanUser" + i
                    };
                generatedSource.Add(fishermanUser);
            }

            return generatedSource;
        }

    }

}
