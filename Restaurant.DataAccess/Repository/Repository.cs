using Microsoft.EntityFrameworkCore;
using Restaurant.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DataAccess.Repository
{
    //Imlements generic Repository

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
       //from applicationdbcontext
        internal DbSet<T> dbSet; 

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.Product.Include(u => u.Category);
            //this.dbset = _db.Categories; (same as below)
            this.dbSet = _db.Set<T>();
        }

        public void Add(T item)
        {
            dbSet.Add(item);    
        }

        //includeProp - "Category"
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null) 
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProp);
                }    
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {

            IQueryable<T> query = dbSet;

            query= query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> item)
        {
            dbSet.RemoveRange(item);
        }
    }
}
