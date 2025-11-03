#if FALSE // TODO: Migrate to ASP.NET Core equivalents
﻿using System.Collections.Generic;
using System.Linq;
// using System.Web.UI; // TODO: Remove or replace

namespace SmartStore
{
    public static class HtmlTextWriterExtensions
    {
        public static void AddAttributes(this HtmlTextWriter writer, IDictionary<string, object> attributes)
        {
            if (attributes.Any())
            {
                foreach (var pair in attributes)
                {
                    if (pair.Value != null)
                        writer.AddAttribute(pair.Key, pair.Value.ToString(), true);
                }
            }
        }
    }
}
#endif
