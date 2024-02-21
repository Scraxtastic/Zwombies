using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float angleDeviation = 45;
    void Update()
    {
        Vector2 targetPos = target.transform.position;
        Vector2 direction = targetPos - (Vector2) transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + angleDeviation);
        
    }
}