using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Builder : Pattern
    {

        #region Product
        public interface IHouse
        {
            List<string> Parts { get; set; }
            void ListAllPart();
        }

        public class House : IHouse
        {
            public House()
            {
                Parts = new List<string>();
            }
            public List<string> Parts { get; set; }
            public void ListAllPart()
            {
                foreach (var item in Parts) Console.WriteLine(item);
            }
        }
        #endregion

        #region Builder
        public interface IHouseBuilder
        {
            void BuildWalls();
            void BuildDoors();
            void BuildRoof();
            IHouse GetHouse();
        }

        public class WoodenHouseBuilder : IHouseBuilder
        {
            private House house = new House();

            public void BuildDoors() => house.Parts.Add("Wooden doors");

            public void BuildRoof() => house.Parts.Add("Wooden roof");

            public void BuildWalls() => house.Parts.Add("Wooden walls");

            public IHouse GetHouse() => house;
        }

        public class BrickHouseBuilder : IHouseBuilder
        {
            private House house = new House();

            public void BuildDoors() => house.Parts.Add("Brick doors");

            public void BuildRoof() => house.Parts.Add("Brick roof");

            public void BuildWalls() => house.Parts.Add("Brick walls");

            public IHouse GetHouse() => house;
        }
        #endregion

        #region Director 
        public class HouseDirector
        {
            private readonly IHouseBuilder _builder = null;
            public HouseDirector(IHouseBuilder builder)
            {
                _builder = builder;
            }

            public void CreateWoodenHouse() 
            {
                _builder.BuildDoors();
                _builder.BuildRoof();
                _builder.BuildWalls();
            }

            public void CreateBrickHouse()
            {
                _builder.BuildWalls();
                _builder.BuildRoof();
                _builder.BuildDoors();
            }
        }
        #endregion

        /// <summary>
        /// Problem: If we have to initial some object that has many step or complex to initialize.
        /// Solved: Use Builder pattern to solve the problem by create the Product, Builder and Director.
        /// The Builder will handle how to create the object.
        /// The Director will handle to manage the builder for works.
        /// And in finally we will get the product via Builder get result method (GetHouse in this case).
        /// </summary>
        public override void Demo()
        {
            Console.WriteLine($"================Builder================{Environment.NewLine}");
            // in case we want to created wooden house
            var woodenHouseBuilder = new WoodenHouseBuilder();
            var houseDirector = new HouseDirector(woodenHouseBuilder);
            houseDirector.CreateWoodenHouse();
            var woodenHouse = woodenHouseBuilder.GetHouse();
            woodenHouse.ListAllPart();

            // in case we want to create brick house
            var brickHouseBuilder = new BrickHouseBuilder();
            houseDirector = new HouseDirector(brickHouseBuilder);
            houseDirector.CreateBrickHouse();
            var brickHouse = brickHouseBuilder.GetHouse();
            brickHouse.ListAllPart();
            Console.WriteLine($"{Environment.NewLine}================Builder================");
        }
    }
}
