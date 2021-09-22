using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.James.Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private float bulletSpeed = 25f;

        private PlayerStats playerStats;

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = MASTER_GameManager.Instance.player;
            playerStats = player.GetComponent<PlayerStats>();
            
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            Invoke("DestroySelf", 5.0f);
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // <-- ADD PLAYER HEALTH REFERENCE HERE
            }

            DestroySelf();
        }

        void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}

