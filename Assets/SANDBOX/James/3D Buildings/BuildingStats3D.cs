using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStats3D", menuName = "ScriptableObjects/BuildingStats3D")]
public class BuildingStats3D : ScriptableObject
{
    public float health;
    public float score;
    public GameObject brokenModel;
    public GameObject rubbleModel;
}
