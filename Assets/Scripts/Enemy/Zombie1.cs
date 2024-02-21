using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie1 : MonoBehaviour
{
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Health")] [SerializeField] private float health = 10f;

    [Header("Sight")] [Tooltip("The range at which the zombie can see in the current direction")] [SerializeField]
    private float sightRange = 20f;

    [SerializeField] private LayerMask sightLayers;

    [Header("Gizmos")] [SerializeField] private bool showGizmos = true;
    [SerializeField] private Color gizmoColor = Color.red;

    [Header("DEBUG")] [SerializeField] private Animator animator;
    [SerializeField] private bool isSeeingPlayer = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float lastXSpeed = 0;

    [Tooltip("You can set the sprite renderer to make the Gizmos show up accordingly in the editor mode")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        bool isDirectedLeft = spriteRenderer != null && spriteRenderer.flipX;
        Vector3 direction = isDirectedLeft ? Vector3.left : Vector3.right;
        Vector3 size = scale;
        size.x = sightRange / 2;
        Vector3 origin = pos + direction * (size.x / 2);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0, direction, 0, sightLayers);
        isSeeingPlayer = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                isSeeingPlayer = true;
                Debug.Log("I see the player");
            }
        }

        if (isSeeingPlayer)
        {
            Debug.Log("Accelerating by " + (Vector2)direction * acceleration * Time.deltaTime);
            //TODO: A turn needs to reset the lastXSpeed
            Vector2 newVelocity = new Vector2(lastXSpeed, rb.velocity.y);
            newVelocity.x += direction.x * acceleration * Time.deltaTime;
            Debug.Log(@"Clamp: ${newVelocity.x} between -${maxSpeed} and ${maxSpeed}");
            newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed);
            rb.velocity = newVelocity;
            lastXSpeed = newVelocity.x;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            lastXSpeed = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        bool isDirectedLeft = spriteRenderer != null && spriteRenderer.flipX;
        Vector3 direction = isDirectedLeft ? Vector3.left : Vector3.right;
        Vector3 size = scale;
        size.x = sightRange / 2;
        Vector3 origin = pos + direction * (size.x / 2);
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(origin, size);
    }
}