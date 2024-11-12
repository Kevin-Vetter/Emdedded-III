using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddEditToDoPage), typeof(AddEditToDoPage));
        }
    }
}
