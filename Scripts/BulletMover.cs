using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        StartCoroutine(BulletDeath());
    }

    IEnumerator BulletDeath()
    {
        yield return new WaitForSeconds(2f);
        Object.Destroy(this.gameObject);
    }
}