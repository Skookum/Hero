﻿using Hero.Interfaces;
using System.Collections.Generic;

namespace Hero.Services.Interfaces
{
    public interface IAbilityAuthorizationService
    {
        bool Authorize(IRole role, Ability ability);
        bool Authorize(IUser user, Ability ability);
        void RegisterAbility(IRole role, Ability ability);
        void UnregisterAbility(IRole role, Ability ability);
        void RegisterRole(IUser user, IRole role);
        void UnregisterRole(IUser user, IRole role);
        IEnumerable<IRole> GetRolesForUser(IUser user);
        IEnumerable<IRole> GetRolesForUser(string userName);
        IEnumerable<Ability> GetAbilitiesForRole(IRole role);
        IEnumerable<Ability> GetAbilitiesForRole(string roleName);
        IEnumerable<Ability> GetAbilitiesForUser(IUser user);
        IEnumerable<Ability> GetAbilitiesForUser(string userName);
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
    }
}