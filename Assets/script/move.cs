using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class move : MonoBehaviour
{   float x =0 ,y =0;
    [SerializeField] float speed=5 ;
    //[SerializeField] float jumbpower = 600;
    [SerializeField] Transform camera;
    
    Vector2 moves = new Vector2();
    private Rigidbody2D rb;
    bool trap=false;
    bool doublejumb ;


    Vector3 camera1 = Vector3.zero;
    Vector3 start;
    public Animator animation;



    public Transform wallcheck;
    //public LayerMask walllayer;
    bool iswall;
    bool canwallslide;
    float wallslidespeed;

    float walljumpdirection;

    public Transform groundcheck;
    public LayerMask groundlayer;
    bool ground;

    private bool iswalljumpping;
    private float walljumpingtime = 0.2f;
    private float walljumpingcoute;
    private float walljumpingduration=0.4f;
    private Vector2 walljumpingpower = new Vector2(8f,16f);

    void Start()
    {   
        start= transform.position;
        rb= GetComponent<Rigidbody2D>();
    

}

// Update is called once per frame
void Update()
    {   
        camera1 = transform.position;
        camera1.z = -10;
        camera1.y += 1.5f ;
        camera.position = camera1;
        dichuyen();
        jumb();
        daptrap();
        walljump();

        animation.SetFloat("move",Mathf.Abs(x));
        animation.SetFloat("yvelocity",rb.velocity.y);
        checkground();
        
        if(checkwall()&& ground == false && x!=0)
        {
            canwallslide = true;
            animation.SetBool("slidewall",true);
        }
        else
        {
            animation.SetBool("slidewall", false);
            canwallslide = false;
        }
        if (canwallslide)
        {
            rb.velocity = new Vector2 (rb.velocity.x,Mathf.Clamp(rb.velocity.y,-wallslidespeed,float.MaxValue));
        }


    }
    private void checkground()
    {
        if (Physics2D.OverlapCircle(groundcheck.position, 0.4f, groundlayer))
        {
            
            ground = true;
        }
        else 
        {
            
            ground = false;
        }
        animation.SetBool("isground", ground);
    }
    private bool checkwall()
    {
        return iswall= Physics2D.OverlapCircle(wallcheck.position,0.2f,groundlayer);
    }
    
    private void wallslide()
    {
        if (!ground && checkwall())
        {
            canwallslide = true;
            rb.velocity = new Vector2(rb.velocity.x ,-1);
        }
        else {
            canwallslide = false;

        }
    }
    private void walljump()
    {
        if (canwallslide)
        {
            iswalljumpping = false;
            walljumpdirection = -transform.localScale.x;
            walljumpingcoute = walljumpingtime;
            CancelInvoke(nameof(stopwalljumping));
        }
        else { 
            walljumpingcoute -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space)&& walljumpingcoute > 0f)
        {
            iswalljumpping= true;
            rb.velocity = new Vector2 (walljumpdirection* walljumpingpower.x ,walljumpingpower.y);
            walljumpingcoute = 0f;
            //if(transform.localRotation.x != walljumpdirection)
            //{

            //}
            Invoke(nameof(stopwalljumping), walljumpingduration);
        }
    }
    private void stopwalljumping()
    {
        iswalljumpping = false ;
    }
   
    private void dichuyen(){
        
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            moves = new Vector2(x, y);

           
            rb.velocity = new Vector2(moves.x * speed, rb.velocity.y);
            
          
            if (rb.velocity.x < -0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                walljumpdirection = 1;
        }
            if (rb.velocity.x > 0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                walljumpdirection = -1;
        }

    }

    private void jumb()
    {

        if (ground && !Input.GetKey(KeyCode.Space) )
        {
            doublejumb = false;
            animation.SetBool("isdoublejumb", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!checkwall())
            {
                if (ground || doublejumb)
                {
                    if (doublejumb)
                    {
                        animation.SetBool("isdoublejumb", true);
                    }

                    if (doublejumb == false)
                    {
                        animation.SetBool("isdoublejumb", false);
                    }
                    rb.AddForce(new Vector2(0, 550));
                    doublejumb = !doublejumb;


                }
            }
        }    
       
    }
    private void walljumb()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!ground && checkwall())
            {
                rb.AddForce(new Vector2(walljumpdirection * 100f, 20f));
            }
        }
        }
        private void daptrap() {

        
        if (trap)
        {
            //SceneManager.LoadScene("save sence");
            transform.position = start;
        }
        if (transform.position.y < -6f)
        {
           // SceneManager.LoadScene("save sence");
            transform.position = start;
        }
    }
    private void FixedUpdate()
    {
        
    }
}
//private void OnCollisionEnter2D(Collision2D collision)
//{   if (collision.gameObject.CompareTag("grond")){
//        ground = true;
//        animation.SetBool("isground",ground);
//    }
//    if (collision.gameObject.CompareTag("trap"))
//    {
//        trap = true;
//    }
//}
//private void OnCollisionExit2D(Collision2D collision)
//{
//    if (collision.gameObject.CompareTag("grond")){
//        ground = false;
//        animation.SetBool("isground", ground);
//    }
//    if (collision.gameObject.CompareTag("trap"))
//    {
//        trap = false;
//    }
//}