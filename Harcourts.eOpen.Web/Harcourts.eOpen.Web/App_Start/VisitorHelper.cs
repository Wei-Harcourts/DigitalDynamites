using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Harcourts.eOpen.Web.Models;
using Newtonsoft.Json.Linq;

namespace Harcourts.eOpen.Web
{
    public static class VisitorHelper
    {
        public static Visitor Create(JToken token)
        {
            var test = ParseBoolean(token["isFacebookVisitor"] as JValue);
            return test ? token.ToObject<FacebookVisitor>() : token.ToObject<Visitor>();
        }

        private static bool ParseBoolean(JValue value)
        {
            var str = value?.Value?.ToString() ?? bool.FalseString;
            if (string.Equals(str, "1", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (string.Equals(str, "0", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return bool.Parse(str);
        }
    }
}