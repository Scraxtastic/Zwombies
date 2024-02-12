using UnityEngine;

public class BaseOffset : MonoBehaviour
{
    [SerializeField] private SpriteRenderer watch;
    [Header("DEBUG")] [SerializeField] private float xOffset;

    void Start()
    {
        xOffset = transform.localPosition.x;
    }

    void Update()
    {
        // IntelliJ says it's more efficient 
        Transform t = this.transform;
        if (watch.flipX)
        {
            t.localPosition = new Vector3(-xOffset, transform.localPosition.y, t.localPosition.z);
        }
        else
        {
            t.localPosition = new Vector3(xOffset, transform.localPosition.y, t.localPosition.z);
        }
    }
}