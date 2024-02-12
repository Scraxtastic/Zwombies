using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindTo : MonoBehaviour
{
    [SerializeField] private GameObject toFollow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 toPos = toFollow.transform.position;
        this.transform.position = new Vector3(toPos.x, toPos.y, transform.position.z);
    }
}
