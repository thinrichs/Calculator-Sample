﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Core
{
    public static class TestMethodExtensions
    {
        public static void ShouldBeTrue(this bool value)
        {
            Assert.IsTrue(value);
        }

        public static void ShouldBeFalse(this bool value)
        {
            Assert.IsFalse(value);
        }

        public static void ShouldNotEqual(this object extendedEntity, object value)
        {
            Assert.AreNotEqual(value, extendedEntity);
        }

        public static void ShouldEqual(this object extendedEntity, object value)
        {
            Assert.AreEqual(value, extendedEntity);
        }

        public static void ShouldNotBeNull(this object value)
        {
            Assert.IsNotNull(value);
        }

        public static void ShouldBeNull(this object value)
        {
            Assert.IsNull(value);
        }     
      
        public static void ShouldBeLessThan(this double extendedEntity, double value)
        {
            Assert.IsTrue(extendedEntity < value);
        }
      
        public static void ShouldBeGreaterThan(this double extendedEntity, double value)
        {
            Assert.IsTrue(extendedEntity > value);
        }
       
        public static void ShouldBeGreater(this int extendedEntity, int value)
        {
            Assert.IsTrue(extendedEntity > value);
        }
        
        public static void ShouldBeLess(this int extendedEntity, int value)
        {
            Assert.IsTrue(extendedEntity < value);
        }

        public static void ShouldUnorderedEqual<T>(this ICollection<T> extendedCollection, ICollection<T> value)
        {
            Assert.IsTrue(ICollectionComparer.UnorderedEqual<T>(extendedCollection, value));
        }
        public static void ShouldUnorderedNotEqual<T>(this ICollection<T> extendedCollection, ICollection<T> value)
        {
            Assert.IsFalse(ICollectionComparer.UnorderedEqual<T>(extendedCollection, value));
        }
        /// <summary>
        /// Verifies that two objects are the same instance.
        /// </summary>
        /// <param name="actual">The actual object.</param>
        /// <param name="expected">The expected object.</param>
        public static void ShouldBeTheSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Verifies that two objects are not the same instance.
        /// </summary>
        /// <param name="actual">The actual object.</param>
        /// <param name="notExpected">The unexpected object.</param>
        public static void ShouldNotBeTheSameAs(this object actual, object notExpected)
        {
            Assert.AreNotSame(notExpected, actual);
        }

        /// <summary>
        /// Verifies that an object is of a specific type.
        /// </summary>
        /// <param name="actual">The actual object.</param>
        /// <param name="expected">The expected type.</param>
        public static void Is(this object actual, Type expected)
        {
            Assert.IsInstanceOfType(actual, expected);
        }

        /// <summary>
        /// Verifies that an object is not a specific type.
        /// </summary>
        /// <param name="actual">The actual object.</param>
        /// <param name="expected">The unexpected typ.</param>
        public static void IsNot(this object actual, Type notExpected)
        {
            Assert.IsNotInstanceOfType(actual, notExpected);
        }
        public static void ShouldContain<T>(this IEnumerable<T> extendedCollection, T value)
        {
            Assert.IsTrue(extendedCollection.Contains(value));
        }
    }
}
