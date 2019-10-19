using DataStructures.BiDictionary;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class BiDictionaryTest
    {
        [Test]
        public void BiDictionaryTest_add_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            Assert.AreEqual(new Vector2Int(4, 4), biDictionary.KeyMap[new Vector2Int(5, 5)]);
            Assert.AreEqual(new Vector2Int(5, 5), biDictionary.ValueMap[new Vector2Int(4, 4)]);

            Assert.AreEqual(new Vector2Int(3, 3), biDictionary.KeyMap[new Vector2Int(6, 6)]);
            Assert.AreEqual(new Vector2Int(6, 6), biDictionary.ValueMap[new Vector2Int(3, 3)]);

            Assert.AreEqual(new Vector2Int(2, 2), biDictionary.KeyMap[new Vector2Int(7, 7)]);
            Assert.AreEqual(new Vector2Int(7, 7), biDictionary.ValueMap[new Vector2Int(2, 2)]);
        }

        [Test]
        public void BiDictionaryTest_count_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            Assert.AreEqual(3, biDictionary.Count());
        }

        [Test]
        public void BiDictionaryTest_clear_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            biDictionary.Clear();

            Assert.AreEqual(0, biDictionary.Count());
        }

        [Test]
        public void BiDictionaryTest_containsKey_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            Assert.IsTrue(biDictionary.ContainsKey(new Vector2Int(5, 5)));
            Assert.IsFalse(biDictionary.ContainsKey(new Vector2Int(4, 4)));
        }

        [Test]
        public void BiDictionaryTest_containsValue_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            Assert.IsTrue(biDictionary.ContainsValue(new Vector2Int(3, 3)));
            Assert.IsFalse(biDictionary.ContainsValue(new Vector2Int(6, 6)));
        }

        [Test]
        public void BiDictionaryTest_removeKey_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            biDictionary.RemoveKey(new Vector2Int(5, 5));

            Assert.IsFalse(biDictionary.ContainsKey(new Vector2Int(5, 5)));
            Assert.IsFalse(biDictionary.ContainsValue(new Vector2Int(4, 4)));
        }

        [Test]
        public void BiDictionaryTest_removeValue_simple()
        {
            var biDictionary = new BiDictionary<Vector2Int, Vector2Int>
            {
                { new Vector2Int(5, 5), new Vector2Int(4, 4) },
                { new Vector2Int(6, 6), new Vector2Int(3, 3) },
                { new Vector2Int(7, 7), new Vector2Int(2, 2) }
            };

            biDictionary.RemoveValue(new Vector2Int(4, 4));

            Assert.IsFalse(biDictionary.ContainsKey(new Vector2Int(5, 5)));
            Assert.IsFalse(biDictionary.ContainsValue(new Vector2Int(4, 4)));
        }
    }
}
