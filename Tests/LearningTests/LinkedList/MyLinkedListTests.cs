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
        public void Test_Has_no_Next_Item()
        {
            var cut = new MyLinkedList<int> { 1 };

            var actual = cut.GetItem(0).Next;

            Assert.Null(actual);
        }

        [Fact]
        public void Test_Has_Next_Item()
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            var actual   = cut.GetItem(0).Next;
            var expected = cut.GetItem(1);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(9, false)]
        public void Test_Contains(int value, bool expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            var actual = cut.Contains(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(9, -1)]
        public void Test_IndexOf(int value, int expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            var actual = cut.IndexOf(value);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 3, 3)]
        [InlineData(0, 1, 1)]
        public void Test_Insert(int insertAt, int indexOfValue, int expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            cut.Insert(insertAt, 4);
            var actual = cut.IndexOf(indexOfValue);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 3, 1)]
        [InlineData(0, 1, -1)]
        public void Test_RemoveAt(int removeAt, int indexOfValue, int expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            cut.RemoveAt(removeAt);
            var actual = cut.IndexOf(indexOfValue);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        public void Test_Remove(int remove)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            cut.Remove(remove);
            var actual = cut.IndexOf(remove);

            Assert.Equal(-1, actual);
        }


        [Theory]
        [InlineData(0, 1, 2, 3)]
        [InlineData(1, 0, 1, 2, 3)]
        public void Test_CopyTo(int arrayIndex, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            int[] array = new int[expected.Length];

            // what a bad interface...
            cut.CopyTo(array, arrayIndex);

            Assert.Equal(expected, array);
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
