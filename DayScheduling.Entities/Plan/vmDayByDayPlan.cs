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

    public enum DrinkCategories
    {
        CoffeShop = 110,
        Alcohol = 200
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
        public string Province { get; set; }
        public List<vmPartialActivity> Plan { get; set; }
        public TimeSpan StartTime = new TimeSpan(10,0,0);
        public TimeSpan CurrentTime { get; set; }
        public TimeSpan FinishTime { get; set; }
        public List<Travel> TravelList { get; set; }
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
        public string DrinkCategory { get; set; }
    }

    public class vmPlaceDetail
    {
        public string PlaceName { get; set; }
        public string ActivityStartTime { get; set; }
        public string ActivityFinishTime { get; set; }
        public string PlaceDescription { get; set; }
        public int PlaceRate { get; set; }
        public string PlaceAddress { get; set; }
        public string Phone { get; set; }
        public int RecomendedDuration { get; set; }
    }
    public class pmPlaceDetail
    {
        public int PlaceID { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
    }
}
