using AttriRent.Commands.BaseCommand;
using AttriRent.ViewModel.Navigation;
using AttriRent.Views.Frames;
using System;

namespace AttriRent.Models.CustomModels
{
    class AttributeModel
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public int price { get; set; }
        public bool inStock { get; set; }
        public string? description { get; set; }
        private string? _imagePath { get; set; }
        public string imagePath 
        { 
            get
            {
                if (_imagePath != null)
                    return _imagePath;
                else
                    return "/LocalImages/DefaultImage.jpg";
            }
        }

        private Command _selectItemCommand;
        public Command SelectItemCommand
        {
            get
            {
                return _selectItemCommand ??
                    (_selectItemCommand = new Command(obj =>
                    {
                        ApplicationInfo.SetNewPage(new AttributePage((int)obj), nameof(AttributePage));
                    }));
            }
        }

        public AttributeModel (int id, string name, int price, string? description, string? imagePath, bool inStock)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            _imagePath = imagePath;
            this.inStock = inStock;
        }
    }
}
