using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{

    [SerializeField]private GameObject shadowShot;
    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject swordAura;
    [SerializeField] private GameObject swordAura2;
    [SerializeField] private GameObject fireTrailR;
    [SerializeField] private GameObject fireTrailL;
    [SerializeField] private GameObject teleportEffect;

    [Header("Blast")]
    [SerializeField] private GameObject shadowBlast;
    private PlayerBodyObjects bodyObjects;
    public GameObject ShadowShot { get => shadowShot; set => shadowShot = value; }
    public GameObject Lightning { get => lightning; set => lightning = value; }
    public GameObject SwordAura { get => swordAura; set => swordAura = value; }
    public GameObject SwordAura2 { get => swordAura2; set => swordAura2 = value; }
    public GameObject TeleportEffect { get => teleportEffect; set => teleportEffect = value; }

    private void Start() {
        bodyObjects = GetComponent<PlayerBodyObjects>();
    }
    private void SwordAuraControl(bool val) {
        SwordAura.SetActive(val);
        print("Touched");
    }
    private void SwordAuraControl2(bool val) {
        SwordAura2.SetActive(val);
    }
    //public void FireShadowBlast() {
    //    GameObject blast;
    //    blast= Instantiate(shadowBlast,bodyObjects.RightHandBlastPoint.transform.position, Player.GetPlayer().transform.rotation);
    //    blast.transform.SetParent(bodyObjects.RightHandBlastPoint.transform);
    //}
}
