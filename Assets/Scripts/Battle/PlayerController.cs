using UnityEngine.InputSystem;
namespace Battle
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerController : BattleEntity
    {

        /// <summary>
        /// 
        /// </summary>
        public static PlayerController Instance;

        public PlayerInput PlayerInput { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(this);
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        protected override void Start()
        {
            base.Start();
            BattleScene.Instance.PlayerHealth.text = Health.ToString();
            PlayerInput = GetComponent<PlayerInput>();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        protected override void Update()
        {
            base.Update();

        }
    }
}