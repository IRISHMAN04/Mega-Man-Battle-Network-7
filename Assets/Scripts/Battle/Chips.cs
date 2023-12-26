using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utility;
namespace Battle.Chips
{
    public abstract class Chip
    {
        public string Name { get; protected set; } = null;
        public DamageType Type { get; protected set; } = DamageType.Normal;
        public int Damage { get; protected set; } = -1;
        public int Healing { get; protected set; } = -1;
        public char Code { get; protected set; } = 'A';

        public Chip(char code)
        {
            Code = code;
        }
    }

    public class Cannon : Chip
    {
        public Cannon(char code) : base(code)
        {
            Name = "Cannon";
            Damage = 40;
        }
    }

    public class Shockwave : Chip
    {
        public Shockwave(char code) : base(code)
        {
            Name = "Shockwave";
            Damage = 40;
        }
    }

    public class Recover : Chip
    {
        public Recover(char code) : base(code)
        {

        }
    }

    public class Recover10 : Recover
    {
        public Recover10(char code) : base(code)
        {
            Name = "Recover10";
            Healing = 10;
        }
    }
}