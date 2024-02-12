using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class PlayerGroundedHandler : MonoBehaviour
{
    [SerializeField] private Vector2 size = new Vector2(1, 1);
    [SerializeField] private Vector2 direction = new Vector2(0, 1);
    [SerializeField] public float maxDistance = 2;
    [SerializeField] private LayerMask layerMask;
    public event Action OnGrounded;

    protected void InvokeOnGrounded()
    {
        OnGrounded?.Invoke();
    }

    void Update()
    {
        // Cast a ray straight down.
        RaycastHit2D hit =
            Physics2D.BoxCast(transform.position, size, this.transform.eulerAngles.z, direction,
                maxDistance, layerMask);
        // If it hits something...
        if (hit.collider == null)
        {
            return;
        }
        InvokeOnGrounded();
        float distance = Mathf.Sqrt(Mathf.Pow(hit.point.y - transform.position.y, 2) +
                                    Mathf.Pow(hit.point.x - transform.position.x, 2));


        Debug.Log("BoxCast" + " " + this.name + " " + distance);

        Debug.DrawLine(transform.position, hit.point, Color.yellow);
    }
}