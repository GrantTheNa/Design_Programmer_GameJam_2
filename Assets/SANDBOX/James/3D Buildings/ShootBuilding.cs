using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBuilding : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private GameObject bulletPrefab;

    private float shootTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(playerPos);
    }

    // Update is called once per frame
    void Update()
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
