using System;

namespace Battle.Entity
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

        /// <summary>
        /// Time that it takes for the custom window to open
        /// </summary>
        [NonSerialized]
        public int customTime = 10;

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Setup(100, 100);
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