using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Enums;
using AttriRent.Models;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using AttriRent.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ViewModels;
using System.Text.RegularExpressions;

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
            if (!IsDataValid(true)) return false;

            HashServise hs = new HashServise(); 

            if (_db.users.Include(r => r.user_role).Include(p => p.user_password).FirstOrDefault(u => (u.email == UserLogin || u.name == UserLogin)) is User user)
            {
                if (hs.VerifyHashedPassword(user.user_password.password, UserPassword))
                {
                    ApplicationInfo.UserId = user.id;
                    ApplicationInfo.ShowAdminPanelButton((Roles)user.user_role.role);
                    return true;
                }
                else
                {
                    ErrorMessage = "Incorrect password!";
                    return false;
                }
            }
            else
            {
                ErrorMessage = "Incorrect login!";
                return false;
            }

            return false;
        }

        private bool Register()
        {
            if(!IsDataValid()) return false;

            HashServise hs = new HashServise();

            if(_db.users.FirstOrDefault(u => u.email == UserEmail || u.name == UserLogin ) is null)
            {
                var user = 
                    new User()
                    {
                        email = UserEmail,
                        name = UserLogin,
                    };

                user.user_password = new UserPassword() { password = hs.HashPassword(UserPassword), user = user };
                user.user_role = new UserRole() { role = 1, user = user };
                _db.users.Add(user);
                _db.SaveChanges();
                return true;
            }
            else
            {
                ErrorMessage = "User exists";
                return false;
            }
        }

        private bool IsDataValid(bool isLogin = false)
        {
            ErrorMessage = string.Empty;

            if (!isLogin && string.IsNullOrWhiteSpace(UserEmail))
            {
                ErrorMessage = "Email field is empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(UserLogin))
            {
                ErrorMessage = "Login field is empty";
                return false;
            }

            if (string.IsNullOrWhiteSpace(UserPassword))
            {
                ErrorMessage = "Password field is empty";
                return false;
            }

            if (double.TryParse(UserLogin, out double _))
            {
                ErrorMessage = "Login cannot be a number";
                return false;
            }

            Regex reg = new Regex("^\\w+@{1}\\w+\\.{1}\\w+$");
            if(!isLogin && !reg.IsMatch(UserEmail))
            {
                ErrorMessage = "Incorrect email";
                return false;
            }

            return true;
        }
    }
}
