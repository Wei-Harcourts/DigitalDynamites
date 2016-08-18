using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harcourts.eOpen.Web.Models
{
    public class Visitor
    {
        public Visitor()
        {
            Name = string.Empty;
            PhoneNumber = string.Empty;
            EmailAddress = string.Empty;
        }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool IsBuyer { get; set; }
        public bool IsVendor { get; set; }
        public bool InTouch { get; set; }
        public string ListingNumber { get; set; }
    }
}