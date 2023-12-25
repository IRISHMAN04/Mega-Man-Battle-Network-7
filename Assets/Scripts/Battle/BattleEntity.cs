using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Utility;
namespace Battle
{
    /// <summary>
    /// 
    /// </summary>
    public class BattleEntity : MonoBehaviour
    {

        public bool isPlayer = false;

        public int Health { get; set; }

        public void DealDamage(int damage, DamageType type)
        {

        }

        public void TakeDamage(int damage, DamageType type)
        {

        }

        public int tileX;

        public int tileY;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        protected virtual void Start()
        {
            Tile spawnTile = gameObject.transform.parent.GetComponent<Tile>();
            IEnumerable<int> tileName = spawnTile.gameObject.name.Split(",").Select(e => int.Parse(e));
            tileX = tileName.First();
            tileY = tileName.Last();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected virtual void Update()
        {

        }

        public void MoveUp(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Move(Direction.Up);
                    break;
                default:
                    break;
            }
        }

        public void MoveRight(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Move(Direction.Right);
                    break;
                default:
                    break;
            }
        }

        public void MoveDown(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Move(Direction.Down);
                    break;
                default:
                    break;
            }
        }

        public void MoveLeft(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Move(Direction.Left);
                    break;
                default:
                    break;
            }
        }

        private void Move(Direction direction)
        {
            Tile tile;
            int xDiff;
            int yDiff;
            switch (direction)
            {
                case Direction.Up:
                    xDiff = -1;
                    yDiff = 0;
                    break;
                case Direction.Right:
                    xDiff = 0;
                    yDiff = 1;
                    break;
                case Direction.Down:
                    xDiff = 1;
                    yDiff = 0;
                    break;
                case Direction.Left:
                    xDiff = 0;
                    yDiff = -1;
                    break;
                default:
                    throw new NotImplementedException($"Direction {direction} not supported");
            }
            try
            {
                tile = BattleScene.Instance.tiles[tileX + xDiff, tileY + yDiff];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            if (tile.PlayerOwned)
            {
                tileX += xDiff;
                tileY += yDiff;
                gameObject.transform.SetParent(tile.transform);
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 0);
            }
        }
    }
}