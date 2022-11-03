using UnityEngine;
using UnityEngine.UI;
public class HitText : MonoBehaviour
{
    private string text;
    private Player player;
    private GameManager gm;
    [SerializeField] private Text counter;
    [SerializeField] private Canvas canvas;
    public string Text { get => text; set { text = value; counter.text = text; } }
    public HitText(string text) {
        this.text = text;
        Debug.Log("bruh");
    }
    // Start is called before the first frame update
    void Start() {
        gm = GameManager.GetManager();
        Destroy(gameObject, 0.5f);
    }
    // Update is called once per frame
    void Update() {
        Vector3 direction = gm.Camera.transform.position - transform.position;
        Quaternion qTo;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = qTo;
        transform.localPosition += new Vector3(0, 3, 0) * Time.deltaTime;
    }
}
