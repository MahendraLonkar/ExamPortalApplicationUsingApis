using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExamPortalApplicationUsingApis.Models;
using System.Data;
namespace ExamPortalApplicationUsingApis.BL
{
    public class RepositoryBL<TEntity,TKey>:IRepositoryBL<TEntity,TKey> where TEntity:class
    {
        CIIT_CRMDBEntities dbobj;
        public RepositoryBL(CIIT_CRMDBEntities dbobj)
        {
            this.dbobj = dbobj;
        }

        public void Create(TEntity entity)
        {
            dbobj.Set<TEntity>().Add(entity);
            Save();

        }

        public void Update(TEntity entity)
        {
            dbobj.Entry<TEntity>(entity).State = EntityState.Modified;
            Save();

        }

        public void Delete(TEntity entity)
        {
            dbobj.Entry<TEntity>(entity).State = EntityState.Modified;
            Save();
           
        }

        public List<TEntity> GetAll()
        {
            return dbobj.Set<TEntity>().ToList();
        }

        public TEntity GetById(TKey key)
        {
          return  dbobj.Set<TEntity>().Find(key);
        }

        private void Save()
        {
            dbobj.SaveChanges();
        }
    }
}