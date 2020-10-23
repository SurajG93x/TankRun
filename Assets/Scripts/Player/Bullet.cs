using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private Rigidbody bullet;

    public void MoveBullet(float speed)
    {
        bullet.AddForce(new Vector3(0,0,1f) * speed);
        StartCoroutine("DestroyAfterTime");
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
        }
    }
}
