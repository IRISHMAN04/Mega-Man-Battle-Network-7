using System;
using System.Collections.Generic;
using Battle.Chips;
using TMPro;
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
        public GameTile[,] GameTiles;

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
        public TextMeshProUGUI PlayerHealth;

        /// <summary>
        /// 
        /// </summary>
        public int ChipSelectionCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Stack<ChipTile> ChipStack;

        /// <summary>
        /// 
        /// </summary>
        public ChipTile[,] ChipTiles;

        /// <summary>
        /// 
        /// </summary>
        public List<Sprite> ChipTypes;

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
            ChipSelectionCount = 5;
            char[] CODES = new char[] { 'A', 'B', 'C', 'D', 'E' };

            ChipTiles = new ChipTile[3, 5];
            int enabled = 0;
            for (int x = 0; x < ChipTiles.GetLength(0); x++)
            {
                Transform chipRow = GameObject.Find($"Chip row {x}").transform;
                for (int y = 0; y < ChipTiles.GetLength(1); y++)
                {
                    ChipTile thisTile = chipRow.Find($"Chip {y}").GetComponent<ChipTile>();
                    if (enabled < ChipSelectionCount)
                    {
                        enabled++;
                        int chipSelection = UnityEngine.Random.Range(0, 3);
                        thisTile.SetChip(chipSelection switch
                        {
                            0 => new Cannon(CODES[UnityEngine.Random.Range(0, 5)]),
                            1 => new Shockwave(CODES[UnityEngine.Random.Range(0, 5)]),
                            2 => new Recover10(CODES[UnityEngine.Random.Range(0, 5)]),
                            _ => throw new NotImplementedException($"Chip {chipSelection}"),
                        });
                    }
                    else
                        thisTile.gameObject.SetActive(false);
                    ChipTiles[x, y] = thisTile;
                }
            }

            ChipScreen.SetCurrentChip(ChipTiles[0,0].chip);

            ChipStack = new Stack<ChipTile>();

            GameTiles = new GameTile[3, 6];
            for (int x = 0; x < ChipTiles.GetLength(0); x++)
                for (int y = 0; y < ChipTiles.GetLength(1); y++)
                    GameTiles[x, y] = GameObject.Find($"{x},{y}").GetComponent<GameTile>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }
    }
}