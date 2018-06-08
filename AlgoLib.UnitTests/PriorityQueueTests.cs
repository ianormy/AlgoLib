using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoLib.UnitTests
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void MinPriorityQueue_Dequeue_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Min);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 4);
            Assert.IsTrue(queue.Count == 7);
            val = queue.Dequeue();
            Assert.IsTrue(val == 7);
            Assert.IsTrue(queue.Count == 6);
        }
        [TestMethod]
        public void MinPriorityQueue_EnqueueMin_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Min);
            queue.Enqueue(2);
            Assert.IsTrue(queue.Count == 9);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 2);
            Assert.IsTrue(queue.Count == 8);
            val = queue.Peek();
            Assert.IsTrue(val == 4);
            Assert.IsTrue(queue.Count == 8);
        }
        [TestMethod]
        public void MinPriorityQueue_EnqueueMax_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Min);
            queue.Enqueue(2322);
            Assert.IsTrue(queue.Count == 9);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 4);
            Assert.IsTrue(queue.Count == 8);
            val = queue.Peek();
            Assert.IsTrue(val == 7);
            Assert.IsTrue(queue.Count == 8);
        }

        [TestMethod]
        public void MinPriorityQueue_DateTime_Success()
        {
            var rnd = new Random();
            var queue = new PriorityQueue<DateTime>(PriorityOrder.Min);
            for (int i = 0; i < 100; i++)
            {
                var ms = rnd.Next(2, 20);
                Thread.Sleep(ms);
                var offset = rnd.Next(5, 10) - 10;
                var dt = DateTime.Now.AddSeconds(offset);

                var peek = (queue.Count > 0) ? queue.Peek() : DateTime.MinValue;

                if (dt > peek)
                    queue.Enqueue(dt);

                if (queue.Count > 20)
                {
                    var dq = queue.Dequeue();
                    Assert.IsTrue(dq <= dt);
                }
            }
        }
        [TestMethod]
        public void MinPriorityQueue_Peek_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 101, 96, 45, 88, 25, 18, 4, 7
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Min);
            var val = queue.Peek();
            Assert.IsTrue(val == 4);
        }

        [TestMethod]
        public void MaxPriorityQueue_Dequeue_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Max);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 101);
            Assert.IsTrue(queue.Count == 7);
            val = queue.Dequeue();
            Assert.IsTrue(val == 96);
            Assert.IsTrue(queue.Count == 6);
        }
        [TestMethod]
        public void MaxPriorityQueue_MaxEnqueue_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Max);
            queue.Enqueue(232);
            Assert.IsTrue(queue.Count == 9);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 232);
            Assert.IsTrue(queue.Count == 8);
            val = queue.Peek();
            Assert.IsTrue(val == 101);
            Assert.IsTrue(queue.Count == 8);
        }
        [TestMethod]
        public void MaxPriorityQueue_MinEnqueue_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 4, 7, 18, 25, 45, 88, 96, 101
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Max);
            queue.Enqueue(2);
            Assert.IsTrue(queue.Count == 9);
            var val = queue.Dequeue();
            Assert.IsTrue(val == 101);
            Assert.IsTrue(queue.Count == 8);
            val = queue.Peek();
            Assert.IsTrue(val == 96);
            Assert.IsTrue(queue.Count == 8);
        }
        [TestMethod]
        public void MaxPriorityQueue_Peek_Success()
        {
            // from 45, 25, 4, 88, 96, 18, 101, 7
            //   to 101, 96, 45, 88, 25, 18, 4, 7
            IList<int> data = new List<int> { 45, 25, 4, 88, 96, 18, 101, 7 };
            var queue = new PriorityQueue<int>(data, PriorityOrder.Max);
            var val = queue.Peek();
            Assert.IsTrue(val == 101);
        }

    }
}
