using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    float power;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * power);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
   
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sute-zi")
        {
            
            Destroy(this.gameObject);

        }
    }
}
