using System;
using UnityEngine;

namespace Battle
{
    /// <summary>
    /// 
    /// </summary>
    public class Tile : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        public bool PlayerOwned
        {
            get
            {
                if (renderer.material.name.StartsWith(BattleScene.Instance.PlayerTile.name))
                    return true;
                else if (renderer.material.name.StartsWith(BattleScene.Instance.EnemyTile.name))
                    return false;
                else
                {
                    Debug.Log(renderer.material.name);
                    throw new NotImplementedException(renderer.material.name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private new Renderer renderer;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }
    }
}