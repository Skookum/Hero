﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hero.Interfaces;
using Hero.Services;
using Hero.Tests.Models;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityAuthorizationServiceTests
    {
        private IRole _role1;
        private IRole _role2;
        private IUser _user1;
        private IUser _user2;
        private Ability _ability1;
        private Ability _ability2;
        private Ability _ability3;
        private Ability _ability4;
        private AbilityConsumer _abilityConsumer;
        private RoleConsumer _roleConsumer;
        private AbilityAuthorizationService _authorizationService;

        [SetUp]
        public void Initialize()
        {
            _role1 = new Role(1, "Role1");
            _role2 = new Role(2, "Role2");
            _user1 = new User("User1", "User1");
            _user2 = new User("User2", "User2");
            _ability1 = new Ability("Ability1");
            _ability2 = new Ability("Ability2");
            _ability3 = new Ability("Ability3");
            _ability4 = new Ability("Ability4", new List<Ability>{_ability1, _ability2, _ability3});
            _abilityConsumer = new AbilityConsumer();
            _roleConsumer = new RoleConsumer();
            _authorizationService = new AbilityAuthorizationService();
        }

        [Test]
        public void TestAuthorizionWithEmptyRoleThrowException()
        {
            Assert.Throws<NotImplementedException>(() => _authorizationService.Authorize(""));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterAbilityWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterRoleWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleAbilitiesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleRolesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role2));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleAbilitiesAndRolesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability2));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role2, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role2, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleRolesAndUserssWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role2));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user2, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user2, _role2));
        }

        [Test]
        public void TestAuthorizationServiceUnregisterAbilityDoesNothingWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.UnregisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceUnregisterRoleDoesNothingWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.UnregisterRole(_user1, _role1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleOneAbility()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserOneRole()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserOneAbility()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.Authorize(_user1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleMultipleAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserMultipleRoles()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);

            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleRolesMultipleAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleUsersMultipleRoles()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterRole(_user2, _role1);
            _authorizationService.RegisterRole(_user2, _role2);

            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleOneAbilityAfterUnregister()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.False(_authorizationService.Authorize(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserOneRoleAfterUnregister()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.False(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleRolesMultipleAbilitiesAfterUnregister()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.False(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleUsersMultipleRolesAfterUnregister()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterRole(_user2, _role1);
            _authorizationService.RegisterRole(_user2, _role2);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));


            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.False(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));
        }

        [Test]
        public void TestAuthorizationServiceAbilityEvents()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.AreEqual(1, _abilityConsumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _abilityConsumer.Param[0]);
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.AreEqual(0, _abilityConsumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _abilityConsumer.Param[0]);
        }

        [Test]
        public void TestAuthorizationServiceRoleEvents()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.AreEqual(1, _roleConsumer.Counter);
            Assert.AreEqual(new UserRole(_user1, _role1), _roleConsumer.Param[0]);
            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.AreEqual(0, _roleConsumer.Counter);
            Assert.AreEqual(new UserRole(_user1, _role1), _roleConsumer.Param[0]);
        }

        [Test]
        public void TestGetAbilitiesForRoleThatExists()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role1);
            Assert.AreEqual(2, abilitiesForRole.Count());
            Assert.True(new List<Ability> {_ability1, _ability2}.SequenceEqual(abilitiesForRole));
        }

        [Test]
        public void TestGetAbilitiesForRoleThatDoesNotExist()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role2);
            Assert.AreEqual(0, abilitiesForRole.Count());
        }

        [Test]
        public void TestGetRolesForUserThatDoesNotExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            IEnumerable<IRole> rolesForUser = _authorizationService.GetRolesForUser(_user2);
            Assert.AreEqual(0, rolesForUser.Count());
        }

        [Test]
        public void TestRegisterAbilityWithChildrenAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability4);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role1);
            Assert.AreEqual(4, abilitiesForRole.Count());
            Assert.True(new List<Ability> {_ability4, _ability1, _ability2, _ability3}.SequenceEqual(abilitiesForRole));
        }

        [Test]
        public void TestUnregisterAbilityWithChildrenAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability4);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role1);
            Assert.AreEqual(4, abilitiesForRole.Count());
            Assert.True(new List<Ability> {_ability4, _ability1, _ability2, _ability3}.SequenceEqual(abilitiesForRole));
            _authorizationService.UnregisterAbility(_role1, _ability4);
            Assert.AreEqual(0, abilitiesForRole.Count());
        }
    }
}
