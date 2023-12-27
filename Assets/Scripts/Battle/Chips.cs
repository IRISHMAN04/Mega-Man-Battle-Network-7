using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utility;
namespace Battle.Chips
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class Chip
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; protected set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public DamageType Type { get; protected set; } = DamageType.Normal;

        /// <summary>
        /// 
        /// </summary>
        public int Damage { get; protected set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        public int Healing { get; protected set; } = -1;

        /// <summary>
        /// 
        /// </summary>
        public char Code { get; protected set; } = 'A';

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Chip(char code)
        {
            Code = code;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Cannon : Chip
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Cannon(char code) : base(code)
        {
            Name = "Cannon";
            Damage = 40;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Shockwave : Chip
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Shockwave(char code) : base(code)
        {
            Name = "Shockwave";
            Damage = 40;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Recover : Chip
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Recover(char code) : base(code)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Recover10 : Recover
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Recover10(char code) : base(code)
        {
            Name = "Recover10";
            Healing = 10;
        }
    }
}