using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Chips;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utility;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class ChipScreen : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        public PlayerInput PlayerInput { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI chipName;

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI chipCode;

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI chipElement;

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI chipDamage;

        /// <summary>
        /// 
        /// </summary>
        public Image chipIcon;

        /// <summary>
        /// 
        /// </summary>
        private int tileX = 0;

        /// <summary>
        /// 
        /// </summary>
        private int tileY = 0;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            PlayerInput = GetComponent<PlayerInput>();
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
            //TODO: Setup current in use chips
            SwitchToGame();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddChips()
        {
            //TODO: Increase ChipSelectionCount
            SwitchToGame();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SwitchToGame()
        {
            //TODO: Disable pressing buttons by pressing space globally
            gameObject.SetActive(false);
            BattleScene.Instance.GameScreen.gameObject.SetActive(true);
        }

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
            ChipTile tile;
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
                tile = BattleScene.Instance.ChipTiles[tileX + xDiff, tileY + yDiff];
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
            if (tile.chip != null)
            {
                BattleScene.Instance.ChipTiles[tileX, tileY].selection.SetActive(false);
                tileX += xDiff;
                tileY += yDiff;
                tile.selection.SetActive(true);
                SetCurrentChip(tile.chip);
            }
        }

        public void SetCurrentChip(Chip chip)
        {
            chipName.text = chip.Name;
            chipCode.text = chip.Code.ToString();
            chipElement.text = chip.Type switch
            {
                DamageType.Normal => "•",
                DamageType.Aqua => "💧",
                DamageType.Fire => "🔥",
                DamageType.Elec => "⚡",
                DamageType.Wood => "🌿",
                _ => throw new NotImplementedException($"Damage type {chip.Type} not implemented"),
            };
            if (chip.Damage > 0)
                chipDamage.text = chip.Damage.ToString();
            else
                chipDamage.text = "";
            chipIcon.sprite = BattleScene.Instance.ChipTypes.First(x => x.name == chip.Name);
        }
    }
}
