using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * TODO:
 * - Add camera offset
 * - Add camera shake
 * - Add camera smoothing
 * - Add camera zoom (e.g. for boss fights)
 */
public class PlayerCamera : MonoBehaviour
{
    [Header("Player to follow")]
    [SerializeField] private GameObject player;

    [Header("Camera Offset")] [SerializeField]
    private float minSpeed = 0.1f;
    [SerializeField] private float maxSpeed = 1f;
    
    [Header("DEBUG")]
    [SerializeField] Vector3 velocity = Vector3.zero;

    [SerializeField] private Rigidbody2D playerRb;  

    private float zOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        zOffset = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPos = player.transform.position;
        transform.position = new Vector3(toPos.x, toPos.y, zOffset);
    }
}
