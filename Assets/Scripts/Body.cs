using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float mass;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 position;
    public Vector2 mvm;
    public Vector2 force;

    public bool isRandom;

    // Start is called before the first frame update
    void Start()
    {
        if (isRandom)
        {
            mass = Random.Range(0, 10);
            force.x = Random.Range(-100, 100);
            force.y = Random.Range(-100, 100);
            force *= Time.deltaTime;
        }
        else
        {
            mass = 1.0f;
            force = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Player")
            if (Vector2.Distance(position, Vector2.zero) > 1000)
                reset();
        move();
    }

    public void nullifyForce()
    {
        force = Vector2.zero;
    }

    public void stop()
    {
        nullifyForce();
        velocity = Vector2.zero;
    }

    public void reset()
    {
        stop();
        position = Vector2.zero;

        if (isRandom)
        {
            force.x = Random.Range(-100, 100);
            force.y = Random.Range(-100, 100);
            force *= Time.deltaTime;
        }
    }

    private Vector2 computeMovementVector()
    {
        Vector2 v = velocity.normalized;
        v.x = Mathf.Round(v.x);
        v.y = Mathf.Round(v.y);
        return v;
    }

    private void move()
    {
        mvm = computeMovementVector();

        position.x = position.x + velocity.x * Time.deltaTime;
        position.y = position.y + velocity.y * Time.deltaTime;

        acceleration.x = force.x / mass;
        acceleration.y = force.y / mass;

        velocity.x = velocity.x + acceleration.x * Time.deltaTime;
        velocity.y = velocity.y + acceleration.y * Time.deltaTime;

        transform.position = position;
    }
}
