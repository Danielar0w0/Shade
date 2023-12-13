using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public float distance = 10f;
    public float speed = 0.1f;

    private Vector3 startPos;
    private Vector3 endPos;

    public bool moveRight = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(distance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object
        if (moveRight)
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1.0f));
        else
            transform.position = Vector3.Lerp(endPos, startPos, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
