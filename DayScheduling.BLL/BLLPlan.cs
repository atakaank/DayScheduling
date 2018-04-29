using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayScheduling.Entities.Plan;
using DayScheduling.Entities.Activity;
using DayScheduling.Entities.Account;
using DayScheduling.Data;
using System.Collections;
using GeoWeb;

namespace DayScheduling.BLL
{
    public class BLLPlan
    {
        BLLActivity bllAct = new BLLActivity();
        DALPlace dalplace = new DALPlace();
        DALPlaceType dalpt = new DALPlaceType();
        
        public vmDayByDayPlan GetvmDayByDay(pmPlanCriteria param)
        {
            LocationManager locationManager = new LocationManager();
            Dictionary<string, string> MustDoList = new Dictionary<string, string>();
            Place currentPlace = new Place();
            //MustDoList.Add("AllComplete", "0");
            for (int i = 0; i < param.categoryGroupNames.Count && param.categoryGroupNames != null; i++)
            {
                MustDoList.Add(param.categoryGroupNames.ElementAt(i), param.categoryGroupNames.ElementAt(i));
            } 
            if(!string.IsNullOrEmpty(param.FoodCategory))
            MustDoList.Add("food", param.FoodCategory);
            if (!string.IsNullOrEmpty(param.DrinkCategory))
            MustDoList.Add("drink", param.DrinkCategory);
            Random rnd = new Random();
            vmDayByDayPlan model = new vmDayByDayPlan();
            model.Province = Enum.GetName(typeof(Provinces), param.ProvinceId);
            model.Plan = new List<vmPartialActivity>();
            model.TravelList = new List<Travel>();
            model.CurrentTime = model.StartTime;
            while(MustDoList.Count !=0) // model.CurrentTime <= new TimeSpan(19,0,0) && hepsi yapılmışsa ve saat en az 19.00'u geçmişse. Hepsi yapılmış ve 19.00u geçmemişse yine random bir category seçilir.
            {
                Travel travel = new Travel();
                vmPartialActivity currentAct = new vmPartialActivity();
                DirectionLatLong direction = new DirectionLatLong();
                if (param.FoodCategory == "10" && model.CurrentTime == model.StartTime)
                {
                    currentAct = getBreakfastActivity(param, model.CurrentTime);
                    if(currentAct != null)
                    {
                        model.CurrentTime = currentAct.FinishTime;
                        MustDoList.Remove("food");
                        model.Plan.Add(currentAct);
                    }
                }
                int index = rnd.Next(0, MustDoList.Count - 1);
                if (MustDoList.ElementAt(index).Key == "culture")
                {
                    currentAct = getCulturelActivity(param, model.CurrentTime);
                    if (currentAct != null)
                    {
                        if(model.Plan.Count!= 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName, currentAct.place.PlaceName); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Replace(" mins", ""))));
                            if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours, 30, 0);
                            }
                            else if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours+1,0,0) && model.CurrentTime > new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours + 1, 0, 0);
                            }
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));

                            model.CurrentTime = currentAct.FinishTime;
                            model.TravelList.Add(travel);
                        }
                        MustDoList.Remove("culture");
                        model.Plan.Add(currentAct); 
                    }
                    else
                    {
                        MustDoList.Remove("culture");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "Shopping")
                {
                    currentAct = getShoppingActivity(param, model.CurrentTime); // Place bulamazsa activity null döncek.
                    if (currentAct != null) // null değilse zaten time güncellenip activity eklenecek.
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName, currentAct.place.PlaceName); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Replace(" mins", ""))));
                            if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours, 30, 0);
                            }
                            else if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours + 1, 0, 0) && model.CurrentTime > new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours + 1, 0, 0);
                            }
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));


                            model.CurrentTime = currentAct.FinishTime;
                            model.TravelList.Add(travel);
                        }
                        MustDoList.Remove("Shopping");
                        model.Plan.Add(currentAct);
                    }
                    else
                    {
                        MustDoList.Remove("Shopping");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if(MustDoList.ElementAt(index).Key == "Historic Sites")
                {
                    currentAct = getHistoricSitesActivity(param, model.CurrentTime);
                    if(currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName, currentAct.place.PlaceName); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Replace(" mins", ""))));
                            if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours, 30, 0);
                            }
                            else if (model.CurrentTime <= new TimeSpan(model.CurrentTime.Hours + 1, 0, 0) && model.CurrentTime > new TimeSpan(model.CurrentTime.Hours, 30, 0))
                            {
                                currentAct.StartTime = new TimeSpan(model.CurrentTime.Hours + 1, 0, 0);
                            }
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));


                            model.CurrentTime = currentAct.FinishTime;
                            model.TravelList.Add(travel);
                        }
                        model.CurrentTime = currentAct.FinishTime;
                        MustDoList.Remove("Historic Sites");
                        model.Plan.Add(currentAct);
                    }
                    else
                    {
                        MustDoList.Remove("Historic Sites");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                    //model.CurrentTime = new TimeSpan(19, 30, 0);
            }
            return model;
        }

        private vmPartialActivity getBreakfastActivity(pmPlanCriteria param,TimeSpan currentTime)
        {
            vmPartialActivity breakfastActivity = new vmPartialActivity();
            Place placeBreakfast = dalplace.getBreakfastPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if(placeBreakfast != null)
            {
                breakfastActivity.place = placeBreakfast;
                breakfastActivity.StartTime = currentTime;
                breakfastActivity.FinishTime = breakfastActivity.StartTime.Add(TimeSpan.FromHours(breakfastActivity.place.RecommendedDuration));
                return breakfastActivity;
            }
            return null;
        }

        private vmPartialActivity getCulturelActivity(pmPlanCriteria param, TimeSpan currentTime)
        {
            vmPartialActivity culturelActivity = new vmPartialActivity();
            Place placeCulture = dalplace.getCulturelPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if (placeCulture != null)
            {
                culturelActivity.place = placeCulture;
                //culturelActivity.StartTime = currentTime;
                //culturelActivity.FinishTime = culturelActivity.StartTime.Add(TimeSpan.FromHours(culturelActivity.place.RecommendedDuration));
                return culturelActivity;
            }
            return null;
        }

        private vmPartialActivity getShoppingActivity(pmPlanCriteria param, TimeSpan currentTime)
        {
            vmPartialActivity shoppingActivity = new vmPartialActivity();
            Place placeShopping = dalplace.getShoppingPlace(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if(placeShopping != null)
            {
                shoppingActivity.place = placeShopping;
                shoppingActivity.StartTime = currentTime;
                //shoppingActivity.FinishTime = shoppingActivity.StartTime.Add(TimeSpan.FromHours(shoppingActivity.place.RecommendedDuration));
                return shoppingActivity;
            }
            return null;
        }

        private vmPartialActivity getHistoricSitesActivity(pmPlanCriteria param,TimeSpan currentTime)
        {
            vmPartialActivity historicSitesActivity = new vmPartialActivity();
            Place placeHistoricSite = dalplace.getHistoricSites(param.ProvinceId, param.BudgetInfo, param.style, param.NumberOfFriends);
            if (placeHistoricSite != null)
            {
                historicSitesActivity.place = placeHistoricSite;
                //historicSitesActivity.StartTime = currentTime;
                //historicSitesActivity.FinishTime = historicSitesActivity.StartTime.Add(TimeSpan.FromHours(historicSitesActivity.place.RecommendedDuration));
                return historicSitesActivity;
            }
            return null;
        }
    }
}
