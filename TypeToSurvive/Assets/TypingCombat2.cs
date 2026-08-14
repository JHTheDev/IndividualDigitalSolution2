using UnityEngine;

// Feature: Typing for combat
// Flow: GetKeyInput -> HighlightLetter -> Word complete? -> FireBullet (destroys locked enemy)
//
// Requires a PlayerAimLock component on the same object: this script does
// the per-key checking, PlayerAimLock decides which enemy is targeted.
[RequireComponent(typeof(PlayerAimLock))]
public class TypingCombat2 : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    private PlayerAimLock aimLock;

    void Awake()
    {
        aimLock = GetComponent<PlayerAimLock>();
    }

    void Update()
    {
        GetKeyInput();
    }

    void GetKeyInput()
    {
        foreach (char c in Input.inputString)
        {
            ProcessKey(char.ToLower(c));
        }
    }

    void ProcessKey(char typedChar)
    {
        Enemy target = aimLock.LockedEnemy;

        if (target == null)
        {
            // Not currently locked onto anything - try to acquire a target
            Enemy match = aimLock.FindMatchingEnemy(typedChar);
            if (match != null)
            {
                aimLock.LockAim(match);
                HighlightLetter(match);
            }
            return;
        }

        if (typedChar == target.NextExpectedChar())
        {
            HighlightLetter(target);

            if (target.IsWordComplete)
            {
                FireBullet(target);
                DestroyEnemy(target);
                aimLock.UnlockAim();
            }
        }
        // wrong key presses are ignored - no penalty in this MVP
    }

    void HighlightLetter(Enemy enemy)
    {
        enemy.typedProgress++;
        // TODO: update the enemy's on-screen word to show highlighted progress
    }

    void FireBullet(Enemy target)
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(target.transform.position);
        }
    }

    void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
