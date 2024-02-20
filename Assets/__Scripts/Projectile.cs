using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float explosionRadius = 5;
    public float explosionForce = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var surraundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(var obj in surraundingObjects)
            {
                var rb = obj.GetComponent<Rigidbody>();
                if (rb != null) continue;

                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            Destroy(this.gameObject);
        }
        
    }
}
