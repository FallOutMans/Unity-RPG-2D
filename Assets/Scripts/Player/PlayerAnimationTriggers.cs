using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();
    
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            var hit = colliders[i];
            if (hit.GetComponent<Enemy>() != null)
            {
                // 检测到敌人
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
