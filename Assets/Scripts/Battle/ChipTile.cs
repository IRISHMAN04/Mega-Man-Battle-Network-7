using System.Linq;
using Battle.Chips;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Battle
{

    /// <summary>
    /// 
    /// </summary>
    public class ChipTile : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        public Chip chip;

        /// <summary>
        /// 
        /// </summary>
        public Image icon;

        /// <summary>
        /// 
        /// </summary>
        public TextMeshProUGUI code;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        void Start()
        {

        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        void Update()
        {

        }

        public void SetChip(Chip newChip) {
            chip = newChip;
            icon.sprite = BattleScene.Instance.ChipTypes.First(x => x.name == chip.Name);
            code.text = chip.Code.ToString();
        }
    }
}