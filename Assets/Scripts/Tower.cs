using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Towers { Normal, AOE, Buff, Resources}

[CreateAssetMenu(fileName = "newTower", menuName = "Towers/Create New Tower")]
public class Tower : ScriptableObject
{
    public Towers TowerTypes;
    public float shootRate = 0.2f;
    public float moveSpeed = 4f;
    public string enemyTag = "Enemy";
}
