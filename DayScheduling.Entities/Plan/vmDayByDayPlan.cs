using DayScheduling.Entities.Activity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayScheduling.Entities.Plan
{
    public enum Provinces
    {
        [Display(Name = "Alsancak")]
        Alsancak = 1,
        [Display(Name = "Konak")]
        Konak = 2,
        [Display(Name = "Bornova")]
        Bornova = 3,
        [Display(Name = "Balçova")]
        Balçova = 4,
        Çeşme = 5,
        Urla = 6,
        Narlıdere = 7,
        Karşıyaka = 8,
        Bostanlı = 9,
        Mavişehir = 10,
        Güzelbahçe = 11,
        Gaziemir = 12,
        Buca = 13,
        Bayraklı = 14,
        Foça = 15,
        Seferihisar = 16,
        Karabağlar = 17,
        Karaburun = 18,
        Selçuk = 19,
        Ödemiş = 20,
        Bergama = 21,
        Tire = 22,
        Kemalpaşa = 23,
        Çiğli = 24,
        Dikili = 25,
        Üçyol = 26,
        Güzelyalı = 27,
        Özdere = 28
    }
    public enum FoodCategories
    {
        Breakfast = 10,
        [Display(Name = "Fast Food")]
        FastFood = 11,
        [Display(Name = "Meat&Chicken")]
        MeatChicken = 12,
        [Display(Name = "Sea Food")]
        SeaFood = 13,
        Desert = 30,
        Restaurant = 3
    }

    public enum ActivityTypeUpdate
    {
        Restaurant =3,        
        Breakfast = 10,
        [Display(Name = "Fast Food")]
        FastFood = 11,
        [Display(Name = "Meat&Chicken")]
        MeatChicken = 12,
        [Display(Name = "Sea Food")]
        SeaFood = 13,
        Homemade = 15,
        Patisserie = 30,
        Tavern = 40,
        Bar = 50,
        Club = 60,
        Fun = 1,
        Shopping = 2,
        Beaches = 3,
        Outdoors = 4,
        Relaxing = 5,
        Culturel = 6
    }

    public enum AlcoholCategories
    {
        Tavern = 40,
        Bar = 50,
        Club = 60
    }

    public class Travel
    {
        public string tDuration { get; set; }
        public string tSourceLat { get; set; }
        public string tSourceLong { get; set; }
        public string tDestinationLat { get; set; }
        public string tDestinationLong { get; set; }
    }

    public class vmDayByDayPlan
    {
        public int PlanID { get; set; } 
        public string Province { get; set; }
        public int ProvinceID { get; set; }
        public List<vmPartialActivity> Plan { get; set; }
        public TimeSpan StartTime = new TimeSpan(10,0,0);
        public TimeSpan CurrentTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public List<Travel> TravelList { get; set; }
        public string Popularity { get; set; }
    }

    public class vmPlanCriteria
    {
        public Provinces ProvinceId { get; set; }

    }

    public class pmPlanCriteria
    {
        public int ProvinceId { get; set; }
        public DateTime PlanBeginTime { get; set; }
        public DateTime PlanEndTime { get; set; }
        public int NumberOfFriends { get; set; }
        public int BudgetInfo { get; set; }
        public string style { get; set; }
        public List<string> categoryGroupNames { get; set; }
        public string FoodCategory { get; set; }
        public string AlcoholCategory { get; set; }
    }
    public class vmPlaceDetail
    {
        public int PlaceID { get; set; }
        public string PlaceName { get; set; }
        public string ActivityStartTime { get; set; }
        public string ActivityFinishTime { get; set; }
        public string PlaceDescription { get; set; }
        public int PlaceRate { get; set; }
        public string PlaceAddress { get; set; }
        public string Phone { get; set; }
        public int RecomendedDuration { get; set; }
        public string Photo { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
    public class pmPlaceDetail
    {
        public int PlaceID { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan FinishTime { get; set; }
    }

    public class pmUpdateActivity
    {
        public int updatePlanIDSave { get; set; }
        public int updateActivityID { get; set; }
        //public int updateActivityIDSave { get; set; }
        public int updateProvinceID { get; set; }
        public int updatePlaceIDSave { get; set; }
        public string updateActivityTypeSave { get; set; }
    }

}
