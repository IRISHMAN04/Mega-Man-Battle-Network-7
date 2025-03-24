
using System;
using System.Collections;
using Battle.Entity;
using UnityEngine;
using Utility;
namespace Battle
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

        public ProjectileSettings ProjectileSettings { get; protected set; }

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
        public virtual void Use(BattleEntity owner)
        {
            if (ProjectileSettings != null)
            {
                ProjectileSettings.Owner = owner;
                if (ProjectileSettings.Delay > 0)
                {
                    owner.Frozen = true;
                    BattleScene.Instance.StartCoroutine(Deay(ProjectileSettings.Delay));
                }
                else
                    CreateProjectile();
            }
        }

        public abstract void OnHit(BattleEntity hit, Projectile projectile);

        public void DealDamage(BattleEntity hit, Projectile projectile)
        {
            hit.TakeDamage(Damage, Type);
            ProjectileSettings settings = projectile.projectileSettings;
            if (settings.HitCount != -1)
            {
                settings.HitCount--;
                if (settings.HitCount == 0)
                {
                    UnityEngine.Object.Destroy(projectile.gameObject);
                }
            }
        }

        IEnumerator Deay(float delay)
        {
            yield return new WaitForSeconds(delay);
            CreateProjectile();
        }

        private void CreateProjectile()
        {
            GameObject projectile = GameObject.CreatePrimitive(ProjectileSettings.Shape);
            Projectile script = projectile.AddComponent<Projectile>();
            Rigidbody rigidBody = projectile.AddComponent<Rigidbody>();
            script.rb = rigidBody;
            rigidBody.useGravity = false;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            Collider collider = projectile.GetComponent<Collider>();
            collider.isTrigger = true;
            script.projectileSettings = ProjectileSettings;
            float size = ProjectileSettings.Size / 10;
            projectile.transform.localScale = new Vector3(size, size, size);
            projectile.transform.parent = ProjectileSettings.Owner.transform;
            projectile.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
            projectile.transform.parent = BattleScene.Instance.GameScreen.Projectiles.transform;
            ProjectileSettings.Owner.Frozen = false;
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
            ProjectileSettings = new ProjectileSettings
            {
                Speed = 50f,
                Delay = 0.1f,
                Shape = PrimitiveType.Sphere,
                Size = 2,
                ChipSource = this,
                HitCount = 1,
            };
        }

        public override void OnHit(BattleEntity hit, Projectile projectile)
        {
            DealDamage(hit, projectile);
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
            ProjectileSettings = new ProjectileSettings
            {
                Speed = 20f,
                Delay = 0.3f,
                Shape = PrimitiveType.Cube,
                Size = 4,
                ChipSource = this,
                HitCount = -1,
            };
        }

        public override void OnHit(BattleEntity hit, Projectile projectile)
        {
            DealDamage(hit, projectile);
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

        public override void Use(BattleEntity owner)
        {
            PlayerController.Instance.Health += Healing;
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