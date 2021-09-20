﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingScript3D : MonoBehaviour
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

        //GetComponentInChildren<SpriteRenderer>().sprite = buildingStats.buildingSprite;
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
        //GetComponentInChildren<SpriteRenderer>().sprite = buildingStats.rubbleSprite;
        //GetComponent<BoxCollider>().enabled = false;
        //stillStanding = false;
        //currentHealth = buildingStats.health;
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stillStanding)
        {
            gettingHit = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gettingHit = false;
        }
    }

}
