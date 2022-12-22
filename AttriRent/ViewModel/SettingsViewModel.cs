using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Models;
using AttriRent.ViewModel.Navigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;

namespace AttriRent.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        public SettingsViewModel()
        {
        }

        private Command _changePasswordCommand;
        public Command ChangePasswordCommand
        {
            get
            {
                return _changePasswordCommand ??
                    (_changePasswordCommand = new Command(obj =>
                    {
                        Task.Run(() => ChangePassword());
                    }));
            }
        }

        private async Task ChangePassword()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (await db.users.FirstOrDefaultAsync(u => u.id == ApplicationInfo.UserId) is User user)
                    if (user.password == CurrentPassword)
                        user.password = NewPassword;
                    else
                        ErrorMessage = "Incorrect password!";
            }
        }
    }
}
