namespace UnitTests.Services.PriorityQueue
{
    using Kata.Services.PriorityQueue;
    using Xunit;


    public class PriorityQueueTests
    {
        [Fact]
        public void Test_IsEmpty_true()
        {
            var cut = new PriorityQueue<int>();

            var actual = cut.IsEmpty;

            Assert.True(actual);
        }


        [Fact]
        public void Test_IsEmpty_false()
        {
            var cut = new PriorityQueue<int>();
            cut.Enqueue(2, 1);

            var actual = cut.IsEmpty;

            Assert.False(actual);
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
    }
}