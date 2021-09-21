﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrowingBaby
{
    public class PlayerStats : MonoBehaviour
    {
        //VARS
        public Text dmgText;
        public Text spdText;
        public float plyrDmg = 10;
        public float plyrSpd = 10;
        public float plyrHlth = 100;
        public int plyrGrth = 0;
        public Sprite teenAlien;
        public Sprite preAdAlien;
        

        //REFS
        public PlayerController pC;

        // Start is called before the first frame update
        void Start()
        {
            dmgText.text = plyrDmg.ToString();
            spdText.text = plyrSpd.ToString();
        }

        public void Update()
        {
            if (plyrGrth == 5)
            {
                GetComponent<SpriteRenderer>().sprite = teenAlien;
            }
            else if (plyrGrth == 10)
            {

            }
            else if (plyrGrth == 15)
            {

            }
        }

        public void UpdateDmg()
        {
            plyrGrth += 1;
            dmgText.text = plyrDmg.ToString(); 
        }

        public void UpdateSpd()
        {
            plyrGrth += 1;
            pC.playerSpeed = plyrSpd;
            spdText.text = plyrSpd.ToString();
        }
    }
}

