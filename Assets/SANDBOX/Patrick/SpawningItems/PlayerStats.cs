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
        public float plyrAge = 10;
        public int plyrGrth = 0;
        public Sprite teenAlien;
        public Sprite preAdAlien;
        public Sprite adultAlien;
        public GameObject aSprite;
        

        //REFS
        public PlayerController pC;

        // Start is called before the first frame update
        void Start()
        {
            dmgText.text = plyrDmg.ToString();
            spdText.text = plyrSpd.ToString();
        }

        public void GrowCheck()
        {
            if (plyrGrth == 5)
            {
                aSprite.GetComponent<SpriteRenderer>().sprite = teenAlien;
                plyrDmg += plyrAge;
                plyrSpd += plyrAge;
                UpdateSpd();
                UpdateDmg();
                Debug.Log("I have aged");
            }
            else if (plyrGrth == 10)
            {
                aSprite.GetComponent<SpriteRenderer>().sprite = preAdAlien;
                plyrDmg += plyrAge;
                plyrSpd += plyrAge;
                UpdateSpd();
                UpdateDmg();
                Debug.Log("I have aged");
            }
            else if (plyrGrth == 15)
            {
                aSprite.GetComponent<SpriteRenderer>().sprite = adultAlien;
                plyrDmg += plyrAge;
                plyrSpd += plyrAge;
                UpdateSpd();
                UpdateDmg();
                Debug.Log("I have aged");
            }
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

