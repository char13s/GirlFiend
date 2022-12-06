using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBehavior : StateMachineBehaviour
{
    private Enemy enemy;
    public Enemy Enemy { get => enemy; set => enemy = value; }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Enemy.UnsetHit();
        //Enemy.Rbody.useGravity=
        Debug.Log("Okay Im good now");
        //enemy.Rbody.useGravity = false;
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
    }

}
