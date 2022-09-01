using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DH.Entities.Concrete;
using DH.Core.DataAccess;

namespace DH.DataAccess.Abstract
{
    public interface IPostDal : IEntityRepository<Post>
    {
        public Post InsertReply(Post entity);
        public List<Post> GetReplies(int id);
    }
}