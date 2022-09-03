using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Entities.Concrete;
using X.PagedList;

namespace DH.MvcUI.Models
{
    public class PostIndexViewModel
    {
        public IPagedList<Post> posts { get; set; }
    }
}