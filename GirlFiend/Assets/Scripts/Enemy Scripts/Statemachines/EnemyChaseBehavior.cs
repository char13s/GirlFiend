using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseBehavior : StateMachineBehaviour
{
    private Enemy enemy;
    public Enemy Enemy { get => enemy; set => enemy = value; }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Enemy.Chasing();
        //enemy.Rbody.velocity=enemy.transform.forward*10;
        //enemy.transform.position += enemy.transform.forward * 0.5f*Time.deltaTime;
    }
}
