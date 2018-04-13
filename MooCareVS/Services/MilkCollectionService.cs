using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data;
using System.Data.Objects;

namespace Services
{
    public class MilkCollectionService
    {
        CowRepository repoCow;
        public MilkCollectionService()
        {
            repoCow = new CowRepository();
        }

        public bool AddYield(int idCow, double yieldMilk)
        {
            try
            {
                //Verifica se a vaca existe
                Cow cowValid = repoCow.GetCow(idCow);
                if(cowValid != null){
                    Lactation currentLact = cowValid.lactations.FirstOrDefault(l => l.finished == false);
                    Yield yieldDay = currentLact.yields.FirstOrDefault(y => y.date.Year == DateTime.Now.Year && y.date.Month == DateTime.Now.Month && y.date.Day == DateTime.Now.Day);
                    if (yieldDay == null)
                    {
                        //Insere os dados na lactação corrente
                        Yield yield = new Yield
                        {
                            idLactation = currentLact.idLactation,
                            totalYield = yieldMilk,
                            date = DateTime.Now
                        };
                        repoCow.AddYield(yield);
                    }else
                    {
                        //Altera a produção do dia atual somando a quantidade informada
                        yieldDay.totalYield += yieldMilk;
                        repoCow.UpdateYield(yieldDay);
                    }
                  return true;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return false;
        }
    }
}
