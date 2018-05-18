using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FoodRepository
    {
        DbContextEntities db;
        public FoodRepository()
        {
            db = new DbContextEntities();
        }

        public void Add(Food food)
        {
            db.Foods.Add(food);
            db.SaveChanges();
        }

        public IEnumerable<Food> GetFoodByCow(int idCow)
        {
            return db.Foods.Where(f => f.idCow == idCow);
        }

        public IEnumerable<Food> GetFoodByQuery(Expression<Func<Food, bool>> query, string[] includes = null)
        {
            includes = includes ?? new string[0];
            IQueryable<Food> queryInclude = db.Foods.Where(query);

            foreach (var include in includes)
                queryInclude = queryInclude.Include(include);

            return queryInclude;
        }
    }
}
