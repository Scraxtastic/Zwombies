using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTransformBasedOnSpirteFlip : MonoBehaviour
{
    [SerializeField] private SpriteRenderer watch;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;

    private float startX;
    private float startScaleX;

    // Start is called before the first frame update
    void Start()
    {
        startX = target.localPosition.x;
        startScaleX = target.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        bool flipX = watch.flipX;
        Vector3 scale = target.localScale;
        target.localScale = new Vector3(flipX ? -startScaleX : startScaleX, scale.y, scale.z);
        Vector3 position = target.localPosition;
        target.localPosition.Set(flipX ? -startX : startX, position.y, position.z);
        animator.SetBool("IsLookingRight", !flipX);
    }
}