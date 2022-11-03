using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ElectricSwordRelic : EquipmentObj
{
    public static event UnityAction<bool> electricUp;
    public override void Circle() {
        //Paralysis attack
    }

    public override void CircleReleased() {

    }

    public override void DownCircle() {

    }

    public override void DownSquare() {
        Player.GetPlayer().Anim.Play("SpinAttack2");
    }

    public override void Square() {
        Player.GetPlayer().Anim.SetTrigger("ElectricAttack");
    }

    public override void Triangle() {
        Player.GetPlayer().Anim.Play("ChargeLighning");
    }

    public override void UpCircle() {
        
    }

    public override void UpSquare() {
        Player.GetPlayer().Anim.Play("ReadyLaunch");
    }
    public override void OnEquipped() {
        base.OnEquipped();
        electricUp.Invoke(true);
    }
    public override void UnEquipped() {
        base.UnEquipped();
        electricUp.Invoke(false);
    }
}
