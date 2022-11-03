using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueManager : MonoBehaviour {
    [SerializeField] private Text dialogue;
    //[SerializeField] private GameObject DialogueScreen;
    [SerializeField] private Text whoseTalking;
    [SerializeField] private GameObject textPanel;
    private bool dialogueIsRunning;
    public static event UnityAction requestNextLine;
    //public static event UnityAction skipDialogue;
    public static event UnityAction<int> switchControls;
    // Start is called before the first frame update
    void Start()
    {
        SceneDialogue.pullUpDialogue += DialogueUp;
        SceneDialogue.turnOffDialogue += DialogueUp;
        SceneDialogue.sendName += SetTalker;
        SceneDialogue.sendLine += SetDialogue;
        PlayerInputs.nextLine += NextLine;
    }

    // Update is called once per frame

    private void NextLine() { 
        if (requestNextLine != null) {
                requestNextLine();
        }
    }
    private void DialogueUp(bool val) {
        textPanel.SetActive(val);
        dialogueIsRunning = val;
        if (val) {
            switchControls.Invoke(4);
        }
        else {
            print("fuck u controls");
            switchControls.Invoke(0);
        }
        
    }
    private void SetTalker(string name) {
        whoseTalking.text = name;
    }
    private void SetDialogue(string text) {
        dialogue.text = text;
    }
    
}
