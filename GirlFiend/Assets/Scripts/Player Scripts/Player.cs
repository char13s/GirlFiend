using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private GameObject bodyObject;

    private bool lockedOn;
    #region outside scripts

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
    #endregion
    public static Player GetPlayer() => instance;
    private void Awake() {
        Anim = bodyObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TargetingLogic(bool val) {
        if (val) {
            LockedOn = true;
            if (playerIsLockedOn != null) {
                playerIsLockedOn();
            }
            findClosestEnemy.Invoke();
        }
        else {
            notAiming.Invoke();
            LockedOn = false;
        }
    }

}
