using System;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using FishingPoint.Messages;
using FishingPoint.Views;

namespace FishingPoint.Services
{
    public class PageConductor : IPageConductor
    {
        protected Frame RootFrame { get; set; }

        public PageConductor()
        {

        }

        #region Message recieves
        private void RegisterMessages()
        {
            Messenger.Default.Register<ErrorMessage>(this, OnErrorMessageRecieved);
        }

        public void OnErrorMessageRecieved(ErrorMessage e)
        {
            this.DisplayError("",e.Exception);
        }
        #endregion


        public void DisplayError(string origin, Exception e, string details)
        {
            string description = string.Format("Error occured in {0}. {1} {2}", origin, details, e.Message);
            var error = new Error()
            {
                Description = description,
                Title = "Error Occurred"
            };

            ErrorMessageWindow errorWindow = new ErrorMessageWindow(error);
            errorWindow.Show();
            error = null;
        }

        public void DisplayError(string origin, Exception e)
        {
            DisplayError(origin, e, string.Empty);
        }

        public void GoToView(string viewToken)
        {
            Go(FormatViewPath(viewToken));
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }

        private void Go(string path)
        {
            RootFrame.Navigate(new Uri(path, UriKind.Relative));
        }

        private string FormatViewPath(string viewToken)
        {
            return string.Format("/{0}/{1}.xaml", ViewTokens.Root, viewToken);
        }


    }
}