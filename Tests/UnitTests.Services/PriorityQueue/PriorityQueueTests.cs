namespace UnitTests.Services.PriorityQueue
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Kata.Services.PriorityQueue;
    using Xunit;


    public class PriorityQueueTests
    {
        [Fact]
        public void Test_HasItems_false()
        {
            var cut = new PriorityQueue<int>();

            var actual = cut.HasItems;

            Assert.False(actual);
        }

        [Fact]
        public void Test_HasItems_true()
        {
            var cut = new PriorityQueue<int>();
            cut.Enqueue(2, 1);

            var actual = cut.HasItems;

            Assert.True(actual);
        }

        [Fact]
        public void Test_Count()
        {
            var cut = new PriorityQueue<int>();

            cut.Enqueue(2, 1);
            cut.Enqueue(2, 1);

            var actual = cut.Count;

            Assert.Equal(2, actual);
        }

        [Fact]
        public void Test_TryDequeue_from_empty_queue()
        {
            var cut = new PriorityQueue<int>();

            var actual = cut.TryDequeue(out var item);

            Assert.False(actual);
            Assert.Equal(0, item);
        }

        [Fact]
        public void Test_TryDequeue_first_added()
        {
            var cut = new PriorityQueue<int>();

            cut.Enqueue(2, 1);
            cut.Enqueue(3, 1);

            var actual = cut.TryDequeue(out var item);

            Assert.True(actual);
            Assert.Equal(2, item);
        }

        [Fact]
        public void Test_TryDequeue_second_added()
        {
            var cut = new PriorityQueue<int>();

            cut.Enqueue(2, 2);
            cut.Enqueue(3, 1);

            var actual = cut.TryDequeue(out var item);

            Assert.True(actual);
            Assert.Equal(3, item);
        }

        [Fact]
        public void Test_Concurrency()
        {
            //this test isn't a typically UnitTest...
            var cut = new PriorityQueue<int>();
            var items = Enumerable.Range(0, 100).ToList();

            // each item should be added 'at the same time' from multiple tasks
            // but with different priorities
            Parallel.ForEach(items, item => 
                cut.Enqueue(item, item));

            Assert.Equal(100, cut.Count);

            var list = new List<int>();
            while (cut.HasItems)
            {
                cut.TryDequeue(out var dequeuedItem);
                list.Add(dequeuedItem);
            }

            Assert.Equal(100, list.Count);

            // the items get dequeued in there correct priority
            Assert.All(list, i =>
                Assert.Equal(i, list[i])
                );
        }
    }
}