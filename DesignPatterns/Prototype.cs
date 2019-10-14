using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Prototype : Pattern
    {

        // Cloneable classes
        public abstract class Shape : ICloneable
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Shape() { }

            public Shape(Shape shape)
            {
                X = shape.X;
                Y = shape.Y;
            }

            public abstract object Clone();
        }

        public class Rectangle : Shape
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public Rectangle() { }

            public Rectangle(Rectangle shape) : base(shape)
            {
                Width = shape.Width;
                Height = shape.Height;
            }

            public override object Clone() => new Rectangle(this);
        }

        public class Circle : Shape
        {
            public int Radius { get; set; }

            public Circle() { }

            public Circle(Circle shape) : base(shape)
            {
                Radius = shape.Radius;
            }

            public override object Clone() => new Circle(this);
        }

        /// <summary>
        /// Problem: When we need to implement some class that can clone and get the quite accurate result
        /// Solved: Use Prototype pattern that let the base class implement ICloneable (in C# already has the ICloneable interface)
        /// </summary>
        public override void Demo()
        {
            Console.WriteLine($"================Prototype================{Environment.NewLine}");
            var rectangle = new Rectangle() { Width = 50, Height = 50 };
            var cloneRectangle = rectangle.Clone() as Rectangle;
            Console.WriteLine($"Original Rectangle width: {rectangle.Width}, Height: {rectangle.Height}");
            Console.WriteLine($"Clone Rectangle width: {cloneRectangle.Width}, Height: {cloneRectangle.Height}");
            Console.WriteLine($"{Environment.NewLine}================Prototype================");
        }
    }
}
