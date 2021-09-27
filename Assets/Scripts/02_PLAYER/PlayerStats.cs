using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    //VARS
    public float plyrAge = 10;
    public int plyrGrth = 0;
    public Sprite teenAlien;
    public Sprite preAdAlien;
    public Sprite adultAlien;
    public Animator animator;

    [Header("Players Stats")]
    [SerializeField] private float maxPlayerHealth = 100;
    [SerializeField] private float plyrHlth;
    [SerializeField] private float maxPlayerSpeed = 100;
    [SerializeField] public float plyrSpd = 5;
    [SerializeField] private float maxPlayerDamage = 100;
    [SerializeField] public float plyrDmg = 5;
    public float GetPlayerDamage() { return plyrDmg; } //called by buildings script

    [Header("UI GOs")]
    [SerializeField] private GameObject healthUI;
    [SerializeField] private GameObject speedAttackUI;
    [SerializeField] private GameObject pickUpsUI;

    [Header("Images")]
    [SerializeField] Image speedImg;
    [SerializeField] Image attackImg;

    [Header("UI Sliders")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider attackSlider;

    [Header("Pick Up Counters")]
    [SerializeField] private TMP_Text foodCounterText;
    [SerializeField] private TMP_Text drinkCounterText;
    private int foodCounter = 0;
    private int drinkCounter = 0;
    

    //REFS
    public PlayerController pC;
    public DestroyLevelManager destroyLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        plyrHlth = maxPlayerHealth;
        plyrSpd = 5.0f;
        plyrDmg = 5.0f;

        foodCounterText.text = foodCounter.ToString();
        drinkCounterText.text = drinkCounter.ToString();
    }

    private void Update()
    {
        UpdateUISliders();

        if (plyrHlth <= 0)
        {
            destroyLevelManager.SetGameOverPrompt();
            Destroy(gameObject);
        }
    }

    private void UpdateUISliders()
    {
        healthSlider.value = CalculateHealth();
        speedSlider.value = CalculateSpeed();
        attackSlider.value = CalculateDamage();
    }

    public void GrowCheck()
    {
        if (plyrGrth == 5)
        {
            animator.SetInteger("level", 1);
            plyrDmg += plyrAge;
            plyrSpd += plyrAge;
            UpdateSpd();
            Debug.Log("I have aged");
        }
        else if (plyrGrth == 10)
        {
            animator.SetInteger("level", 2);
            plyrDmg += plyrAge;
            plyrSpd += plyrAge;
            UpdateSpd();
            Debug.Log("I have aged");
        }
        else if (plyrGrth == 15)
        {
            animator.SetInteger("level", 3);
            plyrDmg += plyrAge;
            plyrSpd += plyrAge;

            pC.UpgradePlayerSpeed(plyrSpd);
            UpdateSpd();
            Debug.Log("I have aged");
        }
    }

    public void UpdateFoodCounter()
    {
        foodCounter++;
        foodCounterText.text = foodCounter.ToString();
    }

    public void UpdateDrinkCounter()
    {
        drinkCounter++;
        drinkCounterText.text = drinkCounter.ToString();
    }

    public void ResetCounters()
    {
        drinkCounter = 0;
        drinkCounterText.text = drinkCounter.ToString();

        foodCounter = 0;
        foodCounterText.text = foodCounter.ToString();
    }

    public void UpdateSpd()
    {
        pC.UpgradePlayerSpeed(plyrSpd);
    }

    private float CalculateHealth()
    {
        return plyrHlth / maxPlayerHealth;
    }
    private float CalculateSpeed()
    {
        return plyrSpd / maxPlayerSpeed;
    }
    private float CalculateDamage()
    {
        return plyrDmg / maxPlayerDamage;
    }

    public void SetPowerUpLevelUI()
    {
        healthUI.SetActive(false);
        speedAttackUI.SetActive(true);
        pickUpsUI.SetActive(true);
    }

    public void SetDestroyWorldUI()
    {
        healthUI.SetActive(true);
        speedAttackUI.SetActive(false);
        pickUpsUI.SetActive(false);
    }

    public void ReceiveBulletDamage(float bulletDamage)
    {
        plyrHlth -= bulletDamage;
    }

}
