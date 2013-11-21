﻿using System;
using System.Collections.Generic;

namespace Hero
{
    public class Role : IEquatable<Role>
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Ability> Abilities { get; set; }

        public Role() { } 

        public Role(string name)
        {
            Name = name;
            Id = name;
            Abilities = new List<Ability>();
        }

        public Role(string name, string id)
        {
            Name = name;
            Id = id;
            Abilities = new List<Ability>();
        }

        public virtual bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.ToLower() == other.Id.ToLower();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Role) obj);
        }

        public override int GetHashCode()
        {
            return Id.ToLower().GetHashCode();
        }

        public static bool operator ==(Role left, Role right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Role left, Role right)
        {
            return !Equals(left, right);
        }
    }
}
