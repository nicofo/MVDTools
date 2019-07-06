using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float damageRadius = 1;
    [HideInInspector] new public Rigidbody rigidbody;

    void Reset()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // I want to check if the bullet collided with something.
    void OnTriggerEnter(Collider other)
    {
        // Better to handle this in the player
        // To do it fast, we handle it here
        Debug.Log("hit and destroy");
        Destroy(this.gameObject);
    }
}