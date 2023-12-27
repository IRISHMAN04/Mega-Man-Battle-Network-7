using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    }
}
