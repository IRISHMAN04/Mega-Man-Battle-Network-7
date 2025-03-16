
using System;
using NUnit.Framework.Constraints;
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

        public ProjcetileSettings ProjectileSettings { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Chip(char code)
        {
            Code = code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Use(BattleEntity owner)
        {
            if (ProjectileSettings != null)
            {
                SpawnProjectile(owner);
            }
        }

        public abstract void OnHit(BattleEntity hit, Projectile projectile);

        private void SpawnProjectile(BattleEntity owner)
        {
            ProjectileSettings.Owner = owner;
            GameObject projectile = GameObject.CreatePrimitive(ProjectileSettings.Shape);
            Projectile script = projectile.AddComponent<Projectile>();
            Rigidbody rigidBody = projectile.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            Collider collider = projectile.GetComponent<Collider>();
            collider.isTrigger = true;
            script.projectileSettings = ProjectileSettings;
            float size = ProjectileSettings.Size / 10;
            projectile.transform.localScale = new Vector3(size, size, size);
            projectile.transform.parent = owner.transform;
            projectile.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
            projectile.transform.parent = BattleScene.Instance.GameScreen.Projectiles.transform;
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
            ProjectileSettings = new ProjcetileSettings
            {
                Speed = 0.1f,
                Delay = 5,
                Shape = PrimitiveType.Sphere,
                Size = 2,
                ChipSource = this,
            };
        }

        public override void OnHit(BattleEntity hit, Projectile projectile)
        {
            Debug.Log($"Hitting battle entity {hit.name}");
            hit.TakeDamage(Damage, Type);
            UnityEngine.Object.Destroy(projectile.gameObject);
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
            ProjectileSettings = new ProjcetileSettings
            {
                Speed = 0.03f,
                Delay = 1,
                Shape = PrimitiveType.Cube,
                Size = 4,
                ChipSource = this,
            };
        }

        public override void OnHit(BattleEntity hit, Projectile projectile)
        {
            Debug.Log($"Hitting battle entity {hit.name}");
            hit.TakeDamage(Damage, Type);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class Recover : Chip
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

        public override void OnHit(BattleEntity hit, Projectile projectile)
        {
            throw new NotImplementedException();
        }
    }
}