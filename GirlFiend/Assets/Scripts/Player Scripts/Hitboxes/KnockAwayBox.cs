using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockAwayBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            print("YEAAAAA");
            enemy.KnockedBack();
        }
        else {
            print("empty asf");
        }
    }
}
