using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repositories;

namespace Services
{
    public class FoodService
    {
        FoodRepository foodRepo;
        public FoodService()
        {
            foodRepo = new FoodRepository();
        }

        public double calculateAmoutFood(Lactation currentLactation)
        {
            int windowSize = 7;
            double sumYield = 0;
            double avgYield = 0;
            double amoutFood = 0;

            List<Yield> yield = currentLactation.yields.ToList();
            for (int i = yield.Count - windowSize; i < yield.Count; i++)
            {
                sumYield += yield[i].totalYield;
            }

            avgYield = sumYield / windowSize;
            if(avgYield <= 5)
            {
                //1kg de concentrado para 3kg de leite
                amoutFood = avgYield / 3;
            }
            else
            {
                //1kg de concentrado para 2,5kg de leite produzido -> produzem acima de 5kg
                amoutFood = avgYield / 2.5;
            }

            //Salva a quantidade na base de dados
            foodRepo.Add(new Food {
                date = DateTime.Now,
                idCow = currentLactation.idCow,
                quantity = amoutFood                
            });

            return amoutFood;
        }
    }
}
