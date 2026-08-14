using UnityEngine;

// Feature: Monsters follow path
// Flow: GetDirectionToPlayer -> MoveTowardPlayer -> Reached player? -> Deal damage, despawn
//
// Attach this alongside Enemy.cs on every enemy prefab. Requires the
// player object to be tagged "Player" and to have a PlayerHealth component.
[RequireComponent(typeof(Enemy))]
public class EnemyFollowPath : MonoBehaviour
{
    private Transform player;
    private Enemy enemyData;
    private PlayerHealth playerHealth;

    [Header("Collision")]
    public float reachDistance = 0.5f; // how close counts as "reached the player"

    void Start()
    {
        enemyData = GetComponent<Enemy>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = GetDirectionToPlayer();
        MoveTowardPlayer(direction);

        if (Vector2.Distance(transform.position, player.position) <= reachDistance)
        {
            DealDamageAndDespawn();
        }
    }

    Vector2 GetDirectionToPlayer()
    {
        return (player.position - transform.position).normalized;
    }

    void MoveTowardPlayer(Vector2 direction)
    {
        transform.position += (Vector3)direction * enemyData.moveSpeed * Time.deltaTime;
    }

    void DealDamageAndDespawn()
    {
        if (playerHealth != null)
        {
            playerHealth.ApplyDamage(enemyData.damageToPlayer);
        }

        Destroy(gameObject);
    }
}
