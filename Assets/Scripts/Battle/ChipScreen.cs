using System;
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
        public GameObject selectionStack;

        /// <summary>
        /// 
        /// </summary>
        public List<ChipTile> chipSelectionTiles;

        /// <summary>
        /// 
        /// </summary>
        public int ChipSelectionCount;

        /// <summary>
        /// 
        /// </summary>
        public ChipTile[,] ChipTiles;

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
        /// 
        /// </summary>
        private bool chipGridHasFocus = true;

        /// <summary>
        /// 
        /// </summary>
        public ChipScreenButton OKButton;

        /// <summary>
        /// 
        /// </summary>
        public ChipScreenButton AddButton;

        /// <summary>
        /// 
        /// </summary>
        private static readonly Color WHITE = new(1, 1, 1, 1);

        /// <summary>
        /// 
        /// </summary>
        private static readonly Color GRAY = new(1, 1, 1, 0.2470588235294118f);

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            PlayerInput = GetComponent<PlayerInput>();
            chipSelectionTiles = new List<ChipTile>();
            foreach (ChipTile chipTile in selectionStack.GetComponentsInChildren<ChipTile>())
            {
                chipSelectionTiles.Add(chipTile);
                chipTile.SetChip(null);
            }

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
                        char code = CODES[UnityEngine.Random.Range(0, 5)];
                        thisTile.SetChip(chipSelection switch
                        {
                            0 => new Cannon(code),
                            1 => new Shockwave(code),
                            2 => new Recover10(code),
                            _ => throw new NotImplementedException($"Chip {chipSelection}"),
                        });
                    }
                    else
                        thisTile.SetChip(null);
                    ChipTiles[x, y] = thisTile;
                }
            }

            SetCurrentChip(ChipTiles[0, 0].chip);
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
            if (tileY + yDiff == ChipTiles.GetLength(1))
            {
                HandleOffBoard(xDiff, yDiff, tileX, tileY);
                return;
            }
            int newX = tileX;
            int newY = tileY;
            do
            {
                newX += xDiff;
                newY += yDiff;
                if (newY < 0)
                {
                    newY = ChipTiles.GetLength(1) - 1;
                    newX -= 1;
                }
                if (newY == ChipTiles.GetLength(1))
                {
                    HandleOffBoard(xDiff, yDiff, newX, newY);
                    break;
                }
                try
                {
                    tile = ChipTiles[newX, newY];
                }
                catch (IndexOutOfRangeException)
                {
                    return;
                }
                if (tile.chip != null)
                {
                    if (chipGridHasFocus)
                        ChipTiles[tileX, tileY].selection.SetActive(false);
                    else
                    {
                        OKButton.selection.SetActive(false);
                        AddButton.selection.SetActive(false);
                        chipGridHasFocus = true;
                    }
                    tileX = newX;
                    tileY = newY;
                    tile.selection.SetActive(true);
                    SetCurrentChip(tile.chip);
                }
            } while (tile.chip == null);
        }

        private void HandleOffBoard(int xDiff, int yDiff, int oldX, int oldY)
        {
            if (xDiff == 1)
            {
                OKButton.selection.SetActive(false);
                AddButton.selection.SetActive(true);
            }
            else if (xDiff == -1)
            {
                OKButton.selection.SetActive(true);
                AddButton.selection.SetActive(false);
            }
            else if (xDiff == 0 && chipGridHasFocus)
            {
                ChipTiles[tileX, tileY].selection.SetActive(false);
                tileY = Math.Clamp(oldY + yDiff, 0, ChipTiles.GetLength(1));
                (tileX == 0 ? OKButton : AddButton).selection.SetActive(true);
                chipGridHasFocus = false;
            }
            tileX = Math.Clamp(oldX + xDiff, 0, ChipTiles.GetLength(0) - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void SelectChip(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    //TODO: Check if current selection is a chip or a button
                    // Halt if hidden
                    if (ChipTiles[tileX, tileY].Hidden) return;
                    // Halt if greyed out
                    if (ChipTiles[tileX, tileY].icon.color.a != 1) return;
                    ChipTile firstFree = chipSelectionTiles.FirstOrDefault(x => x.chip == null);
                    if (firstFree != null)
                    {
                        firstFree.SetChip(ChipTiles[tileX, tileY].chip);
                        firstFree.AssociatedTileID = ChipTiles[tileX, tileY].TileID;
                        ChipTiles[tileX, tileY].HideChip();
                        FilterChips();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void UnSelectChip(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    ChipTile lastUsed = chipSelectionTiles.LastOrDefault(x => x.chip != null);
                    if (lastUsed != null)
                    {
                        lastUsed.SetChip(null);
                        ChipTile found = ChipTiles.First(x => x.TileID == lastUsed.AssociatedTileID);
                        if (found != null)
                        {
                            lastUsed.AssociatedTileID = null;
                            found.ShowChip();
                            FilterChips();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void FilterChips()
        {
            IEnumerable<Chip> selectedChips = chipSelectionTiles.Where(e => e.chip != null).Select(e => e.chip);
            IEnumerable<char> selectedCodes = selectedChips.Select(e => e.Code).ToHashSet();
            int selectedCodesCount = selectedCodes.Count();
            IEnumerable<string> selectedNames = selectedChips.Select(e => e.Name).ToHashSet();
            int selectedNamesCount = selectedNames.Count();
            int totalCount = selectedCodesCount + selectedNamesCount;

            foreach (ChipTile x in ChipTiles.Where(e => e.chip != null))
                x.icon.color = WHITE;

            if (totalCount == 2)
                foreach (ChipTile x in ChipTiles.Where(e => e.chip != null && e.chip.Code != selectedCodes.First() && e.chip.Name != selectedNames.First()))
                    x.icon.color = GRAY;
            else if (selectedCodesCount > selectedNamesCount)
                foreach (ChipTile x in ChipTiles.Where(e => e.chip != null && e.chip.Name != selectedNames.First()))
                    x.icon.color = GRAY;
            else if (totalCount != 0)
                foreach (ChipTile x in ChipTiles.Where(e => e.chip != null && e.chip.Code != selectedCodes.First()))
                    x.icon.color = GRAY;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="chip"></param>
        /// <exception cref="NotImplementedException"></exception>
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
