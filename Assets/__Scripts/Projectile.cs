using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionRadius = 10;
    public float explosionForce = 1000;
    public Collider[] colider = new Collider[20];
    public GameObject particles;

    public GameObject src;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Explode();
        }
    }

    public void Explode()
    {
        int numCollider = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, colider);
        if (numCollider > 0) 
        {
            for(int i = 0; i < numCollider; i++) 
            {
                if (colider[i].TryGetComponent(out Rigidbody rb))
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }
        GameObject boomClone = (GameObject)Instantiate(src, transform.position, Quaternion.identity);
        AudioSource aud = boomClone.GetComponent<AudioSource>();
        aud.Play();
        Destroy(boomClone, 2.0f);
        Destroy(gameObject);
        GameObject cloneParticle = (GameObject)Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(cloneParticle, 1.0f);
    }
}
