using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;

    private Vector3 normaliseDirection;

    public GameObject GB;
    public BoxCollider2D GC;

    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-1, 2);
        y = Random.Range(-1, 2);

        normaliseDirection = new Vector3(x - transform.position.x, y - transform.position.y, 0).normalized;

        GB = GameObject.Find("GameBounds");
        GC = GB.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normaliseDirection * Speed * Time.deltaTime;
        if (!GC.bounds.Contains(transform.position))
        {
            x = -x;
            y = -y;
            normaliseDirection = new Vector3(x - transform.position.x, y - transform.position.y, 0).normalized;
        }
        Vector3 moveDirection = (normaliseDirection * Speed * Time.deltaTime );
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
        }

    }
}
