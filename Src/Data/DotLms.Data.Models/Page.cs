using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bytes2you.Validation;
using DotLms.Data.Models.Contracts;
using DotLms.Services.Providers;
using DotLms.Services.Providers.Contracts;

namespace DotLms.Data.Models
{
    public class Page
    {
        public Page()
        {
            this.ChildPages = new HashSet<Page>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string UglyName { get; set; }

        [Required]
        [MinLength(1)]
        public string HtmlContent { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Url { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? LastEditedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public User Author { get; set; }

        public User LastEditedBy { get; set; }

        public bool IsPublished { get; set; }

        public Page ParentPage { get; set; }

        public virtual ICollection<Page> ChildPages { get; set; }
    }
}