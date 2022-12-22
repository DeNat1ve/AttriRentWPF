using AttriRent.Commands.BaseCommand;
using AttriRent.DAL;
using AttriRent.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.Models.DatagridModels
{
    public class DatagridOrder
    {
        public string UserName { get; set; } = null!;
        public string AttributeName { get; set; } = null!;
        public int OrderId { get; set; }
        public string OrderStartDay
        {
            get { return _orderStartDay.ToShortDateString(); }
        }
        public string OrderEndDay
        {
            get { return _orderEndDay.ToShortDateString(); }
        }
        private DateTime _orderStartDay { get; set; }
        private DateTime _orderEndDay { get; set; }

        public DatagridOrder(string userName, string attributeName, int attributeId, DateTime orderStartDay, DateTime orderEndDay)
        {
            UserName = userName;
            AttributeName = attributeName;
            OrderId = attributeId;
            _orderStartDay = orderStartDay;
            _orderEndDay = orderEndDay;
        }

        public void ChangeEndRentDate(DateTime newDate)
        {
            _orderEndDay = newDate;
        }

    }
}
