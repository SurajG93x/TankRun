using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform target;
    public float camDistance = 6.3f;

    public float camHeight = 1.5f;
    public float height_damping = 3.25f;
    public float rotation_damping = 0.27f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float wanted_rotation_angle = target.eulerAngles.y;
        float wanted_height = target.position.y + camHeight;

        float currentRotationAngle = transform.eulerAngles.y;
        float current_Height = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wanted_rotation_angle, rotation_damping * Time.deltaTime);
        current_Height = Mathf.Lerp(current_Height, wanted_height, height_damping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * camDistance;

        transform.position = new Vector3(transform.position.x, current_Height, transform.position.z);
    }
}
