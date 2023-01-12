using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateStart : MonoBehaviour
{
    public SpriteRenderer Self;

    // Start is called before the first frame update
    void Start()
    {
        Self.color = new Color(255, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }
}
