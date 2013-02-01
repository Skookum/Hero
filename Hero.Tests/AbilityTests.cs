﻿using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityTests
    {
        [SetUp]
        public void Initialize()
        {
            
        }

        [Test]
        public void TestAbilityHasNameProperty()
        {
            Ability ability = new Ability("ability");
            Assert.AreEqual("ability", ability.Name);
        }
    }
}
