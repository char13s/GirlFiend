using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool pause;
    public static event UnityAction<bool> pauseScreen;
    public static event UnityAction<int> switchMap;
    public static event UnityAction gameOver;
    [SerializeField] private GameObject camera;

    private int orbAmt;
    public enum GameState { Paused, PlayMode }
    private GameState currentState;
    public GameState CurrentState { get => currentState; set { currentState = value; StateMappings(); } }
    public GameObject Camera { get => camera; set => camera = value; }
    public int OrbAmt { get => orbAmt; set => orbAmt = value; }

    public static GameManager GetManager() => instance;
    // Start is called before the first frame update
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
        CurrentState = GameState.Paused;
    }
    void Start() {
        //PlayerInputs.pause += PauseGame;
        //LevelManager.gameMode += GameStateControl;
        //Stats.onOrbGain += Collect;
        //PlayerInputs.playerEnabled += StateMappings;
        //Player.onPlayerDeath += HandlePlayerDeath;
        //PauseCanvas.pause += PauseGame;
    }
    public void PauseGame() {
        if (pause) {
            pause = false;
            Time.timeScale = 1;
            CurrentState = GameState.PlayMode;
        }
        else {
            pause = true;
            Time.timeScale = 0;
            CurrentState = GameState.Paused;
            //close.Invoke();
        }
        if (pauseScreen != null) {
            pauseScreen(pause);
        }
        Debug.Log("Pause");
    }
    private void HandlePlayerDeath() {
        //kill player controls
        //tell level manager to send back to main menu
        if (switchMap != null)
            switchMap(99);
        if (gameOver != null) {
            gameOver();
        }
    }
    private void GameStateControl(bool val) {
        if (val) {
            CurrentState = GameState.PlayMode;
        }
        else {
            CurrentState = GameState.Paused;
        }
    }
    void StateMappings() {
        switch (currentState) {
            case GameState.PlayMode:
                if (switchMap != null)
                    switchMap(0);
                break;
            case GameState.Paused:
                if (switchMap != null)
                    switchMap(1);
                break;
        }
    }
}
