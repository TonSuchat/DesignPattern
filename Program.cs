﻿using DesignPattern.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderMenu();
        }

        public static IEnumerable<Type> GetDesignPatterns()
        {
            var baseClassType = typeof(Pattern);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                               .SelectMany(s => s.GetTypes())
                                               .Where(p => baseClassType.IsAssignableFrom(p) && p.FullName != baseClassType.FullName)
                                               .OrderBy(p => p.Name);
            return types;
        }

        public static void RenderMenu()
        {
            // A variable to keep track of the current Item, and a simple counter.
            short curItem = 0, c;

            // The object to read in a key
            ConsoleKeyInfo key;

            // Get list of design patterns Type
            IEnumerable<Type> DesignPatternsType = GetDesignPatterns();

            // Our array of Items for the menu (in order)
            List<string> menuItems = DesignPatternsType?.Select(d => d.Name)?.ToList() ?? new List<string>();

            do
            {
                // Clear the screen.  One could easily change the cursor position,
                // but that won't work out well with tabbing out menu items.
                Console.Clear();
                Console.WriteLine("Choose the design pattern to see the demo output . . .");

                // The loop that goes through all of the menu items.
                for (c = 0; c < menuItems.Count(); c++)
                {
                    // If the current item number is our variable c, tab out this option.
                    if (curItem == c)
                    {
                        Console.Write(">>");
                        Console.WriteLine(menuItems[c]);
                    }
                    // Just write the current option out if the current item is not our variable c.
                    else
                    {
                        Console.WriteLine(menuItems[c]);
                    }
                }

                // Waits until the user presses a key, and puts it into our object key.
                Console.Write("Select your choice with the arrow keys.");
                key = Console.ReadKey(true);

                // Simply put, if the key the user pressed is a "DownArrow", the current item will deacrease.
                // Likewise for "UpArrow", except in the opposite direction.
                // If curItem goes below 0 or above the maximum menu item, it just loops around to the other end.
                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > menuItems.Count() - 1) curItem = 0;
                }
                else if (key.Key.ToString() == "UpArrow")
                {
                    curItem--;
                    if (curItem < 0) curItem = Convert.ToInt16(menuItems.Count() - 1);
                }
                // Loop around until the user presses the enter go.
            } while (key.KeyChar != 13);
            // selected the pattern
            SelectedPattern(DesignPatternsType.FirstOrDefault(d => d.Name == menuItems[curItem]));
        }

        public static void SelectedPattern(Type selectedPattern)
        {
            Pattern pattern = (Pattern)Activator.CreateInstance(selectedPattern);
            Console.Clear();
            pattern.Demo();
            Console.WriteLine($"{Environment.NewLine}Press any key to go back to menu.");
            Console.ReadLine();
            RenderMenu();
        }
    }

}

