using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DesignPattern.DesignPatterns
{
    public class Adapter : Pattern
    {
        // Class that client used in application
        public class Hero
        {
            public Hero() { }

            public string Name { get; set; }
            public string Attribute { get; set; }

            public string GetInfo()
            {
                return $"Name: {Name}, Attribute: {Attribute}";
            }
        }

        // Actual Service that provide data as JSON
        public class HeroJSONAPI
        {
            public string Hero()
            {
                Dictionary<string, object> json = new Dictionary<string, object>() { { "Name", "Axe" }, { "Attribute", "Strength" } };
                return JsonConvert.SerializeObject(json);
            }
        }

        // Adapter & Interface
        public interface IHeroAdapter
        {
            Hero GetHero();
        }

        public class HeroJSONAdapter : IHeroAdapter
        {
            public Hero GetHero()
            {
                var api = new HeroJSONAPI();
                Dictionary<string, object> json = JsonConvert.DeserializeObject<Dictionary<string, object>>(api.Hero());
                var hero = new Hero() { Name = json["Name"].ToString(), Attribute = json["Attribute"].ToString() };
                return hero;
            }
        }

        /// <summary>
        /// Problem: When we need to add some implement without touching existing code
        /// Solved: Use the interface for adapt new service to actual result
        /// </summary>
        public override void Demo()
        {
            IHeroAdapter adapter = new HeroJSONAdapter();
            var hero = adapter.GetHero();
            Console.WriteLine(hero.GetInfo());
        }
    }
}
