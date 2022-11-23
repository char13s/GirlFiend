using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DarkSwordRelic : EquipmentObj
{
    public static event UnityAction<bool> demonUp;

    public override void Circle() {
        //Pull Enemies in
        //if (!Player.GetPlayer().Extendo)
            Player.GetPlayer().Anim.Play("CastOut");
    }

    public override void CircleReleased() {

    }

    public override void DownCircle() {

    }

    public override void DownSquare() {
        print("UP");
        Player.GetPlayer().Anim.Play("SwordUppercut");
    }

    public override void Square() {
        Debug.Log("Demon Sword Attack");
        Player.GetPlayer().Anim.SetTrigger("Attack");
    }

    public override void Triangle() {
        Player.GetPlayer().Anim.SetTrigger("EnergyAttack");
        
    }
    public override void DownTriangle() {
        Player.GetPlayer().Anim.Play("Holdattack");
    }
    public override void UpTriangle() {
       // Player.GetPlayer().Anim.Play("AoeMagicAttack");
    }
    public override void UpCircle() {
        //Player.GetPlayer().Anim.SetTrigger("");
    }

    public override void UpSquare() {
        Player.GetPlayer().Anim.Play("HoldStab");
    }
    public override void OnEquipped() {
        base.OnEquipped();
        demonUp.Invoke(true);
    }
    public override void UnEquipped() {
        base.UnEquipped();
        demonUp.Invoke(false);
    }
}
