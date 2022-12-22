using AttriRent.Commands.BaseCommand;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using ViewModels;
namespace AttriRent.ViewModel
{
    internal class NavbarViewModel : BaseViewModel
    {
        private Command _directToAccountCommand;
        public Command DirectToAccountCommand
        {
            get
            {
                return _directToAccountCommand ??
                    (_directToAccountCommand = new Command(obj =>
                    {
                        if(ApplicationInfo.CurrentPage != nameof(Account))
                            ApplicationInfo.SetNewPage(new Account(), nameof(Account));
                    }));
            }
        }

        private Command _directToAttributesCommand;
        public Command DirectToAttributesCommand
        {
            get
            {
                return _directToAttributesCommand ??
                    (_directToAttributesCommand = new Command(obj =>
                    {
                        if (ApplicationInfo.CurrentPage != nameof(Attributes))
                            ApplicationInfo.SetNewPage(new Attributes(), nameof(Attributes));
                    }));
            }
        }

        private Command _directToSettingsCommand;
        public Command DirectToSettingsCommand
        {
            get
            {
                return _directToSettingsCommand ??
                    (_directToSettingsCommand = new Command(obj =>
                    {
                        if (ApplicationInfo.CurrentPage != nameof(Settings))
                            ApplicationInfo.SetNewPage(new Settings(), nameof(Settings));
                    }));
            }
        }

        private Command _directToAdminPanelCommand;
        public Command DirectToAdminPanelCommand
        {
            get
            {
                return _directToAdminPanelCommand ??
                    (_directToAdminPanelCommand = new Command(obj =>
                    {
                        if (ApplicationInfo.CurrentPage != nameof(Admin))
                            ApplicationInfo.SetNewPage(new Admin(), nameof(Admin));
                    }));
            }
        }
    }
}
