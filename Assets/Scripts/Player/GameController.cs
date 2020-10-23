using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Vector3 speed;

    public float forwardspeed = 15f, turnSpeed = 8f;
    public float acc = 15f, decc = 10f;

    protected float rotationSpeed = 10f, maxAngle = 10f;
    protected float lowSound, normalSound, highSound;
    public AudioClip engineOn, engineOff;

    private bool isSlow;

    private AudioSource SoundManager;

    // Start is called before the first frame update
    void Awake()
    {
        speed = new Vector3(0f, 0f, forwardspeed);
        SoundManager = GetComponent<AudioSource>();
    }

    protected void MoveLeft()
    {
        speed = new Vector3(-turnSpeed / 2f, 0, speed.z);
    }

    protected void MoveRight()
    {
        speed = new Vector3(turnSpeed / 2f, 0, speed.z);
    }

    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0, speed.z);
    }

    protected void MoveNormal()
    {
        if (isSlow)
        {
            isSlow = false;
            SoundManager.Stop();
            SoundManager.clip = engineOn;
            SoundManager.volume = 0.3f;
            SoundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, forwardspeed);
    }

    protected void MoveSlow()
    {
        if (!isSlow)
        {
            isSlow = true;

            SoundManager.Stop();
            SoundManager.clip = engineOff;
            SoundManager.volume = 0.5f;
            SoundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, decc);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, acc);
    }
}
