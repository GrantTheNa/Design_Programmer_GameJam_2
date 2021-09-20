using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjects : MonoBehaviour
{
    [SerializeField]
    private BuildingStats buildingStats = null;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(buildingStats.health);

        GetComponent<SpriteRenderer>().sprite = buildingStats.sprite;
        Debug.Log(buildingStats.sprite);
    }



}
