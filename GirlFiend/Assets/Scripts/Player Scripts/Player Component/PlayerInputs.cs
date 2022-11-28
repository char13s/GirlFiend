using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class PlayerInputs : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    private PlayerInput map;
    //private Animator anim;
    private Vector2 rotationLook;
    #region Extra attack logic
    bool holdAttack;
    #endregion
    [SerializeField] private float increasedSpeed;
    [SerializeField] private EquipmentObj relic;
    [SerializeField] private EquipmentObj relicUp;
    [SerializeField] private EquipmentObj relicDown;
    [SerializeField] private EquipmentObj relicRight;
    [SerializeField] private EquipmentObj relicLeft;
    
    public EquipmentObj Relic { get => relic; set { relic = value;relic.OnEquipped(); } }// Need code to create relic on player in specific spot so it can be used or just carry them all will they really take up alot of data?
    public Vector2 RotationLook { get => rotationLook; set => rotationLook = value; }
    #region Events
    public static event UnityAction nextLine;
    public static event UnityAction pause;
    public static event UnityAction close;
    public static event UnityAction<int> turnPage;
    public static event UnityAction<bool> transformed;
    public static event UnityAction playerEnabled;
    #endregion
    private void OnEnable() {
        DialogueManager.switchControls += SwitchMaps;
        GameManager.switchMap += SwitchMaps;
        PlayerAnimationEvents.summonWeapon += SummonWeapon;
    }
    private void OnDisable() {
        DialogueManager.switchControls -= SwitchMaps;
        GameManager.switchMap -= SwitchMaps;
        PlayerAnimationEvents.summonWeapon -= SummonWeapon;
    }
    // Start is called before the first frame update
    void Start() {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        map = GetComponent<PlayerInput>();
        //anim = GetComponent<Animator>();
        //playerEnabled.Invoke();
        //Relic = relicUp;
    }

    #region Base Controls
    private void OnMovement(InputValue value) {
        playerMovement.Displacement = value.Get<Vector2>();
        //freeFallMode.Displacement = value.Get<Vector2>();
    }
    private void OnAttack(InputValue value) {

    }
    private void OnHoldAttack(InputValue value) {

    }
    private void OnUp() {

    }
    private void OnEnergy() {

    }

    private void OnJump(InputValue value) {
        print("Jump");
    }
    private void OnAbility(InputValue value) {

    }
    private void OnLook(InputValue value) {
        //print("Looking");
        RotationLook = value.Get<Vector2>();
    }
    private void OnLockOn(InputValue value) {
        if (value.isPressed) {
            player.TargetingLogic(true);
        }
        else {
            player.TargetingLogic(false);
        }
    }

    #region transformations
    #endregion

    #endregion

    #region Dialogue Controls
    private void OnNextLine() {
        nextLine.Invoke();
    }
    #endregion
    #region Pause Controls

    private void OnPause() {
        pause.Invoke();
        Debug.Log("Fuck is this doing?");
    }
    private void OnNextPage() {
        if (turnPage != null) {
            turnPage(1);
        }
    }
    private void OnPreviousPage() {
        if (turnPage != null) { 
            turnPage(-1);
        }
    }
    private void OnClose() {
        if (close != null) { 
            close(); 
        }
    }
    #endregion
    private void SwitchMaps(int val) {
        switch (val) {
            case 0:
                map.SwitchCurrentActionMap("Default Controls");
                //print("Switched to default controls");
                break;
            case 1:
                map.SwitchCurrentActionMap("PauseControls");
                //print("Switched to pause controls");
                break;
            case 2:
                map.SwitchCurrentActionMap("Fall Controls");
                Debug.Log("Falling");
                break;
            case 3:
                map.SwitchCurrentActionMap("Timeline Controls");
                break;
            case 4:
                map.SwitchCurrentActionMap("Dialogue Controls");
                break;
            case 99:
                map.SwitchCurrentActionMap("EmptyControls");
                break;
        }
    }
    private void SummonWeapon() {
        Relic.Weapon.SetActive(true);
    }
}
