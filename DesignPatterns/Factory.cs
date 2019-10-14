using DesignPattern.DesignPatterns;
using System;

namespace ConsoleApp.DesignPatterns
{

    public class Factory : Pattern
    {
        // products
        public interface IButton
        {
            string Clicked();
        }

        public interface ICheckbox
        {
            string Checked();
        }

        public class Button : IButton
        {
            public string OSName { get; set; }

            public string Clicked()
            {
                return $"{OSName} button has been clicked";
            }
        }
        public class Checkbox : ICheckbox
        {
            public string OSName { get; set; }

            public string Checked()
            {
                return $"{OSName} checkbox has been checked";
            }
        }

        // factory
        public interface GUIFactory
        {
            Button createButton();
            Checkbox createCheckbox();
        }

        // window factory (windows os implementation version of GUIFactory)
        public class WinFactory : GUIFactory
        {
            private readonly string osName = "Windows";

            public Button createButton()
            {
                return new Button() { OSName = osName };
            }

            public Checkbox createCheckbox()
            {
                return new Checkbox() { OSName = osName };
            }
        }

        // mac factory (mac os implementation version of GUIFactory)
        public class MacFactory : GUIFactory
        {
            private readonly string osName = "Mac";
            public Button createButton()
            {
                return new Button() { OSName = osName };
            }

            public Checkbox createCheckbox()
            {
                return new Checkbox() { OSName = osName };
            }
        }
        
        /// <summary>
        /// Problem: Have to implement GUI elements(Button, Checkbox) for Windows and Mac OS (etc...)
        /// Solved: Use Factory pattern for solving this problem
        /// </summary>
        public override void Demo()
        {
            Console.WriteLine($"================Factory================{Environment.NewLine}");
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
            Console.WriteLine($"{Environment.NewLine}================Factory================");
        }
    }

}
