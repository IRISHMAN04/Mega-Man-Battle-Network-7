
using System;
using UnityEditor.MPE;
using UnityEngine;
namespace Battle.Chips
{

    /// <summary>
    /// 
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public ProjcetileSettings projectileSettings;
        private DateTime spawnTime;


        private void Awake()
        {
            spawnTime = DateTime.Now;

        }

        private void Update()
        {
            if ((spawnTime - DateTime.Now).Seconds < projectileSettings.Delay)
            {
                if (float.IsPositiveInfinity(projectileSettings.Speed))
                {
                    Debug.Log("// TODO: Handle");
                }
                else if (float.IsNegativeInfinity(projectileSettings.Speed))
                {
                    Debug.Log("// TODO: Handle");
                }
                // TODO: Fix this so that it's using timeDelta
                else if (projectileSettings.Speed > 0)
                    gameObject.transform.position += gameObject.transform.forward * projectileSettings.Speed;
                else if (projectileSettings.Speed <= 0)
                    gameObject.transform.position -= gameObject.transform.forward * projectileSettings.Speed;
            }
        }

        void OnTriggerEnter(Collider other)
        {
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
                    // TODO: Handle
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
                    // TODO: Handle
                }
            }
            else if (other.gameObject.TryGetComponent(out BoxCollider killbox))
            {
                Debug.Log($"Hitting killbox {killbox.name}");
                Destroy(gameObject);
            }
        }
    }
}