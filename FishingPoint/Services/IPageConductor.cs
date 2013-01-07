using System;

namespace FishingPoint.Services  
{
    public interface IPageConductor
    {
        void DisplayError(string origin, Exception e, string details);
        void DisplayError(string origin, Exception e);
        void GoToView(string viewToken);
        void GoBack();

    }
}