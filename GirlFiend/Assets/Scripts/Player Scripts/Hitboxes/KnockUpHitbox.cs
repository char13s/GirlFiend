using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUpHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {

    }
    private void OnTriggerEnter(Collider other) {
       
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            print("YEAAAAA");
            enemy.KnockedUp();
        }
        else {
            print("empty asf");
        }
    }
}
