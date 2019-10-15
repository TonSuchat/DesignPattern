using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Singleton : Pattern
    {

        public class Database
        {
            // private property to storage the object
            private static Database _database;
            public static Database Instance
            {
                get
                {
                    // initial only the first time if it's not exited
                    if (_database == null) return new Database();
                    return _database;
                }
            }

            public void Execute(string query)
            {
                Console.WriteLine($"Execute query: {query}");
            }
        }

        /// <summary>
        /// Problem: When we don't need the object create many and many times due to lots of performance or any issue.
        /// Solved: We make it can access globally via 'static' keyword and initial it for only the first time if it's not existed
        /// </summary>
        public override void Demo()
        {
            // no need to new this class just use the instance
            var query = "SELECT * FROM PRODUCTS";
            Database.Instance.Execute(query);
        }
    }
}
