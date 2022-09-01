using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Insert(TEntity entity);
        List<TEntity> GetAll();

        TEntity GetById(int id);
    }
}