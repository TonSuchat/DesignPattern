using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Mediator : Pattern
    {

        #region Mediator
        // interface
        public interface IMessageMediator
        {
            void SendMessage(string msg, DeviceColleague sender);
        }

        // concrete mediator
        public class MessageMediator : IMessageMediator
        {
            public IList<DeviceColleague> deviceColleagues { get; set; }
            public MessageMediator() => deviceColleagues = new List<DeviceColleague>();
            public void SendMessage(string msg, DeviceColleague sender)
            {
                foreach (var receiver in deviceColleagues)
                {
                    if (receiver != sender) receiver.ReceiveMessage(msg);
                }
            }
        }
        #endregion

        #region Colleague 
        // base colleague
        public abstract class DeviceColleague
        {
            public IMessageMediator messageMediator { get; private set; }
            public DeviceColleague(IMessageMediator messageMediator) => this.messageMediator = messageMediator;
            public void Send(string msg) => messageMediator.SendMessage(msg, this);
            public abstract void ReceiveMessage(string msg);
        }
        // concrete colleagues
        public class MobileColleague : DeviceColleague
        {
            public MobileColleague(IMessageMediator messageMediator) : base(messageMediator) { }
            public override void ReceiveMessage(string msg) => Console.WriteLine($"Mobile: {msg}");
        }

        public class PCColleague : DeviceColleague
        {
            public PCColleague(IMessageMediator messageMediator) : base(messageMediator) { }
            public override void ReceiveMessage(string msg) => Console.WriteLine($"PC: {msg}");
        }

        public class SatelliteColleague : DeviceColleague
        {
            public SatelliteColleague(IMessageMediator messageMediator) : base(messageMediator){ }
            public override void ReceiveMessage(string msg) => Console.WriteLine($"Satellite: {msg}");
        }
        #endregion

        /// <summary>
        /// Problem: When we have to manage the same type of objects with difference execute
        /// Solved: Use Mediator as a middle ware 
        /// </summary>
        public override void Demo()
        {
            var mediator = new MessageMediator();
            var mobile = new MobileColleague(mediator);
            var pc = new PCColleague(mediator);
            var satellite = new SatelliteColleague(mediator);

            mediator.deviceColleagues.Add(mobile);
            mediator.deviceColleagues.Add(pc);
            mediator.deviceColleagues.Add(satellite);

            Console.WriteLine("Mobile send a message");
            mobile.Send("Hello from mobile");

            Console.WriteLine("PC response a message");
            pc.Send("Hello I'm PC");

            Console.WriteLine("Satellite response a message");
            satellite.Send("Hello I'm Satellite");
        }
    }
}
