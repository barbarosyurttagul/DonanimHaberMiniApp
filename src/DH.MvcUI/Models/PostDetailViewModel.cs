using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Entities.Concrete;

namespace DH.MvcUI.Models
{
    public class PostDetailViewModel
    {
        public Post Post { get; set; }
        public List<Post> Replies { get; set; }
    }
}