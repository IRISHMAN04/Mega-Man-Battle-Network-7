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
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public Tile[,] tiles;

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
        public static BattleScene Instance;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            tiles = new Tile[3, 6];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tiles[x, y] = GameObject.Find($"{x},{y}").GetComponent<Tile>();
                }
            }
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }
    }
}