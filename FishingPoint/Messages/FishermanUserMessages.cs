using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Web;

namespace FishingPoint.Messages
{
    public class LaunchEditFishermanUserMessage : MessageBase
    {
        public LaunchEditFishermanUserMessage()
            : base()
        { }

        public LaunchEditFishermanUserMessage(FishermanUser fishermanUser)
        {
            this.FishermanUser = fishermanUser;
        }

        public FishermanUser FishermanUser { get; set; }
    }

    public class UpdatedFishermanUserMessage : MessageBase
    {
        public UpdatedFishermanUserMessage()
            : base()
        { }

        public UpdatedFishermanUserMessage(FishermanUser fishermanUser)
        {
            this.FishermanUser = fishermanUser;
        }

        public FishermanUser FishermanUser { get; set; }
    }

    public class SavedFishermanUserDialogMessage : DialogMessage
    {

        public SavedFishermanUserDialogMessage(string content, string title)
            : base(content, null)
        {
            Button = MessageBoxButton.OK;
            Caption = title;
        }
    }

    public class LaunchNewFishermanUserMessage : MessageBase
    {
        public LaunchNewFishermanUserMessage()
            : base()
        { }

        public LaunchNewFishermanUserMessage(FishermanUser fishermanUser)
        {
            this.FishermanUser = fishermanUser;
        }

        public FishermanUser FishermanUser { get; set; }
    }
}
