using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public double health;
    [SerializeField] public double maxHealth;
    [SerializeField] public double attackDamage;
    [SerializeField] public double defense;
    
    [Header("Money")]
    [SerializeField] public double money;
    
    [Header("Craftables")]
    [SerializeField] public double glibber;
    

}
