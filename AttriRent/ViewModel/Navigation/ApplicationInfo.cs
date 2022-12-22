using AttriRent.Views.Frames;
using System.Windows.Controls;
using System.Windows;
using AttriRent.Enums;
using System.Windows.Input;
using System.Windows.Data;

namespace AttriRent.ViewModel.Navigation
{
    public static class ApplicationInfo
    {
        public static int UserId;
        public static string CurrentPage { get { return _currentPage; } }

        private static Frame frame;
        private static StackPanel stackPanel; 
        private static string _currentPage = nameof(Login);

        public static void SetUI(Frame newFrame, StackPanel newStackPanel)
        {
            frame = newFrame;
            stackPanel = newStackPanel;
            stackPanel.DataContext = new NavbarViewModel();
        }

        public static void SetNewPage(Page newPage, string pageName)
        {
            frame.Navigate(newPage);
            _currentPage = pageName;
        }

        public static void ShowNavbar()
        {
            stackPanel.Visibility = Visibility.Visible;
        }

        public static void ShowAdminPanelButton(Roles role)
        {
            if(role == Roles.Administrator)
            {
                stackPanel.Children.Add(new Button()
                {
                    Content = "Admin Panel",
                    Height = 30,
                    Command = new NavbarViewModel().DirectToAdminPanelCommand
                });
            }
        }
    }
}
