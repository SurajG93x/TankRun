using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public GameObject bloodFX;
    public float speed;

    private Rigidbody mybody;

    // Start is called before the first frame update
    void Start()
    {
        mybody = GetComponent<Rigidbody>();
        mybody.velocity = new Vector3(0f,0f,-speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        mybody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Animator>().Play("Idle");
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.2f);
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    private IEnumerator Destroyem()
    {
        Die();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFX, transform.position, Quaternion.identity);
            StartCoroutine("Destroyem");

        }
    }
}
