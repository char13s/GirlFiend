using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private Vector3 direction;
    [SerializeField] private Shot shot;
    private Shot shotClone;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToShoot());
    }
    IEnumerator waitToShoot() {
        YieldInstruction wait = new WaitForSeconds(fireRate);
        while (isActiveAndEnabled) { 
        yield return wait;
        shotClone=Instantiate(shot,transform.position,Quaternion.identity);
        shotClone.SetDirection(direction);
        }
    }
}
