using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.Timeline;
#pragma warning disable 0649
public class PlayerBattleSceneMovement : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>(16);
    private Player player;
    private PlayerMovement playerMove;
    private SignalReceiver signal;
    private int t;//targeted enemy in the array of enemies
    private Enemy enemyTarget;
    public static event UnityAction<bool> onLockOn;
    public static event UnityAction<int> playBattleTheme;
    private Enemy closestEnemy;
    private bool locked;
    private float rotateSpeed;
    private GameObject aimPoint;
    //private GameObject leftPoint;
    private bool rotLock;
    private float rotationSpeed;
    private bool takedown;

    [SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private CinemachineVirtualCamera battleCam;
    public float RotationSpeed { get => rotationSpeed; set { rotationSpeed = value; Mathf.Clamp(value, 5, 8); } }
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public int T { get => t; set { t = value; Mathf.Clamp(t, 0, Enemies.Count); } }
    public Enemy EnemyTarget { get => enemyTarget; set { enemyTarget = value; } }
    public float RotateSpeed { get => rotateSpeed; set { rotateSpeed = value; Mathf.Clamp(value, 5, 8); } }

    public Enemy ClosestEnemy { get => closestEnemy; set => closestEnemy = value; }
    public bool Takedown { get => takedown; set => takedown = value; }
    public SignalReceiver Signal { get => signal; set => signal = value; }

    private void Awake() {
        //Player.attackModeUp += LockOnFuctionality;
        //FindPlayer.sendThisCam += GetMainCam;
        Enemy.onAnyDefeated += RemoveTheDead;
        Player.onPlayerDeath += RemoveAllEnemies;
        //GameController.onGameWasStarted += RemoveAllEnemies;
        //GameController.returnToLevelSelect += RemoveAllEnemies;
        Player.findClosestEnemy += GetClosestEnemy;
        Player.playerIsLockedOn += Locked;
        //UiManager.nullEnemies+=RemoveAllEnemies;
        //Dash.dashu += RemoveAllEnemies;
        T = 0;

    }
    private void OnEnable() {

    }
    private void OnDisable() {

    }
    private void Start() {
        player = Player.GetPlayer();
        playerMove = GetComponent<PlayerMovement>();
        signal = GetComponent<SignalReceiver>();
        //aimPoint = player.AimmingPoint;
        //leftPoint = Player.GetPlayer().PlayerBody.LeftPoint;
    }
    private void RemoveTheDead(Enemy enemy) {
        Enemies.Remove(enemy);
    }
    private void RemoveAllEnemies() {
        Enemies.Clear();
    }
    private void Update() {
        Vector3 position = transform.position;
        for (int i = 0; i < Enemy.TotalCount; i++) {
            Enemy current = Enemy.GetEnemy(i);
            bool shouldBeInList = false;
            if (current != null) { shouldBeInList = Vector3.SqrMagnitude(current.transform.position - position) <= 40000; }
            int index = enemies.IndexOf(current);
            if (shouldBeInList != index >= 0) {
                if (shouldBeInList) {
                    enemies.Add(current);
                    if (enemies.Count > 1) {
                        GetClosestEnemy();
                    }
                }
                else { enemies.RemoveAt(index); }
            }
        }
        //if (Takedown) {
        //    StayLockedToTarget();
        //}
        if(!player.PlayerMove.Moving)
            LockedOn(player.LockedOn);

        if (player.LockedOn) {

            if (enemies.Count == 0) {
                //player.Direction = 0;
                //player.HasTarget = false;
                BasicMovement();

            }
            else {
                //player.HasTarget = true;
                GetInput();
            }
        }

        if (enemies.Count == 0) {
            ClosestEnemy = null;
        }
        if (t > enemies.Count && t > 0) {
            T--;
        }
        if (T == Enemies.Count) {
            T = 0;
        }

        /*if (enemyTarget != null && !playing) {
            playing = true;
            casual = false;
            if (playBattleTheme != null) {
                playBattleTheme(8);
            }
        }
        if (enemyTarget == null && !casual) {
            playing = false;
            casual = true;
            if (playBattleTheme != null) {
                playBattleTheme(7);
            }

        }*/
    }

    private void Locked() { locked = true; }
    private void Unlocked() { locked = false; }
    private void GetInput() {
        if (Enemies.Count != 0 && T < Enemies.Count) {

            LockOn(Enemies[T], playerMove.Displacement.x, playerMove.Displacement.y);
        }
    }


    private void TeleportAttacking(Vector3 location, int t) {
        transform.position = location;
        //player.CmdInput = 101;
    }
    private void EnemyLockedTo() {
        EnemyTarget = enemies[T]; //Enemy.GetEnemy(enemies.IndexOf(enemies[T])); stupid code -_-
        //player.PlayerBody.BattleCamTarget.transform.position = EnemyTarget.transform.position;

    }
    private void GetClosestEnemy() {
        if (T < enemies.Count&&enemies[T]!=null) {
            float enDist = EnDist(enemies[T].gameObject);

            foreach (Enemy en in Enemies) {
                ClosestEnemy = en;
                if (EnDist(en.gameObject) < enDist) {
                    T = Enemies.IndexOf(en);
                }
            }
        }
    }
    private void GetCombatMovement(float x, float y) {
        if (x == 0 && y == 0) {
            //Player.GetPlayer().CombatAnimations = 0;
        }
        if (x == 0) {
            Debug.Log("Combat Jump");
            if (y <= -0.3f) {
                Debug.Log("Combat BackJump");
                player.CombatAnimations = 1;
            }
            if (y >= 0.3f) {
                Debug.Log("Combat BackJump");
                player.CombatAnimations = 5;
            }
        }
        if (y == 0) {
            if (x <= -0.5f) {
                player.CombatAnimations = 2;
            }
            if (x >= 0.5f) {
                player.CombatAnimations = 3;
            }
        }
    }
    private float EnDist(GameObject target) => Vector3.Distance(target.transform.position, player.transform.position);
    private void SetLock() {
        rotLock = true;
    }
    private void ResetLock() {
        rotLock = false;
    }
    private void BasicMovement() {
        //print("Basic moving");
        float x = playerMove.Displacement.x;
        float y = playerMove.Displacement.y;
        //RotateSpeed = 18 - EnDist(player.PlayerBody.BattleCamTarget);
        //Vector3 delta = player.PlayerBody.BattleCamTarget.transform.position;
        //delta.y = 0;
        //transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        //transform.position = Vector3.MoveTowards(transform.position, aimPoint.transform.position, player.MoveSpeed * y * Time.deltaTime);
    }

    private void LockOn(Enemy target, float x, float y) {
        //SwitchLockOn();
        LockOff();
        enemies[T].LockedOn = true;
        EnemyLockedTo();
        //if (!slide) { 
        //MovementInputs(x, y);
        //print("Lockon moving");
        if (Enemies[T] != null) {

            RotateSpeed = 18 - EnDist(target.gameObject);
            Vector3 delta = target.transform.position - player.transform.position;
            delta.y = 0;
            if (!rotLock) {
                transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
            }
            //player.PlayerBody.FarHitPoint.transform.position = (Enemies[T].transform.position - transform.position) / 2;
            //if (!player.Dodge)
                //transform.RotateAround(target.transform.position, target.transform.up, -x * RotateSpeed * player.MoveSpeed * Time.deltaTime);

            if (y != 0) {
                Vector3 speed;
                //speed = transform.forward * player.MoveSpeed * y;
            }
            if (Enemies[T].Dead) {
                GetClosestEnemy();
            }
        }
    }
    private void StayLockedToTarget() {
        Vector3 delta = enemyTarget.transform.position - player.transform.position;
        delta.y = 0;
        if (!rotLock) {
            transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        }
    }
    private void LockOff() {
        foreach (Enemy en in Enemies) {
            if (Enemy.GetEnemy(T) != en) {
                en.LockedOn = false;
                onLockOn.Invoke(false);
            }
        }
    }
    private void LockedOn(bool val) {
        main.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering.m_enabled = val;
    }

   /* private void MovementInputs(float x, float y) {
        if (x == 0) {
            if (y > 0)//forward
            {
                player.Direction = 0;
            }

            if (y < 0)//back
            {
                player.Direction = 2;
            }
        }
        if (x > 0.3)//right
        {
            player.Direction = 3;
        }

        if (x < -0.3)//left
        {
            player.Direction = 1;

        }
        if (Mathf.Abs(x) >= 0.001 || Mathf.Abs(y) >= 0.001) { }

    }*/
    private void GetMainCam(CinemachineVirtualCamera cam) {
        main = cam;
    }
}
