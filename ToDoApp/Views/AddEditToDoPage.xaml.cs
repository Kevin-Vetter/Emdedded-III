using ToDoApp.ViewModels;

namespace ToDoApp.Views
{
    public partial class AddEditToDoPage : ContentPage
    {
        private readonly AddEditToDoViewModel _vm;
        public AddEditToDoPage(AddEditToDoViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}