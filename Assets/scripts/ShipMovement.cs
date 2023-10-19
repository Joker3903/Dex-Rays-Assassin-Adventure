using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float rotSpeed = 6f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

   public void Move(float zAxis, float yAxis) {
        rb.AddRelativeForce(0, 0, zAxis * speed);
        rb.AddTorque(0, yAxis * rotSpeed, 0);
    }
}
