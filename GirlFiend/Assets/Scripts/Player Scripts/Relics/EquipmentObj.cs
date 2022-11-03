using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentObj : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private bool weaponUp;
    public GameObject Weapon { get => weapon; set {  weapon = value;}}
    public bool WeaponUp { get => weaponUp; set => weaponUp = value; }

    // Start is called before the first frame update
    void Start() {

    }
    public virtual void AssignCircle() {
        print("Circle assigned");
        //PlayerCommands.circle += Circle;
        //PlayerCommands.downCircle += DownCircle;
        //PlayerCommands.upCircle += UpCircle;
        //PlayerCommands.holdCircle += HoldCircle;
        // PlayerCommands.triangle += Triangle;
    }
    public virtual void UnAssignCircle() {
        //PlayerCommands.circle -= Circle;
        //PlayerCommands.downCircle -= DownCircle;
        //PlayerCommands.upCircle -= UpCircle;
        //PlayerCommands.holdCircle -= HoldCircle;
        //PlayerCommands.triangle -= Triangle;
    }
    public abstract void Circle();
    public abstract void CircleReleased();
    public abstract void UpCircle();
    public abstract void DownCircle();
    public virtual void HoldCircle() {

    }
    public virtual void PassiveEffect() {

    }
    public abstract void Triangle();
    public virtual void HoldTriangle() {

    }
    public virtual void UpTriangle() {

    }
    public virtual void DownTriangle() {

    }
    public abstract void Square();
    public abstract void UpSquare();
    public abstract void DownSquare();
    public virtual void Transformation() {

    }
    public virtual void OnEquipped() {

    }
    public virtual void UnEquipped() {
        Weapon.SetActive(false);
    }
}
