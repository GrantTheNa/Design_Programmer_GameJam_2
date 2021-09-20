using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpawningItems
{
    public class SpawnItems : MonoBehaviour
    {
        //VARS
        public Vector3 center;
        public Vector3 size;
        public GameObject preFood;
        public GameObject preDrink;

        // Start is called before the first frame update
        void Start()
        {
            SpawnFood();
            SpawnDrink();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                SpawnFood();
                SpawnDrink();
            }
        }

        public void SpawnFood()
        {
            Vector3 pos = transform.localPosition + new Vector3(Random.Range(-size.x / 2, size.x / 2), 1 , Random.Range(-size.z / 2, size.z / 2));
            Instantiate(preFood, pos, preFood.transform.rotation);
        }

        public void SpawnDrink()
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), 1 , Random.Range(-size.z / 2, size.z / 2));
            Instantiate(preDrink, pos, preDrink.transform.rotation);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.localPosition, size);
        }
    }

}
