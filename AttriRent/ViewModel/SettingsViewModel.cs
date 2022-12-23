using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Models;
using AttriRent.ViewModel.Navigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ViewModels;

namespace AttriRent.ViewModel
{
    internal class SettingsViewModel : BaseViewModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string NewName { get; set; } = string.Empty;

        public string PasswordErrorMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            }
        }
        public string PasswordSuccessMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(PasswordSuccessMessage));
            }
        }

        public string NameErrorMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(NameErrorMessage));
            }
        }
        public string NameSuccessMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(NameSuccessMessage));
            }
        }

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
                        Task.Run(() => ChangePasswordAsync());
                    }));
            }
        }

        private Command _changeNameCommand;
        public Command ChangeNameCommand
        {
            get
            {
                return _changeNameCommand ??
                    (_changeNameCommand = new Command(obj =>
                    {
                        Task.Run(() => ChangeNameAsync());
                    }));
            }
        }

        private async Task ChangePasswordAsync()
        {
            PasswordErrorMessage = string.Empty;
            PasswordSuccessMessage = string.Empty;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (await db.users.FirstOrDefaultAsync(u => u.id == ApplicationInfo.UserId) is User user)
                    if (user.password == CurrentPassword)
                    {
                        PasswordSuccessMessage = "Password was change";
                        user.password = NewPassword;
                        await db.SaveChangesAsync();
                    }
                    else
                        PasswordErrorMessage = "Incorrect password!";
            }
        }

        private async Task ChangeNameAsync()
        {
            NameErrorMessage = string.Empty;
            NameSuccessMessage = string.Empty;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (await db.users.FirstOrDefaultAsync(u => u.id == ApplicationInfo.UserId) is User user)
                    if (user.password == CurrentPassword)
                    {
                        NameSuccessMessage = "Name was change";
                        user.name = NewName;
                        await db.SaveChangesAsync();
                    }
                    else
                        NameErrorMessage = "Incorrect password!";
            }
        }
    }
}
