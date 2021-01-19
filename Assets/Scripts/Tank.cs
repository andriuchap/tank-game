using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    Rigidbody rb;
    public ParticleSystem muzzle;
    public AudioSource shot;

    public float movementForce = 10000.0F;
    public float turnForce = 10000.0F;
    public float turretRotationRate = 60.0F;
    public Transform turret;
    public float maxSpeed = 2.0F;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.maxAngularVelocity = 1.0F;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            force += transform.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            force -= transform.forward;
        }

        rb.AddForce(force * movementForce * Time.fixedDeltaTime);
        if(rb.velocity.sqrMagnitude >= maxSpeed*maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }


        Vector3 torque = Vector3.zero;
        if(Input.GetKey(KeyCode.D))
        {
            torque += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            torque -= Vector3.up;
        }

        rb.AddTorque(torque * turnForce * Time.fixedDeltaTime);

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            turret.Rotate(Vector3.up, -turretRotationRate * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            turret.Rotate(Vector3.up, turretRotationRate * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !shot.isPlaying)
        {
            muzzle.Play();
            shot.Play();
        }
    }
}
