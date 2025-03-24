using System.Collections.Generic;
using Battle.Entity;
using UnityEngine;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class BattleScene : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        public static BattleScene Instance;

        /// <summary>
        /// 
        /// </summary>
        public GameScreen GameScreen;

        /// <summary>
        /// 
        /// </summary>
        public ChipScreen ChipScreen;

        /// <summary>
        /// 
        /// </summary>
        public PlayerController PlayerController;

        /// <summary>
        /// 
        /// </summary>
        public Material PlayerTile;

        /// <summary>
        /// 
        /// </summary>
        public Material EnemyTile;

        /// <summary>
        /// 
        /// </summary>
        public List<Sprite> ChipTypes;

        /// <summary>
        /// Prefab of the chip with no description
        /// </summary>
        public GameObject ChipNoDescPrefab;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }
    }
}