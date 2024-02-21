using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer watch;
    [SerializeField] private float bulletForce = 1f;

    // Input variables
    private PlayerController playerControls;

    private PlayerController.InteractionActions interactions;

    private void OnEnable()
    {
        playerControls = new PlayerController();
        playerControls.Enable();
        interactions = playerControls.Interaction;
        interactions.Enable();
        interactions.Attack.performed += _ => ShootBullet();
    }

    private void OnDisable()
    {
        interactions.Disable();
        playerControls.Disable();
    }

    private void ShootBullet()
    {
        Bullet bullet = GameManager.Instance.GetBullet();
        Vector2 direction = watch.flipX ? Vector2.left : Vector2.right;
        bullet.Fire(transform.position, direction, bulletForce);
    }
}