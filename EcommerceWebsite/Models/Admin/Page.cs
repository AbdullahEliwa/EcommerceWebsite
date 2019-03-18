using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceWebsite.Models.Admin
{
    public class Page
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        public byte Sorting { get; set; }

        public bool HasSideBar { get; set; }
    }
}