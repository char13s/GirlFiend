using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AngelicRelic : EquipmentObj
{
    public static event UnityAction teleportTo;
    public static event UnityAction quickDodge;
    public static event UnityAction<bool> angelUp;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Circle() {
        teleportTo.Invoke();
    }
    public override void UpCircle() {
        //teleportTo.Invoke();
        print("boom");
    }
    public override void UnEquipped() {
        base.UnEquipped();
        angelUp.Invoke(false);
    }
    public override void OnEquipped() {
        base.OnEquipped();
        angelUp.Invoke(true);
    }
    public override void CircleReleased() {

    }

    public override void DownCircle() {

    }

    public override void Triangle() {
        //Set up vanish slash
    }

    public override void Square() {
        Player.GetPlayer().Anim.SetTrigger("AngelAttack");
    }

    public override void UpSquare() {

    }

    public override void DownSquare() {
        //Heavy Swing
        Player.GetPlayer().Anim.Play("KatanaHeavySwing");
    }
}
