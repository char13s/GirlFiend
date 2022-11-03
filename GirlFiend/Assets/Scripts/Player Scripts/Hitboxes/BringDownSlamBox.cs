using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringDownSlamBox : MonoBehaviour
{
    [SerializeField] private GameObject pinpoint;
    private Enemy enemy;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            print("YEAAAAA");
            //enemy.gameObject.transform.SetParent(pinpoint.transform);
            
        }
    }
    public void UnBind() {
        enemy.gameObject.transform.SetParent(enemy.gameObject.transform);
        enemy = null;
    }
}
 