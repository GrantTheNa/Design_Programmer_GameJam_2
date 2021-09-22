using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBuilding : MonoBehaviour
{
    private BuildingScript3D bScript;

    private Transform playerPos = null;
    [SerializeField]
    private GameObject bulletPrefab = null;

    [SerializeField]
    private bool shooting = false;

    private SpriteRenderer sRender;
    private AudioSource audSource;

    public Sprite s_stand;
    public Sprite s_gun;
    public Sprite s_aim;
    public Sprite s_fire;

    private float shootTime;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<CharacterController>().transform;

        transform.LookAt(playerPos);
        bScript = transform.parent.gameObject.GetComponent<BuildingScript3D>();

        shootTime = Random.Range(2f, 5f);

        sRender = transform.parent.GetComponentInChildren<SpriteRenderer>();
        sRender.sprite = s_stand;

        audSource = GetComponent<AudioSource>();

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
                audSource.Play();
                shootTime = Random.Range(2f, 5f);
            }
            else if (shootTime > 0 && shootTime <= 1)
            {
                sRender.sprite = s_aim;
            }
            else if (shootTime > 1)
            {
                sRender.sprite = s_gun;
            }
        }
        
    }

    void StopShooting()
    {
        shooting = false;
        sRender.enabled = false;
    }
}
