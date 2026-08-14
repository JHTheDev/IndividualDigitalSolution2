using UnityEngine;

// Feature: Player aim lock
// Flow: Player types a key -> FindMatchingEnemy -> Match found? -> LockAim
//       -> Word complete? (No, keep locked) -> Fire, unlock aim
//
// Owns which single enemy is currently targeted. TypingCombat reads
// LockedEnemy from this script and calls LockAim / UnlockAim as the
// player types.
public class PlayerAimLock : MonoBehaviour
{
    public Enemy LockedEnemy { get; private set; }

    // Searches all active enemies for one whose next expected letter
    // matches the typed character, and locks onto the nearest match.
    public Enemy FindMatchingEnemy(char typedChar)
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy nearest = null;
        float nearestDistSqr = float.MaxValue;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.typedProgress != 0) continue; // already locked by/targeting elsewhere
            if (enemy.NextExpectedChar() != typedChar) continue;

            float distSqr = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distSqr < nearestDistSqr)
            {
                nearestDistSqr = distSqr;
                nearest = enemy;
            }
        }

        return nearest;
    }

    public void LockAim(Enemy enemy)
    {
        LockedEnemy = enemy;
        // TODO: snap an aim reticle / UI indicator onto enemy.transform
    }

    public void UnlockAim()
    {
        LockedEnemy = null;
        // TODO: clear the aim reticle
    }
}