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

        public bool AddYield(int idCow, double yieldMilk, DateTime? date = null)
        {
            try
            {
                DateTime dateYield;
                dateYield = (date ?? DateTime.Now);
                dateYield = new DateTime(dateYield.Year, dateYield.Month, dateYield.Day);
                
                //Verifica se a vaca existe
                Cow cowValid = repoCow.GetCow(idCow);
                if(cowValid != null){
                    Lactation currentLact = cowValid.lactations.FirstOrDefault(l => l.finished == false);
                    Yield yieldDay = currentLact.yields.FirstOrDefault(y => y.date.Year == dateYield.Year && y.date.Month == dateYield.Month && y.date.Day == dateYield.Day);
                    if (yieldDay == null)
                    {
                        //Insere os dados na lactação corrente
                        Yield yield = new Yield
                        {
                            idLactation = currentLact.idLactation,
                            totalYield = yieldMilk,
                            date = dateYield,
                            dayLactation = (int)(dateYield - currentLact.dateBirth).TotalDays
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
                //throw;
            }

            return false;
        }
    }
}
