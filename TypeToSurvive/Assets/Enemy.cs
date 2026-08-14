using UnityEngine;

// Shared data holder for a single enemy. The other four scripts read and
// write these fields, so this component must be present on every enemy
// prefab alongside EnemyFollowPath.
[RequireComponent(typeof(EnemyFollowPath))]
public class Enemy : MonoBehaviour
{
    [Header("Typing target")]
    public string word = "type";      // word this enemy is destroyed by
    public int typedProgress = 0;     // how many letters typed correctly so far

    [Header("Combat")]
    public int damageToPlayer = 10;   // damage dealt if this enemy reaches the player

    [Header("Movement")]
    public float moveSpeed = 2f;

    // Convenience helpers used by TypingCombat / PlayerAimLock
    public bool IsWordComplete => typedProgress >= word.Length;

    public char NextExpectedChar()
    {
        return char.ToLower(word[typedProgress]);
    }
}