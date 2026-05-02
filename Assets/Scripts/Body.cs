using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] public float mass;
    [SerializeField] public Vector2 velocity;
    [SerializeField] public Vector2 acceleration;
    [SerializeField] public Vector2 position;
    [SerializeField] public Vector2 mvm;
    [SerializeField] public Vector2 force;

    [SerializeField] public bool isRandom;
    [SerializeField] public GameObject[] others;

    // Start is called before the first frame update
    void Start()
    {
        init();
        Application.targetFrameRate = 128;
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

    public virtual bool isIntersectsWith(GameObject other)
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

    protected void move()
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

    public virtual void update()
    {
        move();
    }
}
