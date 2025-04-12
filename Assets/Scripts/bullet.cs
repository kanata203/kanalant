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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "sute-zi")
        {

            Destroy(this.gameObject);


        }
        if (collision.gameObject.tag == "enemy")
        {
            
           
        }
        if (collision.gameObject.tag == "player")
        {
            playerhp playerhp = collision.gameObject.GetComponent<playerhp>();
            if (playerhp != null)
            {
                playerhp.takedamege(1);
                
            }
        }
        Destroy(this.gameObject);
    }
}
