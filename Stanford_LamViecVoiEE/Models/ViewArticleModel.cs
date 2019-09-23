using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_LamViecVoiEE.Models
{
    public class ViewArticleModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

    }
}