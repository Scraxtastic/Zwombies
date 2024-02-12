using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask tileMapLayer;
    [Header("DEBUG")] [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void ReturnToBulletPool()
    {
        GameManager.Instance.ReturnBulletToPool(this);
    }

    public void Fire(Vector2 start, Vector2 dir, float speed)
    {
        transform.position = start;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Tilemap"))
        {
            HandleTilemapCollision();
        }
    }

    private void HandleTilemapCollision()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, transform.localScale.x * 0.5f,
            rb.velocity.normalized, 0.5f, tileMapLayer);
        if (hit.collider == null)
        {
            return;
        }
        Vector3 hitPoint = hit.point;
        Vector2 dir2 = hitPoint - transform.position;
        Vector2 offsetHitpoint = hit.point + dir2 * 0.2f;
        Tilemap tilemap = GameManager.Instance.GetTilemap();
        Vector3Int cellPosition = tilemap.WorldToCell(offsetHitpoint);
        TileBase hitTile = tilemap.GetTile(cellPosition);
        if (hitTile != null)
        {
            tilemap.SetTile(cellPosition, null); // Remove the tile
            ReturnToBulletPool();
        }
    }
}