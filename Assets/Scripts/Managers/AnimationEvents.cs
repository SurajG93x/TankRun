using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    private PlayerController pc;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    public void ResetTurret()
    {
        pc.canShoot = true;
        anim.Play("Idle");
    }
}
