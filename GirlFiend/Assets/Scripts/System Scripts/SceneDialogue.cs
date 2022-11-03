using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneDialogue : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private string talker;
    [SerializeField]private bool forBeginning;
    [SerializeField] private int eventNum;
    private int current;
    private bool done;
    [SerializeField]private bool turnOffWholeObject;
    public static event UnityAction<int> sendEndEvent;
    public int Current { get => current; set => current = value; } //Mathf.Clamp(value,0,lines.Length-1); } }

    public static event UnityAction<string> sendName;
    public static event UnityAction<string> sendLine;
    public static event UnityAction<bool> pullUpDialogue;
    public static event UnityAction<bool> turnOffDialogue;
    public static event UnityAction sealPlayerInput;
    public static event UnityAction unsealPlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        //GameController.onNewGame += TheFirstDialouge;
        DialogueManager.requestNextLine += ProcessLineRequest;
        //DialogueManager.skipDialogue += SkipDialogue;
        //EventManager.demoRestart += UnDone;
    }
    private void OnEnable() {
        StartCoroutine(WaitABit());
        Current = 0;
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitABit() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        if (pullUpDialogue != null) {
            pullUpDialogue(true);
        }
        if (sendName != null) {
            sendName(talker);
        }
        if (sendLine != null) {
            sendLine(lines[0]);
        }
        if (sealPlayerInput != null) {
            sealPlayerInput();
        }
    }
    private void TheFirstDialouge() {
        if (forBeginning) {
            StartCoroutine(WaitABit());
        }
    }
    private void ProcessLineRequest() {
        if (!done) {
            Current++;
            if (current == lines.Length) {
                if (turnOffDialogue != null) {
                    turnOffDialogue(false);
                }
                if (sendEndEvent != null) {
                    sendEndEvent(eventNum);
                }
                if (unsealPlayerInput != null) {
                    unsealPlayerInput();
                }
                done = true;
                if (turnOffWholeObject) {
                    gameObject.SetActive(false);
                }
                else {
                    GetComponent<SceneDialogue>().enabled = false;
                }
                
            }
            else {
                Debug.Log(current);
                if (sendLine != null) {
                    sendLine(lines[Current]);
                }
            }
        }
    }
    private void SkipDialogue() {
        Current = lines.Length - 1;
    }
    private void UnDone() {
        done = false;
        Current = 0;
       //if (sealPlayerInput != null) {
       //    sealPlayerInput();
       //}
    }
}
