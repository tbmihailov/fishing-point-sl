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
using FishingPoint;

namespace FishingPoint.Messages
{
    public class ErrorMessage : MessageBase
    {
        public ErrorMessage(Error error, Exception exception)
        {
            Error = error;
            Exception = exception;
        }

        public Error Error { get; set; }
        public Exception Exception { get; set; }
    }
}
