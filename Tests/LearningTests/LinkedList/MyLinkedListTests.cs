namespace LearningTests.LinkedList
{
    using System;
    using Xunit;

    public class MyLinkedListTests
    {
        [Fact]
        public void Test_Add_Element()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var cut = new MyLinkedList<int>();
            cut.Add(1);

            Assert.Single(cut);
        }

        [Fact]
        public void Test_Collection_initializer()
        {
            var cut = new MyLinkedList<int> { 1 };

            Assert.Single(cut);
        }


        [Fact]
        public void Test_Clear_list()
        {
            var cut = new MyLinkedList<int> { 1 };
            cut.Clear();
            
            Assert.Empty(cut);
        }

        [Fact]
        public void Test_Get_Count()
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            var actual = cut.Count;

            Assert.Equal(2, actual);
        }

        [Fact]
        public void Test_Get_with_indexer()
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            var actual = cut[0];

            Assert.Equal(1, actual);
        }

        [Fact]
        public void Test_Set_with_indexer()
        {
            var cut = new MyLinkedList<int> { 1, 2 };
            cut[0] = 3;

            var actual = cut[0];

            Assert.Equal(3, actual);
        }

        [Fact]
        public void Test_Set_with_indexer_throw_exception_on_not_existing_index()
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() => cut[3] = 3);
        }
    }
}
