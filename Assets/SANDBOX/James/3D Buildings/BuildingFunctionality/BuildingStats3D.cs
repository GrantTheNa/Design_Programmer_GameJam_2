using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStats3D", menuName = "ScriptableObjects/BuildingStats3D")] // CREATE More Of These in the 'Create' Menu
public class BuildingStats3D : ScriptableObject // This Script holds data concerning Buildings
{
    public float health; // Building health
    public float score; // How many points a building is worth
    public GameObject brokenModel; // A Prefab of a broken building
    public GameObject rubbleModel; // A Prefab of a collapsed building
    public AudioClip brokenSound; // A Sound for when the building breaks
    public AudioClip rubbleSound; // A Sound for when the building collapses
}
