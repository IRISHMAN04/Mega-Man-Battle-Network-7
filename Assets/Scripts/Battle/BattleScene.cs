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
        public Tile[,] Tiles;

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
        public Stack<Chip> ChipStack;

        /// <summary>
        /// 
        /// </summary>
        public List<ChipTile> ChipTiles;

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
            Tiles = new Tile[3, 6];
            ChipSelectionCount = 5;
            ChipStack = new Stack<Chip>();
            char[] CODES = new char[] { 'A', 'B', 'C', 'D', 'E' };

            for (int i = 0; i < ChipSelectionCount; i++)
            {
                int chipSelection = UnityEngine.Random.Range(0, 3);
                ChipTiles[i].SetChip(chipSelection switch
                {
                    0 => new Cannon(CODES[UnityEngine.Random.Range(0, 5)]),
                    1 => new Shockwave(CODES[UnityEngine.Random.Range(0, 5)]),
                    2 => new Recover10(CODES[UnityEngine.Random.Range(0, 5)]),
                    _ => throw new NotImplementedException($"Chip {chipSelection}"),
                });
            }

            for (int i = ChipSelectionCount; i < ChipTiles.Count; i++)
            {
                ChipTiles[i].gameObject.SetActive(false);
            }

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    Tiles[x, y] = GameObject.Find($"{x},{y}").GetComponent<Tile>();
                }
            }
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void SendChips()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void AddChips()
        {

        }

        public void SwitchToGame()
        {
            //TODO: Disable this player input
            //TODO: Hide chip screen
            //TODO: Enable megaman player input
        }
    }
}