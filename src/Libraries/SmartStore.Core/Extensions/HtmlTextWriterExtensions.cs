// TODO: .NET 8 Migration - HtmlTextWriter not supported
#if FALSE_NOT_COMPATIBLE
﻿using System.Collections.Generic;
using System.Linq;
// TODO: .NET 8 - System.Web.UI removed

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
