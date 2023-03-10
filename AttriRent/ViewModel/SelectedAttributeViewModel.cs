using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Enums;
using AttriRent.Models;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Attribute = AttriRent.Models.Attribute;

namespace AttriRent.ViewModel
{
    class SelectedAttributeViewModel : BaseViewModel
    {
        private int _id;
        private bool _isRentEnable = true;
        private int _index = 0;
        private string? _imagePath { get; set; }
        public int Id { get { return _id; } }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Price { get; set; }
        public string ImagePath
        {
            get
            {
                if (_imagePath != null)
                    return _imagePath;
                else
                    return "/LocalImages/DefaultImage.jpg";
            }
        }
        public bool IsRentEnable 
        {
            get { return _isRentEnable; }
            set
            {
                _isRentEnable = value;
                OnPropertyChanged(nameof(IsRentEnable));
            }
        }
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }


        public SelectedAttributeViewModel(int id)
        {
            _id = id;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {   
                if (db.attributes.FirstOrDefault(a => a.id == _id) is Attribute atr)
                {
                    if (db.orders.Include(o => o.Attribute).OrderByDescending(o => o.order_end_day).FirstOrDefault(a => a.Attribute.id == atr.id) is Order order)
                        if(order.activity_status)
                            IsRentEnable = false;

                    Name = atr.name;
                    Description = atr.description;
                    Price = atr.price.ToString();
                    _imagePath = atr.image_path;
                }
            }
        }

        private Command _getRentCommand;
        public Command GetRentCommand
        {
            get
            {
                return _getRentCommand ??
                    (_getRentCommand = new Command(obj =>
                    {
                        int duration = 0;

                        switch ((CountOfDays)_index)
                        {
                            case CountOfDays.OneDay:
                                duration = 1;
                                break;
                            case CountOfDays.ThreeDays:
                                duration = 3;
                                break;
                            case CountOfDays.FiveDays:
                                duration = 5;
                                break;
                            case CountOfDays.SevenDays:
                                duration = 7;
                                break;
                            case CountOfDays.FourteenDays:
                                duration = 14;
                                break;
                            default:
                                duration = 1;
                                break;
                        }

                        using (ApplicationDbContext db = new ApplicationDbContext())
                        {
                            db.orders.Add(new Order()
                            {
                                activity_status = true,
                                order_start_day = DateTime.UtcNow,
                                order_end_day = DateTime.UtcNow.AddDays(duration),
                                Attribute = db.attributes.FirstOrDefault(a => a.id == _id),
                                User = db.users.FirstOrDefault(u => u.id == ApplicationInfo.UserId)
                            });

                            db.SaveChanges();

                            IsRentEnable = false;
                        }
                    }));
            }
        }
    }
}
