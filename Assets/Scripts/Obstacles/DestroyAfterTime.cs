using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float timer = 3f;

    void Start()
    {
        Invoke("DeactivateGO", timer);
    }

    void DeactivateGO()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
