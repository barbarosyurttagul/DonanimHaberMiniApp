using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Entities.Concrete;

namespace DH.Business.Abstract
{
    public interface IPostService
    {
        List<Post> GetAll();
        List<Post> GetReplies(int id);
        Post Insert(Post entity);
        Post GetById(int id);
        Post InsertReply(Post entity);
    }
}