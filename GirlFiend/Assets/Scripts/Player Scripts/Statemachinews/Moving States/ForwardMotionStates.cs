using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMotionStates : StateMachineBehaviour
{ 
    [SerializeField] private float speed;
    [SerializeField] private float hitOn;
    [SerializeField] private float hitOff;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Player.GetPlayer().CharCon.Move(Player.GetPlayer().transform.forward * speed * Time.deltaTime);
        if (stateInfo.normalizedTime >= hitOn && stateInfo.normalizedTime <= hitOff) {
            Player.GetPlayer().CharCon.Move(speed * Time.deltaTime * Player.GetPlayer().transform.forward);
        }
    }
}
