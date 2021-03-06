using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingStats", menuName = "ScriptableObjects/BuildingStats")]
public class BuildingStats : ScriptableObject
{
    public float health;
    public float score;
    public Sprite buildingSprite;
    public Sprite rubbleSprite;
}
