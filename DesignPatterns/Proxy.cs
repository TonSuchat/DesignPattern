using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Proxy : Pattern
    {

        #region Service Interface
        public interface IHardiskService
        {
            double GetAvailableStorage(string partition);
        }
        #endregion

        #region Service
        public class HardiskService : IHardiskService
        {
            public double GetAvailableStorage(string partition)
            {
                switch (partition.Trim().ToLower())
                {
                    case "c":
                        return 100;
                    case "d":
                        return 150;
                    case "e":
                        return 200;
                    default:
                        return 0;
                }
            }
        }
        #endregion

        #region Proxy
        public class ProxyHardiskService : IHardiskService
        {
            private readonly HardiskService hardiskService;
            public ProxyHardiskService() => hardiskService = new HardiskService();

            public double GetAvailableStorage(string partition)
            {
                // do something before the real operation
                Console.WriteLine($"Log: Get available storage at partition: {partition}");

                return hardiskService.GetAvailableStorage(partition);
            }
        }
        #endregion

        /// <summary>
        /// Problem: When we need to do something before the actual operation or do some lazy initializer
        /// Solved: Use Proxy for add the middle ware service and let's the client use middleware instead of real service
        /// </summary>
        public override void Demo()
        {
            IHardiskService hardiskService = new ProxyHardiskService();
            var freeSpace = hardiskService.GetAvailableStorage("d");
            Console.WriteLine($"Free space: {freeSpace}");
        }
    }
}
