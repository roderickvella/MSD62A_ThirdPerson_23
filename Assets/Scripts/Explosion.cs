using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce;
    public float explosionRadius;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        //load all nearby colliders
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        //a list of nearby rigidbodies
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach(Collider collider in colliders)
        {
            //check if its got a rigidbody
            if(collider.attachedRigidbody != null &&
                !rigidbodies.Contains(collider.attachedRigidbody))
            {
                rigidbodies.Add(collider.attachedRigidbody);
            }
        }

        //apply force to these rigidbodies
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.AddExplosionForce(explosionForce, transform.position, 
                explosionRadius, 1, ForceMode.Impulse);
        }

        //Destroy the explosion
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
