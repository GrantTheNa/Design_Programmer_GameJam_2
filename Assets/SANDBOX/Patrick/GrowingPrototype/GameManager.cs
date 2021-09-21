using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrowingPrototype
{
    public class GameManager : MonoBehaviour
    {
        #region Singelton

        public static GameManager instance;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        #endregion

        public GameObject ground;
        public Transform endPoint;

        // Update is called once per frame
        void Update()
        {

        }
    }
}