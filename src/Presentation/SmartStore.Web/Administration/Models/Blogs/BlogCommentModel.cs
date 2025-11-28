using System;
using Microsoft.AspNetCore.Mvc;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;

namespace SmartStore.Admin.Models.Blogs
{
    public class BlogCommentModel : EntityModelBase
    {
        [SmartResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.BlogPost")]
        public int BlogPostId { get; set; }
        [SmartResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.BlogPost")]

        public string BlogPostTitle { get; set; }

        [SmartResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.Customer")]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        [SmartResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.IPAddress")]
        public string IpAddress { get; set; }

        [SmartResourceDisplayName("Admin.ContentManagement.Blog.Comments.Fields.Comment")]
        public string Comment { get; set; }

        [SmartResourceDisplayName("Common.CreatedOn")]
        public DateTime CreatedOn { get; set; }

    }
}