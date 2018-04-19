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
    public class LactationRepository
    {
        DbContextEntities db;
        public LactationRepository()
        {
            db = new DbContextEntities();
        }

        public Lactation GetLactationById(int idLactation)
        {
            return db.Lactations
                .Include("yields")
                .FirstOrDefault(l => l.idLactation == idLactation);
        }

        public IEnumerable<Lactation> GetLactationByCow(int idCow)
        {
            return db.Lactations.Where(l => l.idCow == idCow);
        }
        
        public IEnumerable<Lactation> GetLactationByQuery(Expression<Func<Lactation, bool>> query, string[] includes = null)
        {
            includes = includes ?? new string[0];
            IQueryable<Lactation> queryInclude = db.Lactations.Where(query);

            foreach (var include in includes)
                queryInclude = queryInclude.Include(include);

            return queryInclude;
        }

        public Lactation GetLastLactationFinished(int idCow, string[] includes = null)
        {
            includes = includes ?? new string[0];
            IQueryable<Lactation> queryInclude = db.Lactations
                .OrderByDescending(l => l.dateBirth)
                .Where(l => l.idCow == idCow && l.finished == true)
                .Take(1);

            foreach (var include in includes)
                queryInclude = queryInclude.Include(include);

            return queryInclude.FirstOrDefault();

            return db.Lactations
                .OrderByDescending(l => l.dateBirth)
                .Include("yields")
                .FirstOrDefault(l => l.finished == true);
        }
    }
}
