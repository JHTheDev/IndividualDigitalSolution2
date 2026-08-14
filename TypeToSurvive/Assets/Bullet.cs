using UnityEngine;

// Fired by TypingCombat when a locked enemy's word is completed.
// Homes in on its target and destroys it (and itself) on arrival.
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float hitDistance = 0.2f;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            // Target was already destroyed by something else - clean up
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= hitDistance)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        if (target != null)
        {
            Destroy(target.gameObject);
        }

        Destroy(gameObject);
    }
}