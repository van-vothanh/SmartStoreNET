using System.Collections.Generic;

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
