using System;
using static ConsoleApp.DesignPatterns.AbstractFactory;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoAbstractFactory();
            Console.ReadLine();
        }

        #region AbstractFactory
        /// <summary>
        /// There're button, checkbox for 2 OS(Windows, Mac) and it has difference implementations, So use AbstractFactory design pattern for solving and demo this problem.
        /// </summary>
        public static void DemoAbstractFactory()
        {
            Console.WriteLine($"================AbstractFactory================{Environment.NewLine}");
            var winFactory = new WinFactory();
            var macFactory = new MacFactory();

            var winButton = winFactory.createButton();
            var macButton = macFactory.createButton();

            Console.WriteLine(winButton.Clicked());
            Console.WriteLine(macButton.Clicked());

            var winCheckbox = winFactory.createCheckbox();
            var macCheckbox = macFactory.createCheckbox();

            Console.WriteLine(winCheckbox.Checked());
            Console.WriteLine(macCheckbox.Checked());
            Console.WriteLine($"{Environment.NewLine}================AbstractFactory================");
        }
        #endregion

    }

}

