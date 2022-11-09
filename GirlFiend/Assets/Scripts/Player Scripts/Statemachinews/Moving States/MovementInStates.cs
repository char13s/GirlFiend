using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInStates : StateMachineBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            Player.GetPlayer().CharCon.Move(direction * speed * Time.deltaTime);
    }
}
