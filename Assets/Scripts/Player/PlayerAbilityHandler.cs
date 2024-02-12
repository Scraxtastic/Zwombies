using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour
{
    [SerializeField] public bool unlockJump = false;
    [SerializeField] public bool unlockDash = false;
    [SerializeField] public bool unlockDoubleJump = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (unlockJump)
        {
            other.gameObject.GetComponent<PlayerMovement>().hasJump = true;
        }

        if (unlockDash)
        {
            other.gameObject.GetComponent<PlayerMovement>().hasDash = true;
        }

        if (unlockDoubleJump)
        {
            other.gameObject.GetComponent<PlayerMovement>().hasDoubleJump = true;
        }
    }
}