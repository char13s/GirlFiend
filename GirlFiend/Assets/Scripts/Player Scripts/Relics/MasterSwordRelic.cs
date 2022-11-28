using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MasterSwordRelic : EquipmentObj
{
    public static UnityEvent<bool> masterUp;
    public override void Circle() {
        //shield shit
    }

    public override void CircleReleased() {

    }

    public override void DownCircle() {

    }

    public override void DownSquare() {

    }

    public override void Square() {
        Player.GetPlayer().Anim.SetTrigger("Attack");
    }

    public override void Triangle() {
        //special
    }

    public override void UpCircle() {

    }

    public override void UpSquare() {

    }
    public override void OnEquipped() {
        base.OnEquipped();
        masterUp.Invoke(true);
    }
    public override void UnEquipped() {
        base.UnEquipped();
        masterUp.Invoke(false);
    }
}
