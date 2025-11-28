using System.Collections.Generic;
// using System.Web.WebPages; // Removed for .NET 8 migration

namespace SmartStore.Web.Framework.UI
{

    public interface IContentContainer
    {

        IDictionary<string, object> ContentHtmlAttributes
        {
            get;
        }

        HelperResult Content
        {
            get;
            set;
        }

    }

}
