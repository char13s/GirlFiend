using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected GameObject mainCam;
    private float moveSpeed=5;
    protected Vector3 direction;
    private Vector2 displacement;
    protected Quaternion qTo;
    private Vector3 speed;

    // gravity variables
    float gravity = -4.9f;
    float groundedGravity = -0.5f;
    // jump pamrs
    #region Jump parms
    bool isJumpPressed;
    float intialJumpVelocity;
    [SerializeField] float maxJumpHeight = 8;
    [SerializeField] float maxJumpTime = .75f;
    [SerializeField] float fallMultipler;
    bool isJumping = false;
    #endregion
    #region anim paramters
    private bool moving;
    bool isFalling;
    private bool grounded;
    //protected bool lockedOn;
    //public bool Grounded { get => grounded; set { grounded = value; Anim.SetBool("Grounded", grounded); } }
    public bool Moving { get => moving; set { moving = value;Anim.SetBool("Moving", moving);  } }//  } }
    //public bool LockedOn { get => lockedOn; set { lockedOn = value; Anim.SetBool("LockedOn", lockedOn); } }
    #endregion
    #region Outside Scripts
    //private DefaultInputs inputs;
    private Animator anim;
    private Player player;
    private CharacterController charCon;
    //private PlayerEffects effects;
    //private PlayerInput map;
    //internal PlayerStats stats = new PlayerStats();
    // private PlayerLockOn lockon;
    #endregion
    #region Getters and Setters
    public Animator Anim { get => anim; set => anim = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public Vector2 Displacement { get => displacement; set => displacement = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public CharacterController CharCon { get => charCon; set => charCon = value; }
    public bool IsJumpPressed { get => isJumpPressed; set { isJumpPressed = value; Debug.Log(isJumpPressed); } }
    public bool IsFalling { get => isFalling; set { isFalling = value; } }

    protected bool Grounded { get => grounded; set { grounded = value; } }
    #endregion
    private void Start() {
        player = GetComponent<Player>();
        Anim = player.Anim;
        CharCon = GetComponent<CharacterController>();
        SetUpJump();
        MovingState.returnSpeed += Move;
        mainCam = GameManager.GetManager().Camera;
        Displacement = new Vector3(1,0,0);
        StartCoroutine(WaitToSTop());
        rotationSpeed = 300;
        //PlayerAnimationEvents.setjump += Jumping;
        //DashBehavior.dash += Dash;
    }
    IEnumerator WaitToSTop() {
        yield return null;
        Displacement = Vector3.zero;
        rotationSpeed = 100;
    }
    private void FixedUpdate() {
        //if (!player.InTeleport)
            GetInputs();
    }
    private void GetInputs() {
        Rotate();
        Anim.SetBool("Grounded", charCon.isGrounded);
        //if (!player.AirAttack) {
            charCon.Move(speed * Time.deltaTime);
            Gravity();
        //}
        //else {
        //    charCon.Move(new Vector3(0, -0.1f, 0) * Time.deltaTime);
        //}
        HandleJump();
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
    private void Rotate() {
        Direction = new Vector3(Displacement.x, 0, 0).normalized;
        if (Displacement.x != 0) {
            Moving = true;
            direction.y = 0;
            //Vector3 rot = Vector3.Normalize(Direction);
            //rot.y = 0;
            qTo = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, Time.deltaTime * rotationSpeed);
            //Vector3 vector = Direction.normalized;
            speed.x = moveSpeed * displacement.x;
            //speed.z = moveSpeed * vector.z;

        }
        else {
            Moving = false;
            MoveSpeed = 0;
            speed.x = 0;
            speed.z = 0;
        }

    }
    void HandleJump() {
        //charCon.Move(speed * Time.deltaTime);
        if (!isJumping && charCon.isGrounded && isJumpPressed) {
            Debug.Log("jumped");
            isJumping = true;
            speed.y = intialJumpVelocity * .5f;
            anim.SetTrigger("Jump");
            //Debug.Log("ran Jump"+intialJumpVelocity);
        }
        else if (isJumping && charCon.isGrounded && !isJumpPressed) {
            isJumping = false;
        }
    }
    void ResetMove() {
        speed = new Vector3(0, 0, 0);
    }
    private void Move(float move) => MoveSpeed = move;
    void SetUpJump() {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        intialJumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }
    public void Jump() {
        //IsJumpPressed = true;
        print("Jump pressed");
        anim.SetTrigger("Jump");
    }
    private void Jumping(bool val) {
        IsJumpPressed = val;
    }
    private void Dash() {
        charCon.Move(transform.forward * -35 * Time.deltaTime);
    }
}