using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockedUp : StateMachineBehaviour
{
    private Enemy enemy;
    public Enemy Enemy { get => enemy; set => enemy = value; }
    [SerializeField] Vector3 direction;
    [SerializeField] float move;
    [SerializeField] private bool forward;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(!forward)
            enemy.CharCon.Move(direction*move*Time.deltaTime);
        else
            enemy.CharCon.Move(enemy.transform.forward * move * Time.deltaTime);
    }
}
