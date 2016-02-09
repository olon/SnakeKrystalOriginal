using UnityEngine;

public class Tail : MonoBehaviour {

    public Transform target;
    public float targetDistance;

    public void Update()
    {
        // Direction of the target
        Vector3 direction = target.position - transform.position;

        // Distance to the target
        float distance = direction.magnitude;

        // If the distance to the target's tail longer given
        if (distance > targetDistance)
        {
            // Move the tail
            transform.position += direction.normalized * (distance - targetDistance);
            // Look at the target
            transform.LookAt(target);
        }
    }
}
