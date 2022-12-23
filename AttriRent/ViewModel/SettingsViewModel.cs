using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Models;
using AttriRent.Services;
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

        private string _passwordErrorMessage = string.Empty;
        private string _passwordSuccessMessage = string.Empty;
        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set
            {
                _passwordErrorMessage = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            }
        }
        public string PasswordSuccessMessage
        {
            get { return _passwordSuccessMessage; }
            set
            {
                _passwordSuccessMessage = value;
                OnPropertyChanged(nameof(PasswordSuccessMessage));
            }
        }

        private string _nameErrorMessage = string.Empty;
        private string _nameSuccessMessage = string.Empty;
        public string NameErrorMessage
        {
            get { return _nameErrorMessage; }
            set
            {
                _nameErrorMessage = value;
                OnPropertyChanged(nameof(NameErrorMessage));
            }
        }
        public string NameSuccessMessage
        {
            get { return _nameSuccessMessage; }
            set
            {
                _nameSuccessMessage = value;
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
            HashServise hs = new HashServise();

            PasswordErrorMessage = string.Empty;
            PasswordSuccessMessage = string.Empty;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (await db.users.Include(p => p.user_password).FirstOrDefaultAsync(u => u.id == ApplicationInfo.UserId) is User user)
                    if (hs.VerifyHashedPassword(user.user_password.password, CurrentPassword))
                    {
                        PasswordSuccessMessage = "Password was change";
                        user.user_password.password = NewPassword;
                        await db.SaveChangesAsync();
                    }
                    else
                        PasswordErrorMessage = "Incorrect password!";
            }
        }

        private async Task ChangeNameAsync()
        {
            HashServise hs = new HashServise();

            NameErrorMessage = string.Empty;
            NameSuccessMessage = string.Empty;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (await db.users.Include(p => p.user_password).FirstOrDefaultAsync(u => u.id == ApplicationInfo.UserId) is User user)
                    if (hs.VerifyHashedPassword(user.user_password.password, CurrentPassword))
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
