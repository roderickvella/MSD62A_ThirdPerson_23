using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float initialForce;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //add the initial force to the rigidbody (attached to this grenade)
        GetComponent<Rigidbody>().AddRelativeForce(0, 0, initialForce);

        StartCoroutine(ExplodeAfterTime(1.0f));

    }

    IEnumerator ExplodeAfterTime(float time)
    {
        //wait for the time
        yield return new WaitForSeconds(time);

        Explode();
    }

    private void Explode()
    {
        print("explode!!");
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
