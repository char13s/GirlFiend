using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//using XInputDotNetPure;
#pragma warning disable 0649
[RequireComponent(typeof(BoxCollider))]
public class HitBox : MonoBehaviour
{
    private Player player;

    [SerializeField] private HitBoxType type;
    [SerializeField] private GameObject effects;
    [SerializeField] private float additionalDamage;
    [SerializeField] private bool fireball;
    private AudioSource audio;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject enemyImAttacking;

    public static UnityAction onEnemyHit;
    public static event UnityAction<Enemy, float> sendFlying;
    public static event UnityAction<AudioClip> sendsfx;
    public static event UnityAction<int> sendHitReaction;
    public GameObject EnemyImAttacking { get => enemyImAttacking; set => enemyImAttacking = value; }
    public HitBoxType Type { get => type; set => type = value; }
    public float AdditionalDamage { get => additionalDamage; set => additionalDamage = value; }

    // Start is called before the first frame update
    void Start() {
        player = Player.GetPlayer();
        //audio = player.Sfx;

    }
    private void OnDisable() {

        enemies.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        //if (!enemies.Contains(other.gameObject)) {
        if(effects!=null)
            Instantiate(effects, other.gameObject.transform);

            if (other != null && other.GetComponent<EnemyBody>() && !enemies.Contains(other.gameObject)) {
                if (enemies.Contains(other.gameObject)) {

                }
                if (onEnemyHit != null) {
                    onEnemyHit();
                }
                enemies.Add(other.gameObject);
                //other.GetComponent<Enemy>().CalculateDamage(0);
                //other.GetComponent<Enemy>().KnockBack(HitKnockback());
                //other.GetComponent<Enemy>().Grounded = false;
                //GamePad.SetVibration(0, 0.2f, 0.2f);
                StartCoroutine(StopRumble());
                //sendHitReaction.Invoke(2);
            }
        //}


        else {
            if(effects!=null)
                Instantiate(effects, other.gameObject.transform);
        }
        if (fireball) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {

    }
    private IEnumerator StopRumble() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        //GamePad.SetVibration(0, 0, 0);
    }
    private void OnTriggerExit(Collider other) {
        //EnemyImAttacking = null;
    }
}
