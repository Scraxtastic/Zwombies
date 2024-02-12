using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // [SerializeField] private PlayerStats playerStats;
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     GameObject otherGameObject = other.gameObject;
    //     CollectCoin(otherGameObject);
    //     CollectItem(otherGameObject);
    // }
    //
    // private void CollectCoin(GameObject otherGameObject)
    // {
    //     if (!otherGameObject.CompareTag("Coin"))
    //     {
    //         return;
    //     }
    //
    //     Coin coin = otherGameObject.GetComponent<Coin>();
    //     playerStats.money += coin.coinValue;
    //     Destroy(otherGameObject);
    // }
    //
    // private void CollectItem(GameObject otherGameObject)
    // {
    //     if (!otherGameObject.CompareTag("DroppedItem"))
    //     {
    //         return;
    //     }
    //
    //     DroppedItem droppedItem = otherGameObject.GetComponent<DroppedItem>();
    //     switch (droppedItem.type.ToLower())
    //     {
    //         case "glibber":
    //             playerStats.glibber += droppedItem.amount;
    //             break;
    //     }
    //
    //     Destroy(otherGameObject);
    // }
}