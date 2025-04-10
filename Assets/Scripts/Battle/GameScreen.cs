using Battle.Entity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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
        /// 
        /// </summary>
        public GameObject Projectiles;

        /// <summary>
        /// 
        /// </summary>
        public Image customBar;

        /// <summary>
        /// 
        /// </summary>
        public float customTime;

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
            customTime += Time.deltaTime;
            customBar.fillAmount = customTime / PlayerController.Instance.customTime;
            if (BattleScene.Debug)
                customTime += PlayerController.Instance.customTime;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SwitchToChip(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (customTime < PlayerController.Instance.customTime) return;
                    //TODO: Check if custom time has been completed
                    gameObject.SetActive(false);

                    BattleScene.Instance.ChipScreen.Initialise();
                    foreach (Transform child in PlayerController.Instance.qlg.transform)
                        Destroy(child.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
