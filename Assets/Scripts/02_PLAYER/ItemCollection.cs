using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            pS.plyrGrth += 1;
            pS.plyrDmg += dmgUp;
            pS.GrowCheck();
            pS.UpdateDmg();
            Debug.Log("I have eaten");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Drink"))
        {
            pS.plyrGrth += 1;
            pS.plyrSpd += spdUp;
            pS.GrowCheck();
            pS.UpdateSpd();
            Debug.Log("I have drunk");
            Destroy(other.gameObject);
        }
    }
}
