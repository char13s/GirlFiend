using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerCommands : MonoBehaviour
{
    private enum Inputs { Null, X, Square, Triangle, Circle, XOut, SquareOut, TriangleOut, CircleOut, Up, Down, Right, Left, Direction }
    private enum InputCombination { Null, X, Square, Triangle, Circle, XOut, SquareOut, TriangleOut, CircleOut, Up, Down, Right, Left, Direction,XUp,XDown,XLeft,XRight,SquareUp,SquareDown,TriangleUp,TriangleDown,CircleUp,CircleDown }
    #region Events
    public static event UnityAction<string> sendInput;
    public static event UnityAction circle;
    public static event UnityAction upCircle;
    public static event UnityAction downCircle;
    public static event UnityAction holdCircle;
    public static event UnityAction triangle;
    public static event UnityAction upTriangle;
    public static event UnityAction downTriangle;
    public static event UnityAction holdTriangle;
    //public static event UnityAction<int> sendChain;
    #endregion

    private Coroutine fakeUpdate;
    private bool lockon;
    private bool still;
    #region Outside Scripts
    private Animator anim;
    private Animator animObject;
    private Player player;
    private PlayerInputs playerInputs;
    private PlayerMovement playerMove;
    #endregion
    #region Anim parameters
    private int chain;
    private int movementChain;
    private Vector2 stick;
    #endregion
    [SerializeField] private Inputs inputs;
    [SerializeField] private Inputs direction;
    private InputCombination combination;
    public int Chain { get => chain; set { chain = value; anim.SetInteger("ChainInput", chain); } }

    public int MovementChain { get => movementChain; set { movementChain = value; /*anim.SetInteger("MoveInput", movementChain); */} }

    private InputCombination Combination { get => combination; set { combination = value;if(combination!=InputCombination.Null)InputRead(); } }

    private void Awake() {
        //inputs = new List<Inputs>(52);
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMovement>();
        animObject = GetComponent<Animator>();

        playerInputs = GetComponent<PlayerInputs>();

    }
    private void OnEnable() {
        //fakeUpdate=StartCoroutine(SlowUpdate());
    }
    private void OnDisable() {
        //StopCoroutine(SlowUpdate());
    }
    void Start() {
        anim = player.Anim;
    }
    private void Update() {
        //if (!player.SkillButton) {
        GetInputs();
        if (stick.sqrMagnitude == 0) {
            ResetDirection();
            still = true;
        }
        else {
            still = false;
        }

    }
    private void GetInputs() {
        InputChains();
        //InputRead();
        if (player.LockedOn) {
            if (player.CharCon.isGrounded) {
                NeutralInputCombinations();
            }
            else {
                AirCombinations();
            }
            AdvancedMovement();
            RelicCombinations();
            if (player.CombatAnimations == 0) {
                MovementInputs();
            }
        }
    }
    private IEnumerator EmptyChain() {
        YieldInstruction wait = new WaitForSeconds(0.2f);
        yield return wait;
        ResetChain();
    }
    private IEnumerator SlowUpdate() {

        YieldInstruction wait = new WaitForSeconds(0.5f);
        while (isActiveAndEnabled) {
            yield return wait;
            ResetChain();
        }
    }
    #region Basic Inputs
    private void OnMovement(InputValue value) {

        stick = value.Get<Vector2>();
        Vector2 min = new Vector2(0.01f, 0.01f);
    }
    private void OnDash(InputValue value) {
        //Chain = 7;
    }
    private void OnUp(InputValue value) {
        direction = Inputs.Up;
        Debug.Log("Up");
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnDown(InputValue value) {
        direction = Inputs.Down;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnRight(InputValue value) {
        direction = Inputs.Right;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnLeft(InputValue value) {
        direction = Inputs.Left;
        if (!value.isPressed) {
            //ResetDirection();
        }
    }
    private void OnJump(InputValue value) {
        if (value.isPressed) {
            if (sendInput != null) {
                sendInput("X");
            }
            AddInput(Inputs.X);
        }
        else {
            AddInput(Inputs.XOut);
        }
        InputProcess();
        StartCoroutine(EmptyChain());
    }
    private void OnEnergy() {
        if (sendInput != null) {
            sendInput("Triangle");
        }
        AddInput(Inputs.Triangle);
        if (triangle != null) {
            triangle();
        }
        InputProcess();
        StartCoroutine(EmptyChain());
    }
    private void OnAttack(InputValue value) {
        //player.AttackState = true;
        Debug.Log("Attack");
        if (sendInput != null) {
            sendInput("Square");
        }
        if (value.isPressed) {
            AddInput(Inputs.Square);
        }
        InputProcess();
        StartCoroutine(EmptyChain());
    }
    private void OnAbility() {

        if (sendInput != null) {
            sendInput("Circle");
        }
        AddInput(Inputs.Circle);
        if (circle != null) {
            circle();
        }
        InputProcess();
        StartCoroutine(EmptyChain());
    }
    private void OnHoldCircle() {
        if (holdCircle != null) {
            holdCircle();
        }
    }
    private void On() {
        Chain = 16;
    }
    private void OnHoldAttack() {
        //timelines.PlayHoldAttack();
    }
    private void OnHoldEnergy() {
        Chain = 17;
    }
    private void OnHoldTriangle(InputValue value) {
        Chain = 5;
        if (holdTriangle != null) {
            holdTriangle();
        }

    }
    private void AddInput(Inputs button) {
        //if (!player.SkillButton) {
        inputs = button;
        //}
    }
    private void InputChains() {

        if (inputs == Inputs.Triangle && inputs == Inputs.Circle) {
            Debug.Log("Fire!BIcth");
            Chain = 9;
            ResetChain();
        }
        if (inputs == Inputs.X && direction != Inputs.Right && direction != Inputs.Left) {
            Chain = 1;
            //anim.SetTrigger("Jump");
            //ResetChain();
        }
    }
    private void InputProcess() {
        if (inputs == Inputs.X && direction == Inputs.Null) {
            Combination = InputCombination.X;
        }
        if (inputs == Inputs.XOut && direction == Inputs.Null) {
            Combination = InputCombination.XOut;
        }
        if (inputs == Inputs.Square && direction == Inputs.Null) {
            Combination = InputCombination.Square;
        }
        if (inputs == Inputs.Triangle && direction == Inputs.Null) {
            Combination = InputCombination.Triangle;
        }
        if (inputs == Inputs.Circle && direction == Inputs.Null) {
            Combination = InputCombination.Circle;
        }
    }
    private void InputRead() {
        
        switch (Combination) {
            case InputCombination.X:
                playerMove.IsJumpPressed = true;
                break;
            case InputCombination.XOut:
                playerMove.IsJumpPressed = false;
                break;
            case InputCombination.Triangle:
                playerInputs.Relic.Triangle();
                break;
            case InputCombination.Square:
                playerInputs.Relic.Square();
                break;
            case InputCombination.Circle:
                playerInputs.Relic.Circle();
                break;
            case InputCombination.TriangleDown:
                playerInputs.Relic.DownTriangle();
                break;
            case InputCombination.TriangleUp:
                playerInputs.Relic.UpTriangle();
                break;
            case InputCombination.SquareUp:
                playerInputs.Relic.UpSquare();
                break;
            case InputCombination.SquareDown:
                playerInputs.Relic.DownSquare();
                break;
            case InputCombination.CircleUp:
                playerInputs.Relic.UpCircle();
                break;
            case InputCombination.CircleDown:
                playerInputs.Relic.DownCircle();
                break;
        }
        //Combination=InputCombination.Null;
    }
    #endregion
    private void AirCombinations() {
        if (inputs == Inputs.Square && direction == Inputs.Up) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            //anim.ResetTrigger("Attack");
            anim.Play("AirDive");

        }
        if (inputs == Inputs.Square && direction == Inputs.Down) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            ResetChain();
            //anim.ResetTrigger("Attack");
            anim.SetTrigger("AirDownAttack");
        }
    }
    private void RelicCombinations() {
        if (inputs == Inputs.Circle && direction == Inputs.Up) {

            if (sendInput != null) {
                sendInput("Up + Circle");
            }
            if (upCircle != null) {
                upCircle();
            }
            playerInputs.Relic.UpCircle();
            ResetChain();
        }
        if (inputs == Inputs.Circle && direction == Inputs.Down) {

            if (sendInput != null) {
                sendInput("Down + Circle");
            }
            if (downCircle != null) {
                downCircle();
            }
            ResetChain();
        }
    }
    private void NeutralInputCombinations() {
        if (inputs == Inputs.Square && direction == Inputs.Up) {
            Debug.Log("Up Attack!");
            if (sendInput != null) {
                sendInput("Up + Square");
            }
            ResetChain();
            //anim.ResetTrigger("Attack");
            //playerInputs.Relic.UpSquare();
            Combination = InputCombination.SquareUp;
            //anim.Play("HoldStab");

        }
        if (inputs == Inputs.Square && direction == Inputs.Down) {
            Debug.Log("Down Attack!");
            if (sendInput != null) {
                sendInput("Down + Square");
            }
            ResetChain();
            //anim.ResetTrigger("Attack");
            playerInputs.Relic.DownSquare();
            Combination = InputCombination.SquareDown;
            //anim.Play("SpinAttack2");

        }
        if (inputs == Inputs.Triangle && direction == Inputs.Down) {
            Debug.Log("Down Element!");
            if (sendInput != null) {
                sendInput("Down + Triangle");
            }
            //playerInputs.DarkPowers.TriangleDown();
            ResetChain();
            playerInputs.Relic.DownTriangle();
            if (downTriangle != null) {
                downTriangle();
            }
        }
        if (inputs == Inputs.Triangle && direction == Inputs.Up) {
            Debug.Log("Up Element");
            if (sendInput != null) {
                sendInput("Up + Triangle");
            }
            //playerInputs.DarkPowers.TriangleUp();
            ResetChain();
            playerInputs.Relic.UpTriangle();
            if (upTriangle != null) {
                upTriangle();
            }
        }

    }
    private void AdvancedMovement() {

        if (inputs == Inputs.X && direction == Inputs.Up) {
            ResetChain();
            player.CombatAnimations = 5;
            playerMove.IsJumpPressed = false;

            anim.ResetTrigger("Jump");
            anim.Play("ForwardDodge");
        }
        if (inputs == Inputs.X && direction == Inputs.Down) {
            ResetChain();
            player.CombatAnimations = 1;
            playerMove.IsJumpPressed = false;
            anim.Play("KickUp");
            anim.ResetTrigger("Jump");
        }
        if (inputs == Inputs.X && direction == Inputs.Right) {
            ResetChain();
            player.CombatAnimations = 3;
            playerMove.IsJumpPressed = false;
            print("Right dodge");
            anim.Play("RightDodge");
            anim.ResetTrigger("Jump");
        }
        if (inputs == Inputs.X && direction == Inputs.Left) {
            ResetChain();
            player.CombatAnimations = 2;
            playerMove.IsJumpPressed = false;
            print("Left dodge");
            anim.Play("LeftDodge");
            anim.ResetTrigger("Jump");
        }
    }
    private void MovementInputs() {
        if (stick.x < 0.5) {
            if (stick.y > 0)//forward
            {
                MovementChain = 1;
            }
            if (stick.y < 0)//back
            {
                MovementChain = 2;
            }
        }
        if (stick.x > 0.5)//right
        {
            MovementChain = 3;
        }
        if (stick.x < -0.5)//left
        {
            MovementChain = 4;
        }
        if (Mathf.Abs(stick.x) >= 0.001 || Mathf.Abs(stick.y) >= 0.001) {
        }
        else {
            MovementChain = 0;
        }
    }
    private void ResetMoveChain() {
        MovementChain = 0;
    }
    private void ResetChain() {
        inputs = Inputs.Null;
        Combination = InputCombination.Null;
    }
    private void ResetDirection() {
        direction = Inputs.Null;
    }
    private void LockControl(bool val) {
        lockon = val;
    }
    private void emptyChain() {
        Chain = 0;
    }
}
