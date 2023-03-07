using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action OnStateChanged;
 
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private const float gamePlayingTimerMax = 60f;
    
    private State state;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    
    private void ChangeState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke();
    }

    protected override void Awake()
    {
        base.Awake();
        state = State.WaitingToStart;
    }
    
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                    ChangeState(State.CountdownToStart);
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    ChangeState(State.GamePlaying);
                    gamePlayingTimer = gamePlayingTimerMax;
                }
                
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                    ChangeState(State.GameOver);
                break;
            case State.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Debug.Log(state);
    }
    public float GetCountdownToStartTimer() => countdownToStartTimer;
    public bool IsCountdownToStart() => state == State.CountdownToStart;
    public bool IsGamePlaying() => state == State.GamePlaying;

    public bool IsGameOver() => state == State.GameOver;
    
    public float GetGamePlayingTimerNormalized() => 1 - gamePlayingTimer / gamePlayingTimerMax;
    
}
