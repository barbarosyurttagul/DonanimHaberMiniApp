using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Core;

namespace DH.Entities.Concrete
{
    public class Post : IEntity
    {
        public int Id { get; set ; }
        public int RootId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PostTitle { get; set; }
        public string Content { get; set; }
        public DateTime DatePublished { get; set; }
    }
}