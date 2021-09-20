using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBuilding : MonoBehaviour
{
    private BuildingScript3D bScript;

    [SerializeField]
    private Transform playerPos = null;
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private bool shooting = false;

    private float shootTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(playerPos);
        bScript = transform.parent.gameObject.GetComponent<BuildingScript3D>();

        bScript.buildingDestroyed.AddListener(StopShooting);
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {
            transform.LookAt(playerPos);

            shootTime -= Time.deltaTime;

            if (shootTime <= 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                shootTime = Random.Range(2f, 5f);
            }
        }
        
    }

    void StopShooting()
    {
        shooting = false;
    }
}
