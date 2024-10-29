using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rigi;
    [SerializeField] int speed =2;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    Vector3 muctieu;
    public Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        muctieu = a.position;
        rigi = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
        
        rigi.velocity = new Vector2(speed, 0);
        if (Vector3.Distance(transform.position, muctieu) < 0.5f)
        {   

            if (Vector3.Distance(transform.position, a.position) < 0.5f)
            {
                muctieu = b.position;
                speed =-speed;
            }
            else
            {
                speed = -speed;
                muctieu = a.position;  
            }
        }
        if (rigi.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rigi.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
    }
}
