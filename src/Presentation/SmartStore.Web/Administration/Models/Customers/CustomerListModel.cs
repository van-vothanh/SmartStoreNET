using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.Admin.Models.Customers
{
    public class CustomerListModel : ModelBase
    {
        public int GridPageSize { get; set; }

        [UIHint("CustomerRoles")]
        [AdditionalMetadata("multiple", true)]
        [SmartResourceDisplayName("Admin.Customers.Customers.List.CustomerRoles")]
        public int[] SearchCustomerRoleIds { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchEmail")]

        public string SearchEmail { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchUsername")]

        public string SearchUsername { get; set; }
        public bool UsernamesEnabled { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchTerm")]

        public string SearchTerm { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchDateOfBirth")]

        public string SearchDayOfBirth { get; set; }
        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchDateOfBirth")]

        public string SearchMonthOfBirth { get; set; }
        public bool DateOfBirthEnabled { get; set; }

        public bool CompanyEnabled { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchPhone")]

        public string SearchPhone { get; set; }
        public bool PhoneEnabled { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchZipCode")]

        public string SearchZipPostalCode { get; set; }
        public bool ZipPostalCodeEnabled { get; set; }

        [SmartResourceDisplayName("Admin.Customers.Customers.List.SearchActiveOnly")]
        public bool? SearchActiveOnly { get; set; }
    }
}