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
        #endregion

        #region concrete component
        public class Notifier : INotifier
        {
            public void Send(string message) => Console.WriteLine($"General Send: {message}");
        }
        #endregion

        #region base decorator
        public class NotifierDecorator : INotifier
        {
            private readonly INotifier wrappee;
            public NotifierDecorator(INotifier notifier) => wrappee = notifier;
            public virtual void Send(string message) => wrappee.Send(message);
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
        }
    }
}
