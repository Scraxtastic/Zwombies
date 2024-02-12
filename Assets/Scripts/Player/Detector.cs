using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Detector : MonoBehaviour
{
    [SerializeField] private LayerMask signLayer;
    [SerializeField] private LayerMask droppedLayer;
    [SerializeField] private LayerMask interactableLayer;

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     GameObject otherGameObject = other.gameObject;
    //     SetSignActive(otherGameObject, true);
    //     StartItemCollection(otherGameObject);
    //     SetInteractableActive(otherGameObject, true);
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     GameObject otherGameObject = other.gameObject;
    //     SetSignActive(otherGameObject, false);
    //     SetInteractableActive(otherGameObject, false);
    // }

    // private void SetSignActive(GameObject otherGameObject, bool state)
    // {
    //     if (signLayer == (signLayer | (1 << otherGameObject.layer)))
    //     {
    //         SignText signText = otherGameObject.GetComponent<SignText>();
    //         if (signText == null)
    //         {
    //             return;
    //         }
    //
    //         signText.textMesh.enabled = state;
    //     }
    // }
    //
    // private void StartItemCollection(GameObject otherGameObject)
    // {
    //     if (droppedLayer == (droppedLayer | (1 << otherGameObject.layer)))
    //     {
    //         Debug.Log("Detector - Entered Trigger" + otherGameObject.name);
    //         if (otherGameObject.CompareTag("Coin"))
    //         {
    //             Coin coin = otherGameObject.GetComponent<Coin>();
    //             coin.StartCollecting(transform);
    //         }
    //
    //         if (otherGameObject.CompareTag("DroppedItem"))
    //         {
    //             DroppedItem droppedItem = otherGameObject.GetComponent<DroppedItem>();
    //             droppedItem.StartCollecting(transform);
    //         }
    //     }
    // }

    // private void SetInteractableActive(GameObject otherGameObject, bool state)
    // {
    //     if (interactableLayer == (interactableLayer | (1 << otherGameObject.layer)))
    //     {
    //         Interactable interactable = otherGameObject.GetComponent<Interactable>();
    //         if (interactable == null)
    //         {
    //             return;
    //         }
    //     
    //         interactable.OnPlayerTriggerChange(state);
    //     }
    // }
}