using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : StateMachineBehaviour
{
    [SerializeField] private float move;
    [SerializeField] private GameObject burst;
    private PlayerMovement playerMove;
    private Player player;
    private CharacterController charCon;
    private Vector3 speed;
    [SerializeField] private float gravity = -4.9f;
    [SerializeField] private float groundedGravity = -0.5f;
    [SerializeField] private float fallMultipler;
    private bool IsFalling;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = Player.GetPlayer();
        playerMove = player.PlayerMove;
        charCon = player.CharCon;
        //player.Jumping = false;
        //player.Grounded = false;

        //pc.RBody.velocity = new Vector3(0, 0, 0);
        //pc.RBody.AddForce(pc.transform.forward * 120, ForceMode.Impulse);
        //pc.RBody.AddForce(new Vector3(0, move, 0), ForceMode.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //pc.transform.position = Vector3.MoveTowards(pc.transform.position,pc.HitPoint.transform.position,move*Time.deltaTime); 
        //pc.transform.position = Vector3.MoveTowards(pc.transform.position, pc.JumpPoint.transform.position, move * Time.deltaTime);
        //player.Grounded = false;
        //pc.RBody.AddForce();
        //Player.GetPlayer().GroundChecker.SetActive(false);


        //charCon.Move(speed * Time.deltaTime);
        //Gravity();
        //HandleJump();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger("Jump");
        //Player.GetPlayer().GroundChecker.SetActive(true);
    }
    private void Gravity() {
        IsFalling = speed.y <= 0.0f;
        //float fallMultipler =0.5f;

        if (charCon.isGrounded) {
            speed.y = groundedGravity;
        }
        else if (IsFalling) {
            float prevVelocity = speed.y;
            float newVelocity = speed.y + (gravity * fallMultipler * Time.deltaTime);
            float nextVelocity = (prevVelocity + newVelocity) * .5f;
            speed.y = nextVelocity;
        }
        else {
            float prevVelocity = speed.y;
            float newVelocity = speed.y + (gravity * Time.deltaTime);
            float nextVelocity = (prevVelocity + newVelocity) * .5f;
            speed.y = nextVelocity;
        }
    }
}
