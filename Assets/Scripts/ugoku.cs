using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ugoku : MonoBehaviour
{
    Rigidbody rb;
    public float hayasa = 1;
    public bool zimen = true;
    public float janpupower = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.forward*y + transform.right*x;
        move.y = 0;
        move.Normalize();
        move *= hayasa;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
        if(Input.GetKey(KeyCode.Space) && zimen)
        {
            rb.AddForce(transform.up*janpupower, ForceMode.Impulse);
            zimen = false;
        }
    }
    private void OnCollisionEnter(Collision cori)
    {
        zimen = true;
    }
}
