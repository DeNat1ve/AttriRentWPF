using System.Windows;
using AttriRent.ViewModel;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using ViewModels;

namespace AttriRent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ApplicationInfo.SetUI(mainFrame, navbar);

            ApplicationInfo.SetNewPage(new Login(), nameof(Login));
        }
    }
}
