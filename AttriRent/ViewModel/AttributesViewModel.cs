using AttriRent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using AttriRent.Models.CustomModels;
using System.Windows.Controls;
using ViewModels;
using AttriRent.Commands.BaseCommand;
using System.Collections.ObjectModel;
using AttriRent.Models;
using Microsoft.EntityFrameworkCore;
using AttriRent.Views.Frames;

namespace AttriRent.ViewModel
{
    internal class AttributesViewModel : BaseViewModel
    {
        private readonly ObservableCollection<AttributeModel> _attributes;
        private ObservableCollection<AttributeModel> _orderedAttributes = new ObservableCollection<AttributeModel>();
        public ObservableCollection<AttributeModel> Attributes
        {
            get { return _orderedAttributes; }
            set
            {
                _orderedAttributes = value;
                OnPropertyChanged(nameof(Attributes));
            }
        }
        private string? _searchQuery;
        public string? SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        private bool _isOnlyInStock = false;
        public bool IsOnlyInStock
        {
            get { return _isOnlyInStock; }
            set
            {
                _isOnlyInStock = value;
                OnPropertyChanged(nameof(IsOnlyInStock));
            }
        }

        public AttributesViewModel() 
        {
            _attributes = GetData();
            FindFromSearchOptions();
        }

        private ObservableCollection<AttributeModel> GetData()
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var attributes = db.attributes.Select(a => a).ToList();
                ObservableCollection<AttributeModel> attributeModels = new ObservableCollection<AttributeModel>();
                
                foreach(var attribute in attributes)
                {
                    bool inStock = db.orders.Include(o => o.Attribute).OrderByDescending(o => o.order_end_day).FirstOrDefault(a => a.Attribute.id == attribute.id)?.activity_status ?? false;
                    
                    attributeModels.Add(new AttributeModel(attribute.id, attribute.name, attribute.price, attribute.description, attribute.image_path, inStock));
                }

                return attributeModels;
            }
        }
        private Command _applyQueryCommand;
        public Command ApplyQueryCommand
        {
            get
            {
                return _applyQueryCommand ??
                (_applyQueryCommand = new Command(obj =>
                {
                    FindFromSearchOptions();
                }));
            }
        }

        private Command _resetCommand;
        public Command ResetCommand
        {
            get
            {
                return _resetCommand ??
                (_resetCommand = new Command(obj =>
                {
                    SearchQuery = string.Empty;
                    IsOnlyInStock = false;

                    FindFromSearchOptions();
                }));
            }
        }

        public void FindFromSearchOptions()
        {
            Attributes.Clear();

            var attributes = new List<AttributeModel>();

            if(IsOnlyInStock)
            {
                foreach (var attribute in _attributes)
                {
                    if (!attribute.inStock)
                        attributes.Add(attribute);
                }
            }
            else
            {
                attributes.AddRange(_attributes);
            }

            if (!string.IsNullOrWhiteSpace(SearchQuery))
                attributes = attributes.Select(a => a).Where(a => a.name.ToLower().Trim().Contains(SearchQuery!.ToLower().Trim())).ToList();

            foreach (var atr in attributes)
            {
                Attributes.Add(atr);
            }
        }
    }
}
