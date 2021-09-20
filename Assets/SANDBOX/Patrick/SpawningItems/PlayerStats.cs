using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpawningItems
{
    public class PlayerStats : MonoBehaviour
    {
        //VARS
        public Text dmgText;
        public Text spdText;
        public float plyrDmg = 10;
        public float plyrSpd = 10;
        public float plyrHlth = 100;

        //REFS
        public PlayerController pC;

        // Start is called before the first frame update
        void Start()
        {
            dmgText.text = plyrDmg.ToString();
            spdText.text = plyrSpd.ToString();
        }

        public void UpdateDmg()
        {
            
            dmgText.text = plyrDmg.ToString(); 
        }

        public void UpdateSpd()
        {
            pC.playerSpeed = plyrSpd;
            spdText.text = plyrSpd.ToString();
        }
    }
}

