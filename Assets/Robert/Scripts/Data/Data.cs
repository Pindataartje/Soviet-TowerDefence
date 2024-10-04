using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data : MonoBehaviour
{
    [Header("Stats")]
    public int cost;
    public float damage;
    public float fireSpeed;
    public float radius;
    [Space]
    public bool isBarbedWire;
    public float speedDecrease;
}
