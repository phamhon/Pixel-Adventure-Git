using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapcua1 : MonoBehaviour
{
    [SerializeField] int speed = 2;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
    [SerializeField] Transform c;
    [SerializeField] Transform d;
    Vector3 muctieu;
    // Start is called before the first frame update
    void Start()
    {
        muctieu = a.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move()
    {
        transform.position = Vector3.Lerp(transform.position, muctieu, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, muctieu) < 0.1f)
        {
            if (Vector3.Distance(transform.position, a.position) < 0.1f)
            {
                muctieu = b.position;
            }
            else if (Vector3.Distance(transform.position,   b.position) < 0.1f)
            {
                muctieu = c.position;
            }
            else if (Vector3.Distance(transform.position, c.position) < 0.1f)
            {
                muctieu = d.position;
            }
            else 
            {
                muctieu = a.position;
            }
        }

    }
}
