using System;
using System.Collections.ObjectModel;
using FishingPoint.Web;
using System.Collections.Generic;

namespace FishingPoint.DesignModel
{
    public class DesignPoints : ObservableCollection<Point>
    {
        private const int entitiesCount = 10;
        public DesignPoints()
            : this(entitiesCount)
        {
        }

        public DesignPoints(int entitiesCount)
        {
            var pointsList = GenerateDesignPointsList(entitiesCount);
            foreach (var point in pointsList)
            {
                this.Add(point);
            }
        }

        public IList<Point> GenerateDesignPointsList(int entitiesCount)
        {
            IList<Point> generatedSource = new List<Point>();

            for (int i = 2; i < entitiesCount; i++)
            {
                var point =
                    new Point()
                    {
                        PointId = i,
                        Latitude = 44.12222,
                        Longitude = 23.232332,
                        Description = "Fishing point "+1,
                        IsPublic = (i%2)==0?false:true,
                        UserId = 1,
                    };
                generatedSource.Add(point);
            }

            return generatedSource;
        }

    }

}
