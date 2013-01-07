using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Web;

namespace FishingPoint.Messages
{
    public class LaunchEditPointMessage : MessageBase
    {
        public LaunchEditPointMessage()
            : base()
        { }

        public LaunchEditPointMessage(FishingPoint.Web.Point point)
        {
            this.Point = point;
        }

        public FishingPoint.Web.Point Point { get; set; }
    }

    public class UpdatedPointMessage : MessageBase
    {
        public UpdatedPointMessage()
            : base()
        { }

        public UpdatedPointMessage(FishingPoint.Web.Point point)
        {
            this.Point = point;
        }

        public FishingPoint.Web.Point Point { get; set; }
    }

    public class SavedPointDialogMessage : DialogMessage
    {

        public SavedPointDialogMessage(string content, string title)
            : base(content, null)
        {
            Button = MessageBoxButton.OK;
            Caption = title;
        }
    }

    public class LaunchNewPointMessage : MessageBase
    {
        public LaunchNewPointMessage()
            : base()
        { }

        public LaunchNewPointMessage(FishingPoint.Web.Point point)
        {
            this.Point = point;
        }

        public FishingPoint.Web.Point Point { get; set; }
    }

    public class AddNewPointMessage :MessageBase
    {
        public FishingPoint.Web.Point Point { get; set; }
    }
}
