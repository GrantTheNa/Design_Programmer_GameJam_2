using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Patrick.ItemPlayerCollect
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
                pS.UpdateText();
                Debug.Log("I have eaten");
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("Drink"))
            {
                pS.plyrSpd += spdUp;
                pS.UpdateText();
                Debug.Log("I have drunk");
                Destroy(other.gameObject);
            }
        }
    }

}
