using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.GoogleAnalytics.Models
{
    public class ConfigurationModel : ModelBase
    {

        public ConfigurationModel()
        {
            AvailableZones = new List<SelectListItem>();
        }

        [SmartResourceDisplayName("Admin.ContentManagement.Widgets.ChooseZone")]
        public string ZoneId { get; set; }
        public IList<SelectListItem> AvailableZones { get; set; }


        [SmartResourceDisplayName("Plugins.Widgets.GoogleAnalytics.GoogleId")]
        public string GoogleId { get; set; }

        [SmartResourceDisplayName("Plugins.Widgets.GoogleAnalytics.TrackingScript")]
        //tracking code
        public string TrackingScript { get; set; }

        [SmartResourceDisplayName("Plugins.Widgets.GoogleAnalytics.EcommerceScript")]
        public string EcommerceScript { get; set; }

        [SmartResourceDisplayName("Plugins.Widgets.GoogleAnalytics.EcommerceDetailScript")]
        public string EcommerceDetailScript { get; set; }

    }
}