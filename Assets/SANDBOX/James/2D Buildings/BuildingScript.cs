using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingScript : MonoBehaviour
{
    [SerializeField]
    private BuildingStats buildingStats = null;

    private float currentHealth;

    private bool gettingHit = false;
    private bool stillStanding = true;
    private float hitTime = 0.3f;

    private float playerDamage = 50f; // <---- REPLACE THIS WITH ACTUAL PLAYER DAMAGE REFERENCE

    public UnityEvent buildingDestroyed = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = buildingStats.health;
        Debug.Log(currentHealth);

        GetComponent<SpriteRenderer>().sprite = buildingStats.buildingSprite;
        Debug.Log(buildingStats.buildingSprite);

        buildingDestroyed.AddListener(DestroyBuilding);
    }

    void Update()
    {
        if (gettingHit)
        {
            hitTime -= Time.deltaTime;
            if (hitTime <= 0)
            {
                WasHit();

                hitTime = 0.3f;
            }
        }
        else
        {
            hitTime = 0.3f;
        }
    }

    void WasHit()
    {
        currentHealth -= playerDamage; // <---- REPLACE THIS WITH ACTUAL PLAYER DAMAGE REFERENCE
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            buildingDestroyed.Invoke();
        }
    }

    void DestroyBuilding()
    {
        GetComponent<SpriteRenderer>().sprite = buildingStats.rubbleSprite;
        GetComponent<BoxCollider2D>().enabled = false;
        stillStanding = false;
        currentHealth = buildingStats.health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && stillStanding)
        {
            gettingHit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gettingHit = false;
        }
    }

}
