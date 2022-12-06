using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private GameObject bodyObject;

    private bool lockedOn;
    private bool moving;
    private int combatAnimations;
    #region outside scripts
    private CharacterController charCon;
    private PlayerMovement playerMove;
    private Animator anim;
    internal Stats stats;
    #endregion
    #region Events
    public static event UnityAction playerIsLockedOn;
    public static event UnityAction findClosestEnemy;
    public static event UnityAction notAiming;
    public static event UnityAction onPlayerDeath;
    #endregion
    #region
    public Animator Anim { get => anim; set => anim = value; }
    public bool LockedOn { get => lockedOn; set => lockedOn = value; }
    public CharacterController CharCon { get => charCon; set => charCon = value; }
    public int CombatAnimations { get => combatAnimations; set => combatAnimations = value; }
    public PlayerMovement PlayerMove { get => playerMove; set => playerMove = value; }

    //public bool Moving { get => moving; set { moving = value;anim.SetBool("Moving",moving); } }
    #endregion
    public static Player GetPlayer() => instance;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }

        Anim = bodyObject.GetComponent<Animator>();
        charCon = GetComponent<CharacterController>();
        playerMove = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TargetingLogic(bool val) {
        print(LockedOn);
        if (val) {
            LockedOn = true;
            if (playerIsLockedOn != null) {
                playerIsLockedOn();
            }
            findClosestEnemy.Invoke();
        }
        else {
            //notAiming.Invoke();
            LockedOn = false;
        }
    }

}
