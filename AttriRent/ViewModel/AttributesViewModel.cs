using AttriRent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using AttriRent.Models.CustomModels;
using System.Windows.Controls;

namespace AttriRent.ViewModel
{
    internal class AttributesViewModel
    {
        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();

        public AttributesViewModel() 
        {
            GetData();
        }

        private void GetData()
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                var attributes = db.attributes.Select(a => a).ToList();

                foreach(var attribute in attributes)
                {
                    Attributes.Add(new AttributeModel(attribute.id, attribute.name, attribute.price, attribute.description));
                }
            }
        }
    }
}
