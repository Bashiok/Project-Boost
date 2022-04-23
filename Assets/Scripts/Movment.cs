using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rocketRotation = 100f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       ProcessThrust();
       ProcessRotation();
    }
    void ProcessThrust()
    {
         if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))
         {
             rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
             
         }
    }
    void ProcessRotation()
    {
         if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            ApplyRotation(rocketRotation);
        }

        else if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)))
         {
             ApplyRotation(-rocketRotation);
         }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rottate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // un freezing so the physic system can take over
    }
}
