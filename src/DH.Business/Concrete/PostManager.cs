using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DH.Business.Abstract;
using DH.Core.CrossCuttingConcerns.Caching;
using DH.DataAccess.Abstract;
using DH.Entities.Concrete;

namespace DH.Business.Concrete
{
    public class PostManager : IPostService
    {
        private readonly IPostDal _postDal;
       
        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public List<Post> GetAll()
        {
            return _postDal.GetAll();
        }

        public List<Post> GetReplies(int id)
        {
            return _postDal.GetReplies(id);
        }
        public Post GetById(int id)
        {
            return _postDal.GetById(id);
        }    

        public Post Insert(Post entity)
        {
            return _postDal.Insert(entity);
        }

        public Post InsertReply(Post entity)
        {
            return _postDal.InsertReply(entity);
        }
    }
}