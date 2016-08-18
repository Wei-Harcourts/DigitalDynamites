using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harcourts.eOpen.Web.Models
{
    public class FacebookVisitor : Visitor
    {
        public bool IsFacebookVisitor => true;

        public string FacebookUserId { get; set; }
    }
}