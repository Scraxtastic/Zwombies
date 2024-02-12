using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Respawns the object, if it falls to far
 */
public class RespawnIfFallsUnder : MonoBehaviour
{
    [SerializeField] private float smallestY = -100f;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < smallestY)
        {
            transform.position = startPosition;
        }
    }
}
