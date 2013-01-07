using System.ComponentModel;

namespace FishingPoint.Services
{
    public abstract class ServiceProviderBase
    {
        public virtual IPageConductor PageConductor { get; protected set; }

        public virtual IFishermanUserDataService FishermanuserDataService { get; protected set; }
        public virtual IPointDataService PointDataService { get; protected set; }

        
        private static ServiceProviderBase _instance;
        public static ServiceProviderBase Instance
        {
            get 
            {
                if (_instance == null)
                {
                    CreateInstance(); 
                }
                return _instance;
            }
        }

        static ServiceProviderBase CreateInstance()
        {
            // TODO:  Uncomment
            if (DesignerProperties.IsInDesignTool)
            {
                _instance = new DesignServiceProvider();
            }
            else
            {
                _instance = new ServiceProvider();
            }
            return _instance;

        }
    }
}