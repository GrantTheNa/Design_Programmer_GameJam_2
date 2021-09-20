using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpawningItems
{
    public class ItemCollection : MonoBehaviour
    {
        //VARS
        public float dmgUp = 10;
        public float spdUp = 10;

        //REFS
        public PlayerStats pS;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Food"))
            {
                pS.plyrDmg += dmgUp;
                pS.UpdateDmg();
                Debug.Log("I have eaten");
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Drink"))
            {
                pS.plyrSpd += spdUp;
                pS.UpdateSpd();
                Debug.Log("I have drunk");
                Destroy(other.gameObject);
            }
        }
    }
}
