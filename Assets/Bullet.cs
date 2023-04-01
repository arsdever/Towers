using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target = null;
    public float velocity = 1f;
    public float damage = 10f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Explode();
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            velocity * Time.deltaTime
        );
    }

    void Explode()
    {
        if (target != null && target.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Explode();
        }
    }
}
