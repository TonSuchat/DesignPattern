using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class State : Pattern
    {
        #region Constants
        private readonly static string INFO = "INFO";
        private readonly static string DRINK_MACHINE = "DRINK-MACHINE";
        private readonly static string INSERT_MORE_COINS = $"{DRINK_MACHINE}: Insert more coins / Minimum price is 7 coins";
        #endregion

        #region State
        public interface IState
        {
            void InsertCoin(int amount);
            void Buy();
        }

        // concrete states
        
        public class NotAvailable : IState
        {
            private DrinkMachine context;
            public NotAvailable(DrinkMachine context) => this.context = context;
            public void Buy() => Console.WriteLine(INSERT_MORE_COINS);
            public void InsertCoin(int amount)
            {
                if (amount >= 7) context.ChangeState(new Available(context));
            }
        }
        public class Available : IState
        {
            private DrinkMachine context;
            public Available(DrinkMachine context) => this.context = context;
            public void Buy()
            {
                if (context.coin < 7) 
                {
                    Console.WriteLine(INSERT_MORE_COINS);
                    context.ChangeState(new NotAvailable(context));
                }
                else
                {
                    Console.WriteLine($"{DRINK_MACHINE}: Buy 1 water");
                    context.coin -= 7;
                    if (context.coin < 7) context.ChangeState(new NotAvailable(context));
                }
            }

            public void InsertCoin(int amount)
            {
                if (amount >= 7) context.ChangeState(new Available(context));
            }
        }
        #endregion

        #region Context
        public class DrinkMachine
        {
            private IState state { get; set; }
            public int coin { get; set; }
            public DrinkMachine() => state = new NotAvailable(this);
            public void ChangeState(IState state) => this.state = state;
            public void InsertCoin(int amount)
            {
                coin += amount;
                state.InsertCoin(coin);
            }
            public void Buy() => state.Buy();
        }
        #endregion

        /// <summary>
        /// Problem: When we have to manage some object that has multiple states
        /// Solved: Use State pattern for split the actual concrete state to subclass and implement the same interface
        /// </summary>
        public override void Demo()
        {
            var drinkMachine = new DrinkMachine();
            Console.WriteLine($"{INFO}: Insert 5 coins and try to buy some drink");
            drinkMachine.InsertCoin(5);
            drinkMachine.Buy();
            Console.WriteLine($"{INFO}: Insert 2 more coins and buy again");
            drinkMachine.InsertCoin(2);
            drinkMachine.Buy();
        }
    }
}
