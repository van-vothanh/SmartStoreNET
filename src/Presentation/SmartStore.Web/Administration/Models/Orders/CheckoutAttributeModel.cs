using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Localization;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.Admin.Models.Orders
{
    [Validator(typeof(CheckoutAttributeValidator))]
    public class CheckoutAttributeModel : EntityModelBase, ILocalizedModel<CheckoutAttributeLocalizedModel>
    {
        public CheckoutAttributeModel()
        {
            Locales = new List<CheckoutAttributeLocalizedModel>();
            AvailableTaxCategories = new List<SelectListItem>();
        }

        [SmartResourceDisplayName("Common.IsActive")]
        public bool IsActive { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.Name")]
        public string Name { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TextPrompt")]
        public string TextPrompt { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.IsRequired")]
        public bool IsRequired { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.ShippableProductRequired")]
        public bool ShippableProductRequired { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TaxCategory")]
        public int? TaxCategoryId { get; set; }
        public IList<SelectListItem> AvailableTaxCategories { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.AttributeControlType")]
        public int AttributeControlTypeId { get; set; }
        [SmartResourceDisplayName("Admin.Catalog.Attributes.AttributeControlType")]
        public string AttributeControlTypeName { get; set; }

        [SmartResourceDisplayName("Common.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<CheckoutAttributeLocalizedModel> Locales { get; set; }

        [UIHint("Stores")]
        [AdditionalMetadata("multiple", true)]
        [SmartResourceDisplayName("Admin.Common.Store.LimitedTo")]
        public int[] SelectedStoreIds { get; set; }

        [SmartResourceDisplayName("Admin.Common.Store.LimitedTo")]
        public bool LimitedToStores { get; set; }
    }

    public class CheckoutAttributeLocalizedModel : ILocalizedModelLocal
    {
        public int LanguageId { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.Name")]
        public string Name { get; set; }

        [SmartResourceDisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TextPrompt")]
        public string TextPrompt { get; set; }
    }

    public partial class CheckoutAttributeValidator : AbstractValidator<CheckoutAttributeModel>
    {
        public CheckoutAttributeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}