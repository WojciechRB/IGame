using UnityEngine;

public class Spring : Body
{
    [SerializeField] public float k;
    [SerializeField] public float damping;

    public override void update()
    {
        others = GameObject.FindGameObjectsWithTag("Player");
        if (gameObject.tag != "Player")
            if (Vector2.Distance(position, Vector2.zero) > 10000)
                reset();
        k = Mathf.Clamp(k, 0, 10);
        damping = Mathf.Clamp(damping, 0, 10);
        force = -k * position - damping * velocity;
        base.update();
    }
}
