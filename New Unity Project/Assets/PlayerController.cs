using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public Collider2D gameBounds;

    public Vector2 target;
    public float Speed;
    public bool Move;

    void Start()
    {
        GameObject GameBounds = GameObject.Find("GameBounds");
        gameBounds = GameBounds.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (gameBounds.bounds.Contains(target))
        {
            Move = true;
        }
        else
        {
            Move = false;
        }
        if (Input.GetMouseButton(0) && Move == true)
        {
            
            Player.transform.position = Vector2.MoveTowards(Player.transform.position, new Vector2(target.x, target.y), Speed * Time.deltaTime);
        }
    }

}
