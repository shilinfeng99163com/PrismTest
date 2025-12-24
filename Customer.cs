using Prism.Mvvm;

namespace PrismTest
{
    public class Customer : BindableBase
    {
        private int _id;
        private string _name;
        private string _email;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
    }
}