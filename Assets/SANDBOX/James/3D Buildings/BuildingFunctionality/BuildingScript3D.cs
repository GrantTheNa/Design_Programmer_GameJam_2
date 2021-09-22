using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingScript3D : MonoBehaviour
{
    [SerializeField]
    private BuildingStats3D buildingStats = null; // A holder for Scriptable Objects (Building Stats)

    private AudioSource audSource;
    private AudioSource buildAudSrc;

    private PlayerStats playerStats;

    private float currentHealth; // The current health of the building

    private bool gettingHit = false; // The state of being hit by the player
    private bool halfWay = false; // The state where a building has less than 50% health remaining
    private bool stillStanding = true; // A state stating whether a building is destroyed or not
    private float hitTime = 0.3f; // How quickly the player hits a building

    private float playerDamage = 50f; // <---- REPLACE THIS WITH ACTUAL PLAYER DAMAGE REFERENCE

    public UnityEvent buildingDestroyed = new UnityEvent(); // A Universal Event for when a building is destroyed

    // Start is called before the first frame update
    void Start()
    {
        playerStats = MASTER_GameManager.Instance.player.GetComponent<PlayerStats>();
        
        currentHealth = buildingStats.health;
        //Debug.Log(currentHealth);

        audSource = GetComponent<AudioSource>();
        buildAudSrc = GetComponentInChildren<Transform>().GetChild(2).GetComponent<AudioSource>();
        buildAudSrc.clip = buildingStats.hitSound;

        buildingDestroyed.AddListener(DestroyBuilding); // Run DestroyBuilding when Event is Invoked
    }

    void Update()
    {
        if (gettingHit) // If Player is in range of a building
        {
            hitTime -= Time.deltaTime; // How long until a player 'hits' the building
            if (hitTime <= 0) // IF time is up...
            {
                WasHit(); // Run 'Hit' Function

                hitTime = 0.3f; // Reset time to hit again.
            }
        }
        else
        {
            hitTime = 0.3f; // ELSE... hitTime is reset
        }
    }

    void WasHit()
    {
        //Debug.Log("Building Health: " + currentHealth + " minus " + playerStats.GetPlayerDamage());
        currentHealth -= playerStats.GetPlayerDamage();
        //Debug.Log("New Building Health " + currentHealth);
        
        buildAudSrc.Play();
        //Debug.Log(currentHealth);

        if (currentHealth < buildingStats.health / 2 && !halfWay) // IF this is the first instance where health has dropped below 50%...
        {
            GameObject oldModel = GetComponentInChildren<Transform>().GetChild(2).gameObject; // Find current model of building
            Destroy(oldModel); // Destroy current model of building
            GameObject newModel = Instantiate(buildingStats.brokenModel, transform.position, Quaternion.Euler(270f, 270f, 270f)); // Make new building model
            newModel.transform.parent = gameObject.transform; // Make this new model a child of THIS gameobject

            buildAudSrc = newModel.GetComponent<AudioSource>();
            buildAudSrc.clip = buildingStats.hitSound;

            audSource.clip = buildingStats.brokenSound;
            audSource.Play();
            

            halfWay = true; // Stop this IF statement from running again
        }
        if (currentHealth <= 0) // IF health drops below 0&
        {
            buildingDestroyed.Invoke(); // Run this event to Destroy the building
        }
    }

    void DestroyBuilding()
    {
        currentHealth = buildingStats.health; // Reset building health in Scriptable Object

        GameObject oldModel = GetComponentInChildren<Transform>().GetChild(2).gameObject; // Find current model of building
        Destroy(oldModel); // Destroy current model of building
        GameObject newModel = Instantiate(buildingStats.rubbleModel, transform.position, Quaternion.Euler(270f, 270f, 270f)); // Make new building model (CHANGE MIDDLE VALUE FOR UP-RIGHT ROTATION)
        newModel.transform.parent = gameObject.transform; // Make this new model a child of THIS gameobject

        //buildAudSrc = newModel.GetComponent<AudioSource>();
        //buildAudSrc.clip = buildingStats.hitSound;

        audSource.clip = buildingStats.rubbleSound;
        audSource.Play();

        stillStanding = false; // The building can no longer get hit again
        gettingHit = false; // Stop hitting this building
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stillStanding) // IF building is standing and player enters range...
        {
            gettingHit = true; // Player starts hitting building
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // IF player leaves building range
        {
            gettingHit = false; // Player stops hitting building
        }
    }

}
