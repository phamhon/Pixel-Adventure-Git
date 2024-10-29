using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapcua : MonoBehaviour
{   
    [SerializeField] int speed =2;
    [SerializeField] Transform a;
    [SerializeField] Transform b;
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
        transform.position = Vector3.Lerp(transform.position, muctieu, speed*Time.deltaTime);
        if(Vector3.Distance(transform.position, muctieu ) < 0.3f){
            if (Vector3.Distance(transform.position, a.position) < 0.3f)
            {
                muctieu = b.position;
            }
            else
            {
                muctieu = a.position;
            }
        }    
        
    }
}
