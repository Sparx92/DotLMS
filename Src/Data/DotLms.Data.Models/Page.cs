using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bytes2you.Validation;
using DotLms.Services.Providers;

namespace DotLms.Data.Models
{
    public class Page
    {
        private DateTimeProvider dateTimeProvider;

        public Page(DateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(dateTimeProvider, nameof(dateTimeProvider)).IsNull().Throw();

            this.dateTimeProvider = dateTimeProvider;

            this.CreatedOn = this.dateTimeProvider.UtcNow();
            this.LastEditedOn = this.dateTimeProvider.UtcNow();
            this.IsPublished = false;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string UglyName { get; set; }

        [Required]
        [MinLength(1)]
        public string HtmlContent { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
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