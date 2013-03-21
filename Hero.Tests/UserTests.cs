﻿using System.Collections.Generic;
using Hero.Interfaces;
using NUnit.Framework;

namespace Hero.Tests
{
    public class UserTests
    {
        private User _user1;
        private User _user2;

        [SetUp]
        public void Initialize()
        {
            _user1 = new User(1, "User1");
            _user2 = new User(2, "User2");
        }

        [Test]
        public void TestUsersAreEqual()
        {
            User userOne = new User(1, "User1");
            Assert.AreEqual(userOne, _user1);
        }

        [Test]
        public void TestUsersEqualityOperator()
        {
            User userOne = new User(1, "User1");
            Assert.True(userOne == _user1);
        }

        [Test]
        public void TestUsersAreNotEqual()
        {
            Assert.AreNotEqual(_user2, _user1);
        }

        [Test]
        public void TestUsersNotEqualOperator()
        {
            Assert.True(_user2 != _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrue()
        {
            Assert.AreEqual(_user1, _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_user1.Equals(_user1));
        }

        [Test]
        public void TestUserNullReferenceReturnsFalse()
        {
            Assert.False(_user1.Equals(null));
        }

        [Test]
        public void TestUserDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_user1.Equals((object)_user1));
        }

        [Test]
        public void TestUserNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_user1.Equals((object)null));
        }

        [Test]
        public void TestUserDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_user1.Equals((object)""));
        }

        [Test]
        public void TestUserHashCodeEquals()
        {
            Assert.AreEqual(_user1.GetHashCode(), _user1.GetHashCode());
        }

        [Test]
        public void TestUserHashCodeNotEquals()
        {
            Assert.AreNotEqual(_user1.GetHashCode(), _user2.GetHashCode());
        }
    }
}

