using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider))]
public class EnemyBody : MonoBehaviour
{
    [SerializeField] private Enemy body;
    [SerializeField] private Material bodyMat;
    [SerializeField] private Material attMat;
    [SerializeField] private Material attLightMat;
    [SerializeField] private Material attDarkMat;
    [SerializeField] private SkinnedMeshRenderer mesh;

    public static event UnityAction<float> force;
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        Attacked(other);
        if(!body.Parry)
            body.CalculateDamage(other.GetComponent<HitBox>().AdditionalDamage, other.GetComponent<HitBox>().Type);
        print("Hit");
        body.Hit = true;
        //body.KnockedUp();
    }
    private void Attacked(Collider other) {
        switch (other.GetComponent<HitBox>().Type) {
            case HitBoxType.Light:
                mesh.material = attLightMat;
                break;
            case HitBoxType.Dark:
                mesh.material = attDarkMat;
                break;
            default:
                mesh.material = attMat;
                break;
        }
        StartCoroutine(waitToReset());
    }
    IEnumerator waitToReset() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        mesh.material = bodyMat;
    }
}
