using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string name;
    public SpriteRenderer SR;
    public Sprite SheildS;
    public Sprite SwallowS;
    public Sprite SlowS;

    // Start is called before the first frame update
    void Start()
    {
        int RNG = Random.Range(1, 4);
        if(RNG == 1)
        {
            name = "Sheild";
            SR.sprite = SheildS;
        }
        if(RNG == 2)
        {
            name = "Slow";
            SR.sprite = SlowS;
        }
        if(RNG == 3)
        {
            name = "Swallow";
            SR.sprite = SwallowS;
        }
        if(RNG == 4)
        {
            name = "Sheild";
            SR.sprite = SheildS;
        }
    }
}
