using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Observer : Pattern
    {
        #region Publisher 
        public class Publisher
        {
            private IList<ISubscriber> subscribers;
            public Publisher() => subscribers = new List<ISubscriber>();
            public void Subscribe(ISubscriber subscriber) => subscribers.Add(subscriber);
            public void UnSubscribe(ISubscriber subscriber) => subscribers.Remove(subscriber);
            public void NotifySubScribers(string msg) => subscribers.ToList().ForEach(s => s.Update(msg));

        }
        #endregion

        #region Subscriber
        public interface ISubscriber
        {
            void Update(string msg);
        }

        public class Student : ISubscriber
        {
            public void Update(string msg) => Console.WriteLine($"Student got message: {msg}");
        }

        public class Teacher : ISubscriber
        {
            public void Update(string msg) => Console.WriteLine($"Teacher got message: {msg}");
        }
        #endregion

        /// <summary>
        /// Problem: When some object needs to get notify from some resouces
        /// Solved: Use Observer pattern to notify all subscribes
        /// </summary>
        public override void Demo()
        {
            var publisher = new Publisher();
            var studentA = new Student();
            var teacherA = new Teacher();

            // studentA and teacherA subscribe
            publisher.Subscribe(studentA);
            publisher.Subscribe(teacherA);

            Console.WriteLine("School send a message to subscribers");
            publisher.NotifySubScribers("This is first message");

            // studentA unsubscribe
            publisher.UnSubscribe(studentA);
            Console.WriteLine("School send a message again");
            publisher.NotifySubScribers("This is second message");
        }
    }
}
