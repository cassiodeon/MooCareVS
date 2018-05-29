using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PredictionRepository
    {
        DbContextEntities db;

        public PredictionRepository()
        {
            db = new DbContextEntities();
        }

        public void Add(Prediction prediction)
        {
            db.Predictions.Add(prediction);
            db.SaveChanges();
        }
    }
}
