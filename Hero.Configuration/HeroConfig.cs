﻿using System;
using System.Collections.Generic;
using Hero; //do not remove
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace Hero.Configuration
{
    public static class HeroConfig
    {
        public static IAbilityAuthorizationService AuthorizationService { get; private set; }

        public static void Initialize(IAbilityAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        /// <summary>
        /// Verify a user can perform the given ability. Primarily
        /// intended as syntatic sugar.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static bool Can(string userName, string ability)
        {
            return AuthorizationService.Authorize(userName, ability);
        }

        /// <summary>
        /// Verify a user cannot perform the given ability. Primarily
        /// intended as syntatic sugar.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ability"></param>
        /// <returns></returns>
        public static bool Cannot(string userName, string ability)
        {
            return !(AuthorizationService.Authorize(userName, ability));
        }

        /// <summary>
        /// Assign a set of abilities to a role.
        /// </summary>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="ability">The ability to assign the role if nescessary</param>
        public static void RegisterAbility(IRole role,
                                           Ability ability)
        {
            RegisterAbilities(role, new List<Ability> {ability});
        }

        /// <summary>
        /// Assign a set of abilities to a role.
        /// </summary>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="abilities">The abilities to assign the role if nescessary</param>
        public static void RegisterAbilities(IRole role, IEnumerable<Ability> abilities)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign abilities.
            //
            // You could additionally add the roles and abilities into this file
            // but it was designed to be general enough to support most use cases
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (role == null)
                throw new ArgumentNullException("role");
            if (abilities == null)
                throw new ArgumentNullException("abilities");
            if (role.Abilities == null)
                role.Abilities = new List<IAbility>();

            foreach (Ability ability in abilities)
                role.Abilities.Add(ability);

            AuthorizationService.AddRole(role);
        }

        /// <summary>
        /// Unassign a set of abilities to a role.
        /// </summary>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="ability">The abilities to assign the role if nescessary</param>
        public static void UnregisterAbility(IRole role, Ability ability)
        {
            UnregisterAbilities(role, new List<Ability> {ability});
        }

        /// <summary>
        /// Unassign a set of abilities to a role.
        /// </summary>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="abilities">The abilities to assign the role if nescessary</param>
        public static void UnregisterAbilities(IRole role, IEnumerable<Ability> abilities)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign abilities.
            //
            // You could additionally add the roles and abilities into this file
            // but it was designed to be general enough to support most use cases
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (role == null)
                throw new ArgumentNullException("role");
            if (abilities == null)
                throw new ArgumentNullException("abilities");
            if (role.Abilities == null)
                role.Abilities = new List<IAbility>();

            foreach (Ability ability in abilities)
                role.Abilities.Remove(ability);
            AuthorizationService.UpdateRole(role);
        }

        /// <summary>
        /// Assign a set of roles to a user
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public static void RegisterRole(IUser user, IRole role)
        {
            RegisterRoles(user, new List<IRole> {role});
        }

        /// <summary>
        /// Assign a set of roles to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void RegisterRoles(IUser user, IEnumerable<IRole> roles)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign roles.
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (user == null)
                throw new ArgumentNullException("user");
            if (roles == null)
                throw new ArgumentNullException("roles");
            if (user.Roles == null)
                user.Roles = new List<IRole>();

            foreach (IRole role in roles)
                user.Roles.Add(role);
            AuthorizationService.AddUser(user);
        }

        /// <summary>
        /// Unassign a set of roles to a user
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public static void UnregisterRole(IUser user, IRole role)
        {
            UnregisterRoles(user, new List<IRole> {role});
        }

        /// <summary>
        /// Unassign a set of roles to a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void UnregisterRoles(IUser user, IEnumerable<IRole> roles)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign roles.
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (user == null)
                throw new ArgumentNullException("user");
            if (roles == null)
                throw new ArgumentNullException("roles");
            if (user.Roles == null)
                user.Roles = new List<IRole>();

            foreach (IRole role in roles)
                user.Roles.Remove(role);
            AuthorizationService.RemoveUser(user);
        }
    }
}
