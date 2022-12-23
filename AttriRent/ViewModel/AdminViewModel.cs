using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.Models;
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
    internal class AdminViewModel : BaseViewModel
    {
        private string? _name;
        private string? _description;
        private string? _price;
        private string? _imagePath;
        public string? Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Price
        {
            get { return _price; }
            set
            {
                _price= value; 
                OnPropertyChanged(nameof(Price));
            }
        }
        public string? Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string? _nameOrId;
        public string? NameOrId
        {
            get { return _nameOrId; }
            set
            {
                _nameOrId = value;
                OnPropertyChanged(nameof(NameOrId));
            }
        }
        public string? ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private string _successMessage = string.Empty;
        private string _errorMessage = string.Empty;
        public string AddSuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                OnPropertyChanged(nameof(AddSuccessMessage));
            }
        }

        public string AddErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(AddErrorMessage));
            }
        }

        public string DeleteSuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                OnPropertyChanged(nameof(DeleteSuccessMessage));
            }
        }

        public string DeleteErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(DeleteErrorMessage));
            }
        }

        private Command _addCommand;
        public Command AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new Command(obj =>
                    {
                        AddSuccessMessage = string.Empty;

                        if (IsDataValid())
                        {
                            Attribute atr = new Attribute()
                            {
                                name = Name!,
                                description = Description,
                                price = int.Parse(Price!),
                                image_path = ImagePath
                            };

                            try
                            {
                                using (ApplicationDbContext db = new ApplicationDbContext())
                                {
                                    db.attributes.Add(atr);

                                    db.SaveChanges();
                                }

                                AddSuccessMessage = "New atrribute added successfully";
                            }
                            catch(Exception ex)
                            {
                                AddErrorMessage = ex.Message; 
                            }

                            Name = string.Empty;
                            Price = string.Empty;
                            Description = string.Empty;
                        }
                    }));
            }
        }

        private Command _deleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new Command(obj =>
                    {
                        DeleteSuccessMessage = string.Empty;
                        DeleteErrorMessage = string.Empty;

                        if (!string.IsNullOrWhiteSpace(NameOrId))
                        {
                            try
                            {
                                using (ApplicationDbContext db = new ApplicationDbContext())
                                {
                                    if (int.TryParse(NameOrId, out int id))
                                    {
                                        if (db.attributes.FirstOrDefault(a => a.id == id) is Attribute atr)
                                        {
                                            db.attributes.Remove(atr);
                                            db.SaveChanges();

                                            DeleteSuccessMessage = $"Attribute with ID {NameOrId} was deleted";
                                            NameOrId = string.Empty;
                                        }
                                        else
                                        {
                                            DeleteErrorMessage = "Attribute with this ID not found";
                                        }
                                    }
                                    else
                                    {
                                        if (db.attributes.FirstOrDefault(a => a.name == NameOrId) is Attribute atr)
                                        {
                                            db.attributes.Remove(atr);
                                            db.SaveChanges();

                                            DeleteSuccessMessage = $"Attribute with name {NameOrId} was deleted";
                                            NameOrId = string.Empty;
                                        }
                                        else
                                        {
                                            DeleteErrorMessage = "Attribute with this name not found";
                                        }
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                DeleteErrorMessage = ex.Message;
                            }
                        }
                        else
                        {
                            DeleteErrorMessage = "Field is empty";
                        }
                            
                    }));
            }
        }

        private bool IsDataValid()
        {
            AddErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Name))
            {
                AddErrorMessage = "Empty attribute name";
                return false;
            }

            if (double.TryParse(Name, out double _))
            {
                AddErrorMessage = "Name cannot be a number";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Price))
            {
                AddErrorMessage = "Empty attribute price";
                return false;
            }

            if (!int.TryParse(Price, out int _))
            {
                AddErrorMessage = "Price not a number";
                return false;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                Description = null;
            }

            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                ImagePath = null;
            }
            else if (!Uri.TryCreate(ImagePath, UriKind.RelativeOrAbsolute, out Uri _))
            {
                AddErrorMessage = "Incorrect URI";
                return false;
            }

            return true;
        }
    }
}
