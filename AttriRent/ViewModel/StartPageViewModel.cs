using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Enums;
using AttriRent.Models;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModels;

namespace AttriRent.ViewModel
{
    internal class StartPageViewModel : BaseViewModel
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        public string UserLogin { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;

        public StartPageViewModel()
        {
        }

        private Command _tryLogin;
        public Command TryLogin
        {
            get
            {
                return _tryLogin ??
                    (_tryLogin = new Command(obj =>
                    {
                        if (Login())
                        {
                            _db.Dispose();
                            ApplicationInfo.ShowNavbar();
                            ApplicationInfo.SetNewPage(new Account(), nameof(Account));
                        }
                        else
                            ErrorMessage = "Incorrect login or password!";
                    }));
            }
        }
        private Command _tryRegister;
        public Command TryRegister
        {
            get
            {
                return _tryRegister ??
                    (_tryRegister = new Command(obj =>
                    {
                        if (Register())
                        {
                            _db.Dispose();
                            ApplicationInfo.SetNewPage(new Login(), nameof(Views.Frames.Login));
                        }
                        else
                            ErrorMessage = "User exists";
                    }));
            }
        }
        private Command _changeStartPage;
        public Command ChangeStartPage
        {
            get
            {
                return _changeStartPage ??
                    (_changeStartPage = new Command(obj =>
                    {
                        if (ApplicationInfo.CurrentPage.Equals(nameof(Views.Frames.Login)))
                        {
                            _db.Dispose();
                            ApplicationInfo.SetNewPage(new Register(), nameof(Views.Frames.Register));
                        }
                        else
                        {
                            _db.Dispose();
                            ApplicationInfo.SetNewPage(new Login(), nameof(Views.Frames.Login));
                        }
                    }));
            }
        }
        private bool Login()
        {
            if (_db.users.FirstOrDefault(u => (u.email == UserLogin || u.name == UserLogin) && u.password == UserPassword) is User user)
            {
                ApplicationInfo.UserId = user.id;
                ApplicationInfo.ShowAdminPanelButton((Roles)user.user_role);
                return true;
            }

            return false;
        }

        private bool Register()
        {
            if(_db.users.FirstOrDefault(u => u.email == UserEmail || u.name == UserLogin ) is null)
            {
                var user = 
                    new User()
                    {
                        email = UserEmail,
                        name = UserLogin,
                        password = UserPassword,
                        user_role = 1
                    };
                _db.users.Add(user);
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
