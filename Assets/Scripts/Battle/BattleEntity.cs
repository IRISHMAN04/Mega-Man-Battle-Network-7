using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Chips;
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

        /// <summary>
        /// 
        /// </summary>
        public bool isPlayer = false;

        /// <summary>
        /// 
        /// </summary>
        public int Health
        {
            get
            {
                return _health;
            }
            private set
            {
                Debug.Log("Health changing");
                _health = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private int _health = 100;

        private Queue<Chip> chips = new Queue<Chip>();

        /// <summary>
        /// 
        /// </summary>
        public int tileX;

        /// <summary>
        /// 
        /// </summary>
        public int tileY;

        /// <summary>
        /// 
        /// </summary>
        private DamageType type = DamageType.Normal;

        /// <summary>
        /// To be used to freeze an entity when they shoot
        /// TODO: Change this to be a state machine to encompass more freezable activities
        /// </summary>
        public bool Frozen;

        #region Damage

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="damageType"></param>
        /// <param name="enemy"></param>
        public void DealDamage(int damage, DamageType damageType, BattleEntity enemy)
        {
            //TODO: Add any buffs to certain damage type
            enemy.TakeDamage(damage, damageType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="damageType"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void TakeDamage(int damage, DamageType damageType)
        {
            switch (type)
            {
                case DamageType.Normal:
                    TakeFull(damage);
                    break;
                case DamageType.Aqua:
                    switch (damageType)
                    {
                        case DamageType.Normal:
                            TakeFull(damage);
                            break;
                        case DamageType.Aqua:
                            TakeFull(damage);
                            break;
                        case DamageType.Fire:
                            TakeHalf(damage);
                            break;
                        case DamageType.Elec:
                            TakeDouble(damage);
                            break;
                        case DamageType.Wood:
                            TakeFull(damage);
                            break;
                        default:
                            throw new NotImplementedException($"Damage Type {damageType} not implemented");
                    }
                    break;
                case DamageType.Fire:
                    switch (damageType)
                    {
                        case DamageType.Normal:
                            TakeFull(damage);
                            break;
                        case DamageType.Aqua:
                            TakeDouble(damage);
                            break;
                        case DamageType.Fire:
                            TakeFull(damage);
                            break;
                        case DamageType.Elec:
                            TakeFull(damage);
                            break;
                        case DamageType.Wood:
                            TakeHalf(damage);
                            break;
                        default:
                            throw new NotImplementedException($"Damage Type {damageType} not implemented");
                    }
                    break;
                case DamageType.Elec:
                    switch (damageType)
                    {
                        case DamageType.Normal:
                            TakeFull(damage);
                            break;
                        case DamageType.Aqua:
                            TakeHalf(damage);
                            break;
                        case DamageType.Fire:
                            TakeFull(damage);
                            break;
                        case DamageType.Elec:
                            TakeFull(damage);
                            break;
                        case DamageType.Wood:
                            TakeDouble(damage);
                            break;
                        default:
                            throw new NotImplementedException($"Damage Type {damageType} not implemented");
                    }
                    break;
                case DamageType.Wood:
                    switch (damageType)
                    {
                        case DamageType.Normal:
                            TakeFull(damage);
                            break;
                        case DamageType.Aqua:
                            TakeFull(damage);
                            break;
                        case DamageType.Fire:
                            TakeDouble(damage);
                            break;
                        case DamageType.Elec:
                            TakeHalf(damage);
                            break;
                        case DamageType.Wood:
                            TakeFull(damage);
                            break;
                        default:
                            throw new NotImplementedException($"Damage Type {damageType} not implemented");
                    }
                    break;
                default:
                    throw new NotImplementedException($"Damage Type {type} not implemented");
            }
            if (Health <= 0)
            {
                if (isPlayer)
                {
                    // TODO: Handle
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        private void TakeHalf(int damage)
        {
            Health -= damage / 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        private void TakeFull(int damage)
        {
            Health -= damage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        private void TakeDouble(int damage)
        {
            Health -= damage * 2;
        }

        #endregion Damage

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        protected virtual void Start()
        {
            GameTile spawnTile = gameObject.transform.parent.GetComponent<GameTile>();
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

        #region Movement

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Move(Direction direction)
        {
            if (Frozen)
                return;
            GameTile tile;
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
                tile = BattleScene.Instance.GameScreen.GameTiles[tileX + xDiff, tileY + yDiff];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            // TODO: Fix this. Won't work for enemy movement lol, don't know what I was thinking 15 months ago
            if (tile.PlayerOwned)
            {
                tileX += xDiff;
                tileY += yDiff;
                gameObject.transform.SetParent(tile.transform);
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 0);
            }
        }

        #endregion Movement

        #region Chips
        public void SendChips(IEnumerable<Chip> sentChips) => chips = new Queue<Chip>(sentChips);

        public void UseChip(InputAction.CallbackContext context)
        {
            if (!Frozen)
                switch (context.phase)
                {
                    case InputActionPhase.Performed:
                        if (chips.TryDequeue(out Chip chip))
                            chip.Use(this);
                        break;
                    default:
                        break;
                }
        }

        #endregion Chips
    }
}