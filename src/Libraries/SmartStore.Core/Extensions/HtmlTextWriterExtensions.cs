using System.Collections.Generic;
using System.Linq;

namespace SmartStore
{
    // TODO: HtmlTextWriter not available in ASP.NET Core - use TagBuilder instead
    /*
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
    */
}
