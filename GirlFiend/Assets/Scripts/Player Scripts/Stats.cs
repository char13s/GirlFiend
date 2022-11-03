using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
[System.Serializable]
public class Stats {
    //Variables
    private int health;
    private int attack;
    private int defense;

    private int mp;
    private float speed;
    private int healthLeft;

    private int mpLeft;
    private byte level = 1;
    private int exp = 0;
    private int requiredExp;

    private int baseAttack;
    private int baseDefense;
    private int baseMp;
    private int baseHealth;

    private int attackBoost;
    private int defenseBoost;
    private int mpBoost;
    private int healthBoost;

    private int swordLevel;
    private int demonFistLevel;
    private int swordProficency;

    private byte kryllLevel;

    private int abilitypoints;
    //Events
    public static event UnityAction onHealthChange;
    public static event UnityAction onMPLeft;
    public static event UnityAction onLevelUp;
    public static event UnityAction onShowingStats;
    public static event UnityAction onBaseStatsUpdate;
    public static event UnityAction onObjectiveComplete;
    public static event UnityAction<int> onPowerlv;
    public static event UnityAction sendSpeed;
    public static event UnityAction<int> onOrbGain;
    //Properties
    #region Getters and Setters
    public int Health { get { return health; } set { health = Mathf.Max(0, value); } }
    public int HealthLeft { get { return healthLeft; } set { healthLeft = Mathf.Clamp(value, 0, health); CalculateStatsOutput(); if (onHealthChange != null) { onHealthChange(); } } }
    public int MPLeft { get { return mpLeft; } set { mpLeft = Mathf.Clamp(value, 0, mp); CalculateStatsOutput(); if (onMPLeft != null) { onMPLeft(); } } }

    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int MP { get { return mp; } set { mp = value; } }
    public float Speed { get { return speed; } set { speed = value; sendSpeed.Invoke(); } }

    public byte Level { get => level; set => level = value; }
    public int Exp { get => exp; set { exp = value; UpdateUi(); } }
    public int BaseAttack { get => baseAttack; set { baseAttack = Mathf.Clamp(value, 0, 300); CalculateStatsOutput(); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseDefense { get => baseDefense; set { baseDefense = Mathf.Clamp(value, 0, 300); CalculateStatsOutput(); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseMp { get => baseMp; set { baseMp = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }
    public int BaseHealth { get => baseHealth; set { baseHealth = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); } }

    public int AttackBoost { get => attackBoost; set { attackBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int DefenseBoost { get => defenseBoost; set { defenseBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int MpBoost { get => mpBoost; set { mpBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }
    public int HealthBoost { get => healthBoost; set { healthBoost = Mathf.Clamp(value, 0, 300); if (onBaseStatsUpdate != null) onBaseStatsUpdate(); SetStats(); } }

    public int RequiredExp { get => requiredExp; set => requiredExp = value; }
    public int Abilitypoints { get => abilitypoints; set { abilitypoints = value; if (onBaseStatsUpdate != null) onBaseStatsUpdate();onOrbGain.Invoke(abilitypoints); } }

    public int SwordProficency { get => swordProficency; set => swordProficency = value; }
    public int SwordLevel { get => swordLevel; set => swordLevel = value; }
    public byte KryllLevel { get => kryllLevel; set => kryllLevel = value; }

    public int CalculateExpNeed() { int expNeeded = 4 * (Level * Level * Level); return Mathf.Abs(Exp - expNeeded); }
    public int ExpCurrent() { return Exp - (4 * ((Level - 1) * (Level - 1) * (Level - 1))); }
    #endregion
    public void AddExp(int points) {
        exp += points;
    }
    public void DisplayAbilities() {
        if (onShowingStats != null) {
            onShowingStats();
        }
    }
    public void Start() {
        
        SetStats();
        //Player.weaponSwitch += SetStats;
        //GameController.onGameWasStarted += UpdateUi;
        //PerfectGuardBox.sendAmt += ChangeMpLeft;
        PlayerInputs.transformed += OnTransformation;
        //SkillTreeNode.sendOrbs += AdjustOrbs;
        Enemy.sendOrbs += AdjustOrbs;
        if (onHealthChange != null) {
            onHealthChange();
        }
        if (onMPLeft != null) {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void UpdateUi() {
        if (onHealthChange != null) {
            onHealthChange();
        }
        if (onMPLeft != null) {
            onMPLeft();
        }
        if (onLevelUp != null) { onLevelUp(); }
    }
    private void SetStats() {
        // + mpBoost
        baseHealth = 120;
        healthLeft = baseHealth;
        baseMp = 15;
        Health = baseHealth;// + healthBoost
        MP = baseMp;
        //mpLeft = baseMp;
        BaseAttack = 5;
        BaseDefense = 5;
        onHealthChange.Invoke();
        onMPLeft.Invoke();
        CalculateStatsOutput();

    }
    private void ChangeMpLeft(int amt) => MPLeft += amt;
    private void CalculateStatsOutput() {
        //calculated everytime health or Mp is changed.
        /*Attack=(HealthLeft/Health+mpLeft)+baseAttack;
        Defense=(HealthLeft/Health+mpLeft)+baseDefense;
        onPowerlv.Invoke((HealthLeft / Health + mpLeft) * (baseDefense+baseAttack));*/
        Attack = BaseAttack+AttackBoost;
        Defense = BaseDefense + DefenseBoost;
    }
    private void AddToAttackBoost() {
        //Upgrading Attacks on Attack boost affect here
        //
        CalculateStatsOutput();
    }
    private void AddToDefenseBoost() {
        //Upgrading Defense boost affect here
        //
        CalculateStatsOutput();
    }
    private void OnTransformation(bool val) {
        if (val) {
            AttackBoost=BaseAttack;
        }
        else {
            AttackBoost = 0;
        }
        //An Mp boost should be given here which would contribute to an attack otput boost
        //but also drains Mp and stamina the longer its held.
        CalculateStatsOutput();
    }
    private void AdjustOrbs(int val) {
        Abilitypoints = val;
    }
    /*private int WeaponBoost() {

        switch (Player.GetPlayer().Weapon) {
            case 0:
                return SwordLevel*2;
            case 1:
                return demonFistLevel*4;
            default:
                return 0;
        }
        
    }
    */
}
