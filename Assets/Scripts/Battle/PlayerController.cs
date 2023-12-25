namespace Battle
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerController : BattleEntity
    {

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        protected override void Start()
        {
            base.Start();
            BattleScene.Instance.PlayerHealth.text = Health.ToString();
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