using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muki : MonoBehaviour
{
    Vector3 movepos;
    public float speed = 1;
    public GameObject came;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        //Debug.Log("ugoita" + x);
        //Debug.Log("ugoita" + y);
        transform.rotation *= Quaternion.Euler(0, x * speed, 0);
        came.transform.rotation *= Quaternion.Euler(-y * speed, 0, 0);
    }
}
