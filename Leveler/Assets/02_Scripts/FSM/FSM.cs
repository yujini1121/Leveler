using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IBaseState
{
    void Enter();
    void Excute();
    void Exit();
}

public enum StateType
{
    Idle,
    Attack,
    Chase,
    Patrol
}

public abstract class BaseState<T> : IBaseState
{
    protected T _context;
    protected FSM<T, StateType> _fsm;

    protected BaseState(T context, FSM<T, StateType> fsm)
    {
        _context = context;
        _fsm = fsm;
    }

    public abstract void Enter();
    public abstract void Excute();
    public abstract void Exit();
}

public class FSM<T, TStateEnum> where TStateEnum : Enum
{
    private Dictionary<TStateEnum, BaseState<T>> _states;
    private IBaseState _curState;
    public IBaseState Curstate => _curState;

    public void SetStates(Dictionary<TStateEnum, BaseState<T>> states)
    {
        _states = states;
    }

    public void ChangeState(TStateEnum stateKey)
    {
        if (_states.TryGetValue(stateKey, out var nextState))
        {
            _curState?.Exit();
            _curState = nextState;
            _curState.Enter();
        }
        else
        {
            Debug.LogWarning($"State {stateKey} not found.");
        }
    }

    public void Update()
    {
        _curState?.Excute();
    }
}