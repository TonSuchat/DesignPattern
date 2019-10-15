using System;
using System.Collections.Generic;

namespace DesignPattern.DesignPatterns
{
    public class Iterator : Pattern
    {
        
        #region Iterator
        // interface
        public interface IIterator
        {
            bool HasMore();
            object GetNext();
        }

        // concrete iterator
        public class QueueIterator : IIterator
        {
            private int currentIndex { get; set; }
            private List<object> data;
            public QueueIterator(List<object> data) => this.data = data;
            public object GetNext() => data[currentIndex++];
            public bool HasMore() => currentIndex < data.Count;
        }

        public class StackIterator : IIterator
        {
            private int currentIndex { get; set; }
            private List<object> data;
            public StackIterator(List<object> data)
            {
                this.data = data;
                currentIndex = data.Count - 1;
            }
            public object GetNext() => data[currentIndex--];
            public bool HasMore() => currentIndex >= 0;
        }
        #endregion

        #region Collection
        // interface
        public interface ICollection
        {
            IIterator CreateIterator();
        }

        public class Queue : ICollection
        {
            private List<object> data = new List<object>();
            public IIterator CreateIterator() => new QueueIterator(data);
            public void Enqueue(object data) => this.data.Add(data);
        }

        public class Stack : ICollection
        {
            private List<object> data = new List<object>();
            public IIterator CreateIterator() => new StackIterator(data);
            public void Push(object data) => this.data.Add(data);
        }
        #endregion

        /// <summary>
        /// Problem: When we need to working with iterator object in many features
        /// Solved: Use Iterator pattern for handler with iteration works (in C# we have a List, Queue, Stack, Etc..)
        /// </summary>
        public override void Demo()
        {
            var queue = new Queue();
            for (int i = 1; i <= 5; i++) queue.Enqueue(i);
            Console.WriteLine("Queue");
            var iterator = queue.CreateIterator();
            ShowIteratorData(iterator);

            Console.WriteLine("====================");

            var stack = new Stack();
            for (int i = 1; i <= 5; i++) stack.Push(i);
            Console.WriteLine("Stack");
            iterator = stack.CreateIterator();
            ShowIteratorData(iterator);
        }

        private void ShowIteratorData(IIterator iterator)
        {
            while (iterator.HasMore()) Console.WriteLine(iterator.GetNext());
        }

    }
}
