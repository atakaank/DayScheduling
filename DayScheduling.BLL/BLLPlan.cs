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
using DayScheduling.BLL;

namespace DayScheduling.BLL
{
    public class BLLPlan
    {
        BLLActivity bllAct = new BLLActivity();
        DALPlace dalplace = new DALPlace();
        DALPlaceType dalpt = new DALPlaceType();
        DALPlan dalplan = new DALPlan();

        #region GetvmDayByDay
        public vmDayByDayPlan GetvmDayByDay(pmPlanCriteria param)
        {
            LocationManager locationManager = new LocationManager();
            Dictionary<string, string> MustDoList = new Dictionary<string, string>();
            Place currentPlace = new Place();
            for (int i = 0; i < param.categoryGroupNames.Count && param.categoryGroupNames != null; i++)
            {
                MustDoList.Add(param.categoryGroupNames.ElementAt(i), param.categoryGroupNames.ElementAt(i));
            }
            if (!string.IsNullOrEmpty(param.FoodCategory))
                MustDoList.Add("food", param.FoodCategory);
            if (!string.IsNullOrEmpty(param.AlcoholCategory))
                MustDoList.Add("alcohol", param.AlcoholCategory);
            Random rnd = new Random();
            vmDayByDayPlan model = new vmDayByDayPlan();
            model.Province = Enum.GetName(typeof(Provinces), param.ProvinceId);
            model.ProvinceID = param.ProvinceId;
            model.Plan = new List<vmPartialActivity>();
            model.TravelList = new List<Travel>();
            model.CurrentTime = model.StartTime;
            model.Popularity = param.style;
            recordPlan(model);
            //dalplan.RecordPlantoHistory(AccountUser.Account.AccountID, model.PlanID);
            while (MustDoList.Count != 0) // model.CurrentTime <= new TimeSpan(19,0,0) && hepsi yapılmışsa ve saat en az 19.00'u geçmişse. Hepsi yapılmış ve 19.00u geçmemişse yine random bir category seçilir.
            {
                Travel travel = new Travel();
                vmPartialActivity currentAct = new vmPartialActivity();
                DirectionLatLong direction = new DirectionLatLong();
                if (param.FoodCategory == "10" && model.CurrentTime == model.StartTime)
                {
                    currentAct = bllAct.getBreakfastActivity(param, model.CurrentTime, "food");
                    if (currentAct != null)
                    {
                        model.CurrentTime = currentAct.FinishTime;
                        MustDoList.Remove("food");
                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                }
                int index = rnd.Next(0, MustDoList.Count - 1);
                if (MustDoList.ElementAt(index).Key == "culture")
                {
                    currentAct = bllAct.getCulturelActivity(param,"Culture");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); 
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("culture");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("culture");
                        }

                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("culture");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "Shopping")
                {
                    currentAct = bllAct.getShoppingActivity(param, "Shopping"); // Place bulamazsa activity null döncek.
                    if (currentAct != null) // null değilse zaten time güncellenip activity eklenecek.
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("Shopping");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("Shopping");
                        }

                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("Shopping");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "Historic Sites")
                {
                    currentAct = bllAct.getHistoricSitesActivity(param, "Historic Sites");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));// Replace(" mins", ""))));
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
                            MustDoList.Remove("Historic Sites");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("Historic Sites");
                        }

                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("Historic Sites");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "fun")
                {
                    currentAct = bllAct.getFunActivity(param, "Fun");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("fun");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("fun");
                        }
                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("fun");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "Beaches")
                {
                    currentAct = bllAct.getBeachActivity(param, "Beaches");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("Beaches");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("Beaches");
                        }

                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("Beaches");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "unwind")
                {
                    currentAct = bllAct.getRelaxingActivity(param, "Relaxing");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("unwind");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("unwind");
                        }

                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("unwind");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "outdoors")
                {
                    currentAct = bllAct.getOutdoorActivity(param, "outdoors");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                            currentAct.SourceLat = direction.SourceLat;
                            currentAct.SourceLong = direction.SourceLong;
                            currentAct.DestLat = direction.DestinationLat;
                            currentAct.DestLong = direction.DestinationLong;
                            currentAct.DirectionDuration = direction.Duration;

                            model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                            MustDoList.Remove("outdoors");
                            model.TravelList.Add(travel);
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            MustDoList.Remove("outdoors");
                        }
                        model.Plan.Add(currentAct);
                        currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                    }
                    else
                    {
                        MustDoList.Remove("outdoors");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "food")
                {
                    bool hourControl = true;
                    currentAct = bllAct.getFoodActivity(param, MustDoList.ElementAt(index).Value,"food");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            if ((currentAct.place.PlaceTypeID == 3 && model.CurrentTime >= new TimeSpan(16, 0, 0)) || currentAct.place.PlaceTypeID != 3) //restaurant olup saat 4ten önce değilse veya restaurant değilse.
                            {
                                direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                                currentAct.SourceLat = direction.SourceLat;
                                currentAct.SourceLong = direction.SourceLong;
                                currentAct.DestLat = direction.DestinationLat;
                                currentAct.DestLong = direction.DestinationLong;
                                currentAct.DirectionDuration = direction.Duration;

                                model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                                model.Plan.Add(currentAct);
                                currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                                MustDoList.Remove("food");
                                model.TravelList.Add(travel);
                            }
                            else
                            {
                                hourControl = false;
                            }
                        }
                        else
                        {
                            currentAct.StartTime = model.CurrentTime;
                            currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration));
                            model.CurrentTime = currentAct.FinishTime;
                            model.Plan.Add(currentAct);
                            currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                            MustDoList.Remove("food");
                        }
                        if (!hourControl && MustDoList.Count == 1 && currentAct.place.PlaceTypeID == 3) // sadece alkol aktivitesi kaldıysa ve o meyhane veya bar ise saat başka aktivite ile artmıcağından saati ileri alıyoruz.
                            model.CurrentTime = new TimeSpan(16, 0, 0);
                    }
                    else
                    {
                        MustDoList.Remove("food");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                else if (MustDoList.ElementAt(index).Key == "alcohol")
                {
                    bool hourControl = true; //club bar için saatin uygunluğunu detect etmeye yarıyor.
                    currentAct = bllAct.getAlcoholActivity(param, MustDoList.ElementAt(index).Value, "alcohol");
                    if (currentAct != null)
                    {
                        if (model.Plan.Count != 0) //ondan önce activityler varsa ilk activity değilse.
                        {
                            if (((currentAct.place.PlaceTypeID == 50 || currentAct.place.PlaceTypeID == 40) && model.CurrentTime >= new TimeSpan(16, 0, 0)) || (currentAct.place.PlaceTypeID == 60 && model.CurrentTime >= new TimeSpan(20, 0, 0)))
                            {
                                direction = locationManager.getDirection(model.Plan[model.Plan.Count - 1].place.PlaceName + " " + model.Plan[model.Plan.Count - 1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress); //adress yapılcak.
                                currentAct.SourceLat = direction.SourceLat;
                                currentAct.SourceLong = direction.SourceLong;
                                currentAct.DestLat = direction.DestinationLat;
                                currentAct.DestLong = direction.DestinationLong;
                                currentAct.DirectionDuration = direction.Duration;

                                model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
                                model.Plan.Add(currentAct);
                                currentAct.ActivityID = model.PlanID.ToString() + model.Plan.IndexOf(currentAct).ToString(); //ActivityID
                                MustDoList.Remove("alcohol");
                                model.TravelList.Add(travel);
                            }
                            else
                            {
                                hourControl = false; // saat uygun değilse false.
                            }
                        }
                        //else
                        //{
                        //    currentAct.StartTime = model.CurrentTime;
                        //    currentAct.FinishTime = currentAct.StartTime.Add(TimeSpan.FromHours(currentAct.place.RecommendedDuration)); // ilk aktivite olamaz.
                        //    model.CurrentTime = currentAct.FinishTime;
                        //    MustDoList.Remove("alcohol");
                        //}
                        if (!hourControl && MustDoList.Count == 1 && (currentAct.place.PlaceTypeID == 40 || currentAct.place.PlaceTypeID == 50)) // sadece alkol aktivitesi kaldıysa ve o meyhane veya bar ise saat başka aktivite ile artmıcağından saati ileri alıyoruz.
                            model.CurrentTime = new TimeSpan(16, 0, 0);
                        else if (!hourControl && MustDoList.Count == 1 && currentAct.place.PlaceTypeID == 60)// sadece alkol aktivitesi kaldıysa ve o club ise saat başka aktivite ile artmıcağından saati ileri alıyoruz.
                            model.CurrentTime = new TimeSpan(20, 0, 0);
                        //MustDoList.Remove("alcohol");
                        //model.Plan.Add(currentAct);
                    }
                    else
                    {
                        MustDoList.Remove("alcohol");// nullsa da kaldırılması gerekiyor çünkü seçilen semtte kritere uygun mekan yok demektir ve kriter kaldırılır.
                    }
                }
                //model.CurrentTime = new TimeSpan(19, 30, 0);
            }
            bllAct.RecordActivities(model);
            return model;
        }
        #endregion

        #region GetExistPlan
        public vmDayByDayPlan GetExistPlan(int PlanID)
        {
            vmDayByDayPlan model = new vmDayByDayPlan();
            vmPartialActivity currentAct = new vmPartialActivity();
            model.Plan =  bllAct.GetActivities(PlanID);
            LocationManager locationManager = new LocationManager();
            DirectionLatLong direction = new DirectionLatLong();
            model.CurrentTime = model.StartTime;
            model.PlanID = model.Plan[0].PlanID; //Act olmadığı için.
            model.ProvinceID = model.Plan[0].place.ProvinceID;
            model.Province = Enum.GetName(typeof(Provinces), model.Plan[0].place.ProvinceID);
            model.Plan[0].StartTime = model.CurrentTime;
            model.Plan[0].FinishTime = model.Plan[0].StartTime.Add(TimeSpan.FromHours(model.Plan[0].place.RecommendedDuration));
            model.CurrentTime = model.Plan[0].FinishTime;
            for (int i=1; i<model.Plan.Count; i++)
            {
                currentAct = model.Plan[i];
                direction = locationManager.getDirection(model.Plan[i-1].place.PlaceName + " " + model.Plan[i-1].place.PlaceAddress, currentAct.place.PlaceName + " " + currentAct.place.PlaceAddress);
                currentAct.SourceLat = direction.SourceLat;
                currentAct.SourceLong = direction.SourceLong;
                currentAct.DestLat = direction.DestinationLat;
                currentAct.DestLong = direction.DestinationLong;
                currentAct.DirectionDuration = direction.Duration;

                model.CurrentTime = model.CurrentTime.Add(TimeSpan.FromMinutes(int.Parse(direction.Duration.Substring(0, direction.Duration.IndexOf(" ")))));
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
            }
            return model;
        }

        #endregion

        private void recordPlan(vmDayByDayPlan model)
        {
            Plan newPlan = new Plan();
            newPlan.PlanName = "Plan" + AccountUser.User.UserName + AccountUser.Account.AccountID;
            newPlan.PlanDate = DateTime.Now;
            newPlan.PlanPopularity = int.Parse(model.Popularity);
            dalplan.RecordPlan(newPlan.PlanName,newPlan.PlanDate,newPlan.PlanPopularity,model.ProvinceID,AccountUser.Account.AccountID);
            model.PlanID = dalplan.GetLastPlanID();
            //newPlan.PlanComplete
            //newPlan.PlanType             
        }

        public List<vmPlanBlock> GetPlanBlockList(int AccountID, bool userpage)
        {
            List<Plan> PlanList = dalplan.GetList(AccountID);
            List<vmPlanBlock> PlanBlockList = new List<vmPlanBlock>();
            int userPageBlockCount = 0;
            foreach (var item in PlanList)
            {
                if (userPageBlockCount == 3 && userpage)
                    break;
                vmPlanBlock planBlock = new vmPlanBlock();
                planBlock.PlanID = item.PlanID;
                planBlock.Popularity = Enum.GetName(typeof(Popularity), item.PlanPopularity);
                planBlock.ProvinceID = item.ProvinceID;
                planBlock.ProvinceName = Enum.GetName(typeof(Provinces), item.ProvinceID);
                List<vmPartialActivity> actList = bllAct.GetActivities(planBlock.PlanID);
                planBlock.Categories = actList[0].ActivityPlaceType;
                for (int i = 1; i < actList.Count; i++)
                {
                    //if (i == actList.Count - 1)
                    //{
                    //    planBlock.Categories += actList[i].ActivityPlaceType;
                    //}
                    planBlock.Categories += "," + actList[i].ActivityPlaceType;
                }
                PlanBlockList.Add(planBlock);
                userPageBlockCount++;
            }
            return PlanBlockList;
        }

        public void DeletePlan(int PlanID)
        {
            dalplan.DeletePlan(PlanID);
        }

        public vmPlaceDetail getPlaceDetail(pmPlaceDetail param)
        {
            vmPlaceDetail vmplacedetail = new vmPlaceDetail();
            Place place = dalplace.Get(param.PlaceID);
            vmplacedetail.PlaceID = param.PlaceID;
            vmplacedetail.Phone = place.Phone; //Null olabilir.
            vmplacedetail.PlaceAddress = place.PlaceAddress;
            vmplacedetail.PlaceDescription = place.PlaceDescription;
            vmplacedetail.PlaceName = place.PlaceName;
            vmplacedetail.PlaceRate = place.PlaceRate*20;
            vmplacedetail.RecomendedDuration = place.RecommendedDuration;
            vmplacedetail.ActivityStartTime = param.StartTime.ToString(@"hh\:mm");
            vmplacedetail.ActivityFinishTime = param.FinishTime.ToString(@"hh\:mm");
            vmplacedetail.Photo = param.PlaceID + ".jpg";
            return vmplacedetail;
        }
        //public vmPlaceDetail getPlaceDetail(pmPlaceDetail param)
        //{
        //    vmPlaceDetail vmplacedetail = new vmPlaceDetail();
        //    Place place = dalplace.Get(param.PlaceID);
        //    vmplacedetail.Phone = place.Phone;
        //    vmplacedetail.PlaceAddress = place.PlaceAddress;
        //    vmplacedetail.PlaceDescription = place.PlaceDescription;
        //    vmplacedetail.PlaceName = place.PlaceName;
        //    vmplacedetail.PlaceRate = place.PlaceRate;
        //    vmplacedetail.RecomendedDuration = place.RecommendedDuration;
        //    vmplacedetail.ActivityStartTime = param.StartTime;
        //    vmplacedetail.ActivityFinishTime = param.FinishTime;
        //    return vmplacedetail;
        //}
    }
}
