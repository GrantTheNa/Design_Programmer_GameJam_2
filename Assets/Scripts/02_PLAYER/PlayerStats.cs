using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    //VARS
    public Text dmgText;
    public Text spdText;
    //public float plyrDmg = 10;
    //public float plyrSpd = 10;
    //public float plyrHlth = 100;
    public float plyrAge = 10;
    public int plyrGrth = 0;
    public Sprite teenAlien;
    public Sprite preAdAlien;
    public Sprite adultAlien;
    public GameObject aSprite;

    [Header("Players Stats")]
    [SerializeField] private float maxPlayerHealth = 100;
    [SerializeField] private float plyrHlth;
    [SerializeField] private float maxPlayerSpeed = 100;
    [SerializeField] public float plyrSpd = 10;
    [SerializeField] private float maxPlayerDamage = 100;
    [SerializeField] public float plyrDmg = 10;

    [Header("UI GOs")]
    [SerializeField] private GameObject healthUI;
    [SerializeField] private GameObject speedUI;
    [SerializeField] private GameObject attackUI;

    [Header("TMPro")]
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text attackText;

    [Header("Images")]
    [SerializeField] Image speedImg;
    [SerializeField] Image attackImg;


    //[Header("UI Sliders")]
    //[SerializeField] private Slider healthSlider;
    //[SerializeField] private Slider speedSlider;
    //[SerializeField] private Slider attackSlider;


    //REFS
    public PlayerController pC;

    // Start is called before the first frame update
    void Start()
    {
        plyrHlth = maxPlayerHealth;
    }

    private void Update()
    {
        healthText.text = plyrHlth.ToString();
        speedText.text = plyrSpd.ToString();
        attackText.text = plyrDmg.ToString();

        //if (plyrSpd > 20)
        //{
        //    speedImg.sprite = Resources.Load("HUD/Speed_Speed") as Sprite;
        //}

        //if (plyrDmg > 20)
        //{
        //    attackImg.sprite = Resources.Load("HUD/Strength_strong") as Sprite;
        //}

        //healthSlider.value = CalculateHealth();
        //speedSlider.value = CalculateSpeed();
        //attackSlider.value = CalculateDamage();

        if (plyrHlth <= 0) 
            MASTER_GameManager.Instance.GoToGameOverScene();
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
        //dmgText.text = plyrDmg.ToString();
        attackText.text = plyrDmg.ToString();
    }

    public void UpdateSpd()
    {
        pC.playerSpeed = plyrSpd;
        //spdText.text = plyrSpd.ToString();

        speedText.text = plyrSpd.ToString();
    }



    //private float CalculateHealth()
    //{
    //    return plyrHlth / maxPlayerHealth;
    //}
    //private float CalculateSpeed()
    //{
    //    return plyrSpd / maxPlayerSpeed;
    //}
    //private float CalculateDamage()
    //{
    //    return plyrDmg / maxPlayerDamage;
    //}

    public void SetPowerUpLevelUI()
    {
        healthUI.SetActive(false);
        speedUI.SetActive(true);
        attackUI.SetActive(true);
    }

    public void SetDestroyWorldUI()
    {
        healthUI.SetActive(true);
        speedUI.SetActive(false);
        attackUI.SetActive(false);
    }

    public void ReceiveBulletDamage(float bulletDamage)
    {
        plyrHlth -= bulletDamage;
    }

}
