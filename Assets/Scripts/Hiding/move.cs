using UnityEngine;

public class move : MonoBehaviour
{
   
    public float movementX;
    public float movementY;
    public float vlcdd;
    public Rigidbody2D rgby2d;
    public Vector2 vt2;
   

    void Start()
    {
        rgby2d = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {

        movementX = Input.GetAxisRaw("Horizontal");
        //movementY = Input.GetAxisRaw("Vertical");
        vt2 = new Vector2(movementX, 0).normalized;
        rgby2d.linearVelocity = vt2 * vlcdd;

       
    }
}
