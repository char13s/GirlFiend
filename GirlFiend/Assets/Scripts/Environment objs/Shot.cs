using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] private float speed;
    private void Start() {
        direction = new Vector3(0,-1,0);
        Destroy(gameObject,10f);
    }
    public void SetDirection(Vector3 val) {
        direction = val;
    }
    private void Update() {
        transform.position += direction * speed*Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other) {
        //
    }
}
