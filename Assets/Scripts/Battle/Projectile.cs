using System;
using Battle.Entity;
using UnityEngine;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public ProjectileSettings projectileSettings;

        public Rigidbody rb;


        private void Awake()
        {
        }

        private void FixedUpdate()
        {
            if (float.IsPositiveInfinity(projectileSettings.Speed))
            {
                throw new NotImplementedException("TODO: Handle raycast");
            }
            else if (float.IsNegativeInfinity(projectileSettings.Speed))
            {
                throw new NotImplementedException("TODO: Handle raycast");
            }
            else
            {
                Vector3 movement = rb.transform.forward * (projectileSettings.Speed * Time.fixedDeltaTime);
                if (projectileSettings.Speed > 0)
                    rb.MovePosition(rb.position + movement);
                else if (projectileSettings.Speed <= 0)
                    rb.MovePosition(rb.position - movement);
                else
                    throw new NotImplementedException();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Entering {other.name}");
            if (other.gameObject == projectileSettings.Owner.gameObject)
            {

            }
            else if (other.gameObject.TryGetComponent(out PlayerController playerController))
            {
                if (projectileSettings.ChipSource != null)
                {
                    projectileSettings.ChipSource.OnHit(playerController, this);
                }
                else
                {
                    throw new NotImplementedException(" TODO: handle buster shot");
                }
            }
            else if (other.gameObject.TryGetComponent(out BattleEntity battleEntity))
            {
                if (projectileSettings.ChipSource != null)
                {
                    projectileSettings.ChipSource.OnHit(battleEntity, this);
                }
                else
                {
                    throw new NotImplementedException(" TODO: handle buster shot");
                }
            }
            else if (other.gameObject.TryGetComponent(out BoxCollider killbox))
                Destroy(gameObject);
        }
    }
}