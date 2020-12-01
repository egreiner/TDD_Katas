namespace UnitTests.Services.LinkedList
{
    using System;
    using System.Linq;
    using Kata.Services.LinkedList;
    using Xunit;

    public class MyLinkedListTests
    {
        [Fact]
        public void Test_Empty_LinkedList()
        {
            var cut = new MyLinkedList<int>();

            Assert.Empty(cut);
        }

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
        public void Test_Get_Count()
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            var actual = cut.Count;

            Assert.Equal(2, actual);
        }

        [Fact]
        public void Test_Clear_list()
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };
            cut.Clear();

            var actual = cut.Count;

            Assert.Equal(0, actual);
            Assert.Empty(cut);
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

        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void Test_Set_with_indexer_throw_exception_on_not_existing_index(int index)
        {
            var cut = new MyLinkedList<int> { 1, 2 };

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() => cut[index] = 3);

            Assert.Equal(nameof(index), actual.ParamName);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(9, -1)]
        public void Test_IndexOf(int value, int expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 1, 2, 3 };

            var actual = cut.IndexOf(value);

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


        [Fact]
        public void Test_Insert_on_empty_linkedList()
        {
            var cut = new MyLinkedList<int>();

            cut.Insert(0, 4);
            var actual = cut.IndexOf(4);

            Assert.Equal(0, actual);
            Assert.Single(cut);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(4)]
        [InlineData(400)]
        public void Test_Insert_throw_exception(int index)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.Insert(index, 4));

            Assert.Equal(nameof(index), actual.ParamName);
        }

        [Theory]
        [InlineData(0, 4, 1, 2, 3)]
        [InlineData(1, 1, 4, 2, 3)]
        [InlineData(2, 1, 2, 4, 3)]
        public void Test_Insert(int insertAt, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            cut.Insert(insertAt, 4);

            var actual = cut.Items().ToList();

            Assert.Equal(expected.ToList(), actual);
            Assert.Equal(4, cut.Count);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(5)]
        [InlineData(500)]
        public void Test_RemoveAt_throw_exception(int index)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 4 };

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.RemoveAt(index));

            Assert.Equal(nameof(index), actual.ParamName);
        }

        [Theory]
        [InlineData(0, 2, 3, 4)]
        [InlineData(1, 1, 3, 4)]
        [InlineData(2, 1, 2, 4)]
        [InlineData(3, 1, 2, 3)]
        public void Test_RemoveAt(int removeAt, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 4 };

            cut.RemoveAt(removeAt);

            var actual = cut.Items().ToList();

            Assert.Equal(expected.ToList(), actual);
            Assert.Equal(3, cut.Count);
        }

        [Theory]
        [InlineData(0, 2, 3, 4, 5)]
        [InlineData(2, 1, 2, 4, 5)]
        [InlineData(3, 1, 2, 3, 5)]
        public void Test_RemoveLast_add_another(int removeAt, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 4 };

            cut.RemoveAt(removeAt);
            cut.Add(5);

            var actual = cut.Items().ToList();

            Assert.Equal(expected.ToList(), actual);
            Assert.Equal(4, cut.Count);
        }

        [Theory]
        [InlineData(1, 2, 3, 4)]
        [InlineData(2, 1, 3, 4)]
        [InlineData(3, 1, 2, 4)]
        [InlineData(4, 1, 2, 3)]
        public void Test_Remove(int remove, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 4 };

            cut.Remove(remove);

            var actual = cut.Items().ToList();

            Assert.Equal(expected.ToList(), actual);
            Assert.Equal(3, cut.Count);
        }

        [Theory]
        [InlineData(-1, 1, 2, 3, 4)]
        [InlineData(0, 1, 2, 3, 4)]
        [InlineData(9, 1, 2, 3, 4)]
        public void Test_Remove_not_existing_items(int remove, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3, 4 };

            cut.Remove(remove);

            var actual = cut.Items().ToList();

            Assert.Equal(expected.ToList(), actual);
            Assert.Equal(4, cut.Count);
        }

        [Theory]
        [InlineData(0, 1, 2, 3)]
        [InlineData(1, 0, 1, 2, 3)]
        [InlineData(4, 0, 0, 0, 0, 1, 2, 3)]
        public void Test_CopyTo(int arrayIndex, params int[] expected)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            int[] array = new int[expected.Length];

            cut.CopyTo(array, arrayIndex);

            Assert.Equal(expected, array);
            Assert.Equal(3, cut.Count);
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 3)]
        public void Test_CopyTo_array_to_short_throws_exception(int arrayIndex, int arrayLength)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            int[] array = new int[arrayLength];

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.CopyTo(array, arrayIndex));

            Assert.Equal(nameof(array), actual.ParamName);
        }

        [Theory]
        [InlineData(-1, 200)]
        public void Test_CopyTo_wrong_index_throws_exception(int arrayIndex, int arrayLength)
        {
            var cut = new MyLinkedList<int> { 1, 2, 3 };

            int[] array = new int[arrayLength];

            var actual = Assert.Throws<ArgumentOutOfRangeException>(() =>
                cut.CopyTo(array, arrayIndex));

            Assert.Equal(nameof(arrayIndex), actual.ParamName);
        }
    }
}
