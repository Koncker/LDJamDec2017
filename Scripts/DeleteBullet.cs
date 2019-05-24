using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullet : MonoBehaviour {

    private void OnTriggerExit(Collider collision)
    {
        Debug.Break();
        Debug.Log("Bullet Left Boundary");
        Object.Destroy(collision.gameObject);
    }
}
