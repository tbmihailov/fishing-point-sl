using GalaSoft.MvvmLight;

namespace FishingPoint
{
    public class Error : ViewModelBase
    {
        public Error()
        {
        }

        public Error(string title, string description)
            : this()
        {
            Title = title;
            Description = description;
        }

        public const string TitlePropertyName = "Title";
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                {
                    return;
                }

                var oldValue = _title;
                _title = value;

                RaisePropertyChanged(TitlePropertyName);
            }
        }

        public const string DescriptionPropertyName = "Description";
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                var oldValue = _description;
                _description = value;

                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
    }
}