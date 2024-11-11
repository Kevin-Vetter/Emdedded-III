using ToDoApp.ViewModels;

namespace ToDoApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _vm;
        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            _vm.GetItemsCommand.Execute(null);

        }
    }

}