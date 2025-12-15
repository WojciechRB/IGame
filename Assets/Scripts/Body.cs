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
    public GameObject[] others;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        update();
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
            position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }

    public bool isIntersectsWith(GameObject other)
    {
        if (other.GetComponent<Body>())
        {
            Body body = other.GetComponent<Body>();
            Vector2 origin = this.position - body.position;
            float distance = Mathf.Sqrt(Mathf.Pow(origin.x, 2) + Mathf.Pow(origin.y, 2));
            return (distance < 0.5f + 0.5f);
        }
        else
            return false;
    }

    private void init()
    {
        others = GameObject.FindGameObjectsWithTag("Player");
        if (isRandom)
        {
            mass = Random.Range(0, 10);
            force.x = Random.Range(-100, 100);
            force.y = Random.Range(-100, 100);
            force *= Time.deltaTime;
            position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
        else
        {
            mass = 1.0f;
            force = Vector2.zero;
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

    private void bounceOff(Body other)
    {
        Vector2 v = this.velocity;
        Vector2 w = other.velocity;

        float x = this.mass + other.mass;
        float y = Mathf.Pow((this.position + other.position).magnitude, 2);


        v -= (2 * other.mass / x) * (Vector2.Dot(this.velocity - other.velocity, this.position - other.position) / y) * (this.position - other.position);
        w -= (2 * this.mass / x) * (Vector2.Dot(other.velocity - this.velocity, other.position - this.position) / y) * (other.position - this.position);

        this.velocity = v;
        other.velocity = w;

        this.move();
        other.move();
    }

    private void lookForCollisionAndBounceOff()
    {
        foreach (GameObject other in others)
        {
            if (other == gameObject)
                continue;
            else if (isIntersectsWith(other))
            {
                if (other.GetComponent<Body>())
                {
                    Body otherBody = other.GetComponent<Body>();
                    while (isIntersectsWith(other))
                    {
                        if(this.position == otherBody.position)
                        {
                            this.reset();
                            otherBody.reset();
                        }
                        this.position -= this.velocity * Time.deltaTime * 0.001f;
                        otherBody.position -= otherBody.velocity * Time.deltaTime * 0.001f;
                    }
                    bounceOff(otherBody);
                }
            }
        }
    }

    private void update()
    {
        others = GameObject.FindGameObjectsWithTag("Player");
        if (gameObject.tag != "Player")
            if (Vector2.Distance(position, Vector2.zero) > 10000)
                reset();
        lookForCollisionAndBounceOff();
        move();
    }
}
