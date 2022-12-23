using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Enums;
using AttriRent.Models;
using AttriRent.Models.DatagridModels;
using AttriRent.ViewModel.Navigation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewModels;

namespace AttriRent.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        private string _userName;
        private string _userEmail;
        private string _userRole;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }
        public string UserRole
        {
            get { return _userRole; }
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }

        private ObservableCollection<DatagridOrder> _orders = new ObservableCollection<DatagridOrder>();
        private ObservableCollection<DatagridOrder> _ordersHistory = new ObservableCollection<DatagridOrder>();
        public ObservableCollection<DatagridOrder> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        public ObservableCollection<DatagridOrder> OrdersHistory
        {
            get { return _ordersHistory; }
            set
            {
                _ordersHistory = value;
                OnPropertyChanged(nameof(OrdersHistory));
            }
        }

        public AccountViewModel()
        {
            SetInfoAboutUser();
        }

        public void SetInfoAboutUser()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.users.Include(r => r.user_role).FirstOrDefault(u => u.id == ApplicationInfo.UserId) is User user)
                {
                    UserName = user.name;
                    UserEmail = user.email;
                    UserRole = ((Roles)user.user_role.role).ToString();
                }

                if (db.orders.Include(o => o.Attribute).Where(o => o.User.id == ApplicationInfo.UserId).ToList() is List<Order> orders)
                {
                    foreach (Order order in orders)
                    {
                        if(order.activity_status)
                            Orders.Add(new DatagridOrder(order.User.name, order.Attribute.name, order.id, order.order_start_day, order.order_end_day));
                        else
                            OrdersHistory.Add(new DatagridOrder(order.User.name, order.Attribute.name, order.id, order.order_start_day, order.order_end_day));
                    }
                }
            }
        }

        private Command _endRentCommand;
        public Command EndRentCommand
        {
            get
            {
                return _endRentCommand ??
                    (_endRentCommand = new Command(obj =>
                    {
                        if (obj is DatagridOrder dg)
                        {
                            using (ApplicationDbContext db = new ApplicationDbContext())
                            {
                                if (db.orders.FirstOrDefault(o => o.id == dg.OrderId) is Order order)
                                {
                                    order.activity_status = false;
                                    order.order_end_day = DateTime.UtcNow;

                                    db.orders.Update(order);
                                }

                                db.SaveChanges();

                                dg.ChangeEndRentDate(DateTime.Now);
                                Orders.Remove(dg);
                                OrdersHistory.Add(dg);
                            }
                        }
                    }));
            }
        }
    }
}
