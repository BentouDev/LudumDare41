using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public GameState StartState;
    
    public GameState CurrentState { get; set; }
    
    private GameState NextState { get; set; }

    private List<GameState> AllStates;

    void Start()
    {
        AllStates.AddRange(FindObjectsOfType<GameState>());
        
        SwitchState(StartState);
    }

    public void ScheduleState<T>()
    {
        ScheduleState(AllStates.FirstOrDefault(s => s is T));
    }

    public void ScheduleState(GameState state)
    {
        NextState = state;
    }

    public void SwitchState<T>()
    {
        SwitchState(AllStates.FirstOrDefault(s => s is T));
    }
    
    public void SwitchState(GameState state)
    {
        if (CurrentState)
            CurrentState.End();

        CurrentState = state;
        
        if (CurrentState)
            CurrentState.Begin();
    }

    void Update()
    {
        if (CurrentState)
            CurrentState.OnUpdate();
    }

    void FixedUpdate()
    {
        if (CurrentState)
            CurrentState.OnFixedUpdate();
    }

    void LateUpdate()
    {
        if (NextState)
        {
            SwitchState(NextState);
            NextState = null;
        }

        if (CurrentState)
            CurrentState.OnLateUpdate();
    }
}
