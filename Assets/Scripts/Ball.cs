using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Body
{
    public override bool isIntersectsWith(GameObject other)
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

    private void bounceOff(Ball other)
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
                if (other.GetComponent<Ball>())
                {
                    Ball otherBall = other.GetComponent<Ball>();
                    while (isIntersectsWith(other))
                    {
                        if (this.position == otherBall.position)
                        {
                            this.reset();
                            otherBall.reset();
                        }
                        this.position -= this.velocity * Time.deltaTime * 4.25f;
                        otherBall.position -= otherBall.velocity * Time.deltaTime * 4.25f;
                    }
                    bounceOff(otherBall);
                    this.nullifyForce();
                    otherBall.nullifyForce();
                }
            }
        }
    }

    public override void update()
    {
        others = GameObject.FindGameObjectsWithTag("Player");
        if (gameObject.tag != "Player")
            if (Vector2.Distance(position, Vector2.zero) > 10000)
                reset();
        lookForCollisionAndBounceOff();
        base.update();
    }
}
