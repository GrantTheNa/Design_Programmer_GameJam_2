using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //VARS
    public Text dmgText;
    public Text spdText;
    public float plyrDmg = 100;
    public float plyrSpd = 100;
    public float plyrHlth = 100;



    // Start is called before the first frame update
    void Start()
    {
        dmgText.text = plyrDmg.ToString();
        spdText.text = plyrSpd.ToString();
    }

    public void UpdateText()
    {
        dmgText.text = plyrDmg.ToString();
        spdText.text = plyrSpd.ToString();
    }
}
