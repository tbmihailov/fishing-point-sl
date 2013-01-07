namespace FishingPoint.Services
{
    public class ServiceProvider : ServiceProviderBase
    {
        public ServiceProvider()
        {
            PageConductor = new PageConductor();
        }

        public override IFishermanUserDataService FishermanuserDataService
        {
            get
            {
                return new FishermanUserDataService();
            }
        }

        public override IPointDataService PointDataService
        {
            get
            { return new PointDataService(); }
        }

    }
}