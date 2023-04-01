using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private GameObject enemyToFollow;

    public float rotSpeed = 250;
    public float damping = 10f;

    public GameObject bullet = null;
    public float shootingClearance = 10f;
    public float cooldown = 1f;

    private float lastShotCooldown = 0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (enemyToFollow == null)
        {
            return;
        }

        var desiredRotQ = Quaternion.LookRotation(enemyToFollow.transform.position);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            desiredRotQ,
            Time.deltaTime * damping
        );

        lastShotCooldown -= Time.deltaTime;
        TryShootAt(enemyToFollow);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemyToFollow = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemyToFollow == other.gameObject)
        {
            enemyToFollow = null;
        }
    }

    private void TryShootAt(GameObject enemy)
    {
        if (enemy == null)
        {
            return;
        }

        var desiredRotQ = Quaternion.LookRotation(enemy.transform.position);
        float angle = Quaternion.Angle(transform.rotation, desiredRotQ);
        if (angle < shootingClearance && lastShotCooldown <= 0)
        {
			ShootAt(enemy);
        }
    }

    private void ShootAt(GameObject enemy)
    {
        var bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstance.GetComponent<Bullet>().target = enemy.transform;
        lastShotCooldown = cooldown;
    }
}
