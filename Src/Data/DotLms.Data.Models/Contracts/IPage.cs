using System;
using System.Collections.Generic;

namespace DotLms.Data.Models.Contracts
{
    public interface IPage
    {
        int Id { get; set; }
        string Name { get; set; }
        string UglyName { get; set; }
        string HtmlContent { get; set; }
        string Url { get; set; }
        DateTime? CreatedOn { get; set; }
        DateTime? LastEditedOn { get; set; }
        DateTime? DeletedOn { get; set; }
        User Author { get; set; }
        User LastEditedBy { get; set; }
        bool IsPublished { get; set; }
        Page ParentPage { get; set; }
        ICollection<Page> ChildPages { get; set; }
    }
}