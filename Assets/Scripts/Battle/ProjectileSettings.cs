using Battle.Entity;
using UnityEngine;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class ProjectileSettings
    {

        /// <summary>
        /// Unit units to move per second
        /// TODO:
        /// if speed is 0, projectile should not shoot
        /// if speed is negative, it goes backwards
        /// if speed is positive it goes forward
        /// if speed is positive infinity it instantly hits the target in front
        /// if speed is negative infinity it instantly hits the target behind
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// Seconds to delay 
        /// </summary>
        public float Delay { get; set; }


        /// <summary>
        /// TODO: Replace Size and Shape with a prefab
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// TODO: Replace Size and Shape with a prefab
        /// </summary>
        public PrimitiveType Shape { get; set; }

        /// <summary>
        /// The BattleEntity that spawned the projectile
        /// </summary>
        public BattleEntity Owner { get; set; }

        /// <summary>
        /// If the source of the projectile is a chip, will be the chip, else will be null
        /// </summary>
        public Chip ChipSource { get; set; }

        /// <summary>
        /// Amount of hits before the projectile is deleted
        /// </summary>
        public int HitCount { get; set; } = -1;

        // TODO: Trail?

    }
}