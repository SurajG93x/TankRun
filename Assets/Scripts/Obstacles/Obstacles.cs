using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public GameObject explosion;
    public int damage = 20;
    [SerializeField] private int val = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            StartCoroutine("DestroyObject");

            collision.gameObject.GetComponent<PlayerHealth>().Damage(damage);
        }

        else if(collision.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            StartCoroutine("DestroyObject");
            GameplayManager.instance.IncreaseScore(val);
        }
    }

    private IEnumerator DestroyObject()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
