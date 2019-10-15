using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Decorator : Pattern
    {
        
        #region component interface
        public interface INotifier
        {
            void Send(string message);
        }

        public interface IDrink
        {
            string Description { get; }
            double Price { get; }
            double GetPrice();
        }
        #endregion

        #region concrete component
        public class Notifier : INotifier
        {
            public void Send(string message) => Console.WriteLine($"General Send: {message}");
        }

        public class Coffee : IDrink
        {
            public string Description => "Coffee";
            public double Price => 45;
            public double GetPrice() => Price;
        }
        #endregion

        #region base decorator
        public class NotifierDecorator : INotifier
        {
            private readonly INotifier wrappee;
            public NotifierDecorator(INotifier notifier) => wrappee = notifier;
            public virtual void Send(string message) => wrappee.Send(message);
        }

        public class ToppingDecorator : IDrink
        {
            private readonly IDrink wrappee;
            public ToppingDecorator(IDrink drink) => wrappee = drink;
            public virtual string Description => wrappee.Description;
            public virtual double Price => wrappee.Price;
            public virtual double GetPrice() => wrappee.GetPrice();
        }
        #endregion

        #region conrete decorators
        public class FacebookNotifier : NotifierDecorator
        {
            public FacebookNotifier(INotifier notifier) : base(notifier) { }
            public override void Send(string message)
            {
                base.Send(message);
                Console.WriteLine($"Facebook Send: {message}");
            }
        }
        public class SlackNotifier : NotifierDecorator
        {
            public SlackNotifier(INotifier notifier) : base(notifier) { }
            public override void Send(string message)
            {
                base.Send(message);
                Console.WriteLine($"Slack Send: {message}");
            }
        }

        public class CondensedMilk : ToppingDecorator
        {
            public CondensedMilk(IDrink drink) : base(drink) { }
            public override string Description => $"{base.Description}, Condensed milk";
            public override double Price => 10;
            public override double GetPrice()
            {
                return base.GetPrice() + Price;
            }
        }

        public class Caramel : ToppingDecorator
        {
            public Caramel(IDrink drink) : base(drink) { }
            public override string Description => $"{base.Description}, Caramel";
            public override double Price => 25;
            public override double GetPrice()
            {
                return base.GetPrice() + 25;
            }
        }
        #endregion

        /// <summary>
        /// Problem: When we need to add new virtual method and don't want to initial and manage many class
        /// Solved: Use Decorator to wrap all objects into one object and only 1 execute method it'll automatically execute all wrappee objects
        /// </summary>
        public override void Demo()
        {
            INotifier notifier = new Notifier();
            notifier = new FacebookNotifier(notifier);
            notifier = new SlackNotifier(notifier);
            notifier.Send("Hello World!");

            Console.WriteLine();

            IDrink drink = new Coffee();
            drink = new CondensedMilk(drink);
            drink = new Caramel(drink);
            Console.WriteLine($"Drink detail: {drink.Description}");
            Console.WriteLine($"Drink price: {drink.GetPrice()}");
        }
    }
}
