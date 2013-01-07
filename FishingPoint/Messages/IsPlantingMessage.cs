using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace FishingPoint.Messages
{
    /// <summary>
    /// Starts planting mode
    /// </summary>
    public class StartPushingModeMessage : MessageBase
    {

    }

    public class EndPushingModeMessage : MessageBase
    {

    }

    public class IsPushingModePropertyChangedMessage : PropertyChangedMessageBase
    {
        public bool OldValue { get; set; }
        public bool NewValue { get; set; }

        public IsPushingModePropertyChangedMessage() : base("IsPlantingMode")
        {
        }
    }


}
