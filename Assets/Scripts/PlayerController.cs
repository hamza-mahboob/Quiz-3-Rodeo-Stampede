using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    GameObject currentAnimal;
    GameManager gameManager;
    LineRenderer lineRenderer;
    public LayerMask CollidableLayers;


    float xBound = 23;
    float speed = 70;
    float forceUp = 25;
    bool isGrounded = false;
    bool isOnAnimal = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lineRenderer = GetComponent<LineRenderer>();
        Physics.gravity = new Vector3(0, -25.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //projectile prediction
        lineRenderer.positionCount = 50;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = transform.position;
        Vector3 startingVelocity = transform.up * 10;
        for (float t = 0; t < 50; t += 0.1f)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            if (Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
            {
                lineRenderer.positionCount = points.Count;
                break;
            }
        }
        lineRenderer.SetPositions(points.ToArray());

        //move left & right
        rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * speed);

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (!isOnAnimal)
            {
                Debug.Log("Not on animal");
                isGrounded = false;
                rb.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("On animal jump");
                isOnAnimal = false;
                isGrounded = false;
                currentAnimal.GetComponent<MoveBack>().enabled = true;
                currentAnimal.transform.DetachChildren();
                rb.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
            }
        }

        //keep player in boundaries
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
            rb.velocity = Vector3.zero;
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Animal"))
        {
            if (isOnAnimal)
            {
                gameManager.GameOver();
            }
            gameManager.AddScore();
            isOnAnimal = true;
            currentAnimal = collision.gameObject;

            Vector3 offset = new Vector3(0, collision.gameObject.GetComponent<Collider>().bounds.size.y, 0);
            transform.position = collision.transform.position + offset;
            collision.gameObject.GetComponent<MoveBack>().enabled = false;
            collision.gameObject.transform.SetParent(transform);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
    }
}
