using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Repositories
{
    public class CowRepository
    {
        DbContextEntities db;

        public CowRepository()
        {
            db = new DbContextEntities();
        }
        public Cow GetCow(int idCow)
        {
            Cow cow = db.Cows
                .Include("lactations.yields")
                .FirstOrDefault(c => c.idCow == idCow);
            return cow;
        }

        public IEnumerable<Cow> GetCowByQuery(Expression<Func<Cow,bool>> query, string[] includes = null)
        {
            includes = includes ?? new string[0];
            IQueryable<Cow> queryInclude = db.Cows.Where(query);

            foreach (var include in includes)
                queryInclude = queryInclude.Include(include);

            return queryInclude;
        }

        public void AddYield(Yield yield)
        {
            db.Yields.Add(yield);
            db.SaveChanges();
        }

        public void UpdateYield(Yield yield)
        {
            Yield yieldUpdate = db.Yields.FirstOrDefault(y => y.idYield == yield.idYield);
            yieldUpdate.date = yield.date;
            yieldUpdate.totalYield = yield.totalYield;
            db.SaveChanges();
        }
    }
}
