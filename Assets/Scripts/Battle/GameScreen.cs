using UnityEngine;
using UnityEngine.InputSystem;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class GameScreen : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        public PlayerInput PlayerInput { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public GameTile[,] GameTiles;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {
            PlayerInput = GetComponent<PlayerInput>();

            GameTiles = new GameTile[3, 6];
            for (int x = 0; x < GameTiles.GetLength(0); x++)
                for (int y = 0; y < GameTiles.GetLength(1); y++)
                    GameTiles[x, y] = GameObject.Find($"{x},{y}").GetComponent<GameTile>();
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
        public void SwitchToChip(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    //TODO: Check if custom time has been completed
                    gameObject.SetActive(false);
                    BattleScene.Instance.ChipScreen.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
