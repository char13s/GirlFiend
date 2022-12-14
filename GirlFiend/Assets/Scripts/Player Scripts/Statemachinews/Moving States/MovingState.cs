using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingState : StateMachineBehaviour
{
    [SerializeField] private float speed;
    public static event UnityAction<float> returnSpeed;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        returnSpeed.Invoke(speed);
    }
}
