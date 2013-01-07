
//DesignPointsLoader.cs
using Microsoft.Windows.Data.DomainServices;
using System.Collections.Generic;
using FishingPoint.Web;
using System.ComponentModel;
using FishingPoint.DesignModel;

namespace FishingPoint.DesignServices
{
    public class DesignPointsLoader : CollectionViewLoader
    {
        public static void LoadSource()
        {
            var generatedSource = new DesignPoints();
            source = generatedSource;
        }

        private static IEnumerable<Point> source;

        public IEnumerable<Point> Source
        {
            get
            {
                if (DesignPointsLoader.source == null)
                {
                    LoadSource();
                }
                return DesignPointsLoader.source;
            }
        }

        public override bool CanLoad { get { return true; } }
        public override void Load(object userState)
        {
            this.OnLoadCompleted(new AsyncCompletedEventArgs(null, false, userState));
        }
    }
}