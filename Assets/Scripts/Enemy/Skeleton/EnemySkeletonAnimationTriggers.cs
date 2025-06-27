using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();
    
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            var hit = colliders[i];
            if (hit.GetComponent<Player>() != null)
            {
                // 检测到敌人
                hit.GetComponent<Player>().Damage();
            }
        }
    }
}
