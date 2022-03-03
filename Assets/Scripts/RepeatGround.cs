using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatGround : MonoBehaviour
{
    Collider col;
    Vector3 startingPositon;
    float speed = 25;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        startingPositon = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z < startingPositon.z - col.bounds.size.z)
        {
            transform.position = startingPositon;
        }
    }
}
