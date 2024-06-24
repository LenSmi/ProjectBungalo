using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameStates
{
    Loading,
    Hub,
    Trench,
    ScoreAttackSaloon,
    ScoreAttackStart,
    ScoreAttackEnd,
}

public class WorldStateManager : MonoBehaviour
{
    public EGameStates initialGameState;
    [HideInInspector]
    public EGameStates gameStates;
    public GameState currentGameState;
    public GameState currentState;
    public float underwaterTime;
    public SceneChangeManager sceneChangeManager;
    public GameObject stateObject;

    public void Start()
    {
#if UNITY_EDITOR
        TransitionToState(initialGameState);
#endif
    }

    public void TransitionToState(EGameStates gameState)
    {

        switch (gameState)
        {
            case EGameStates.Hub:
                ChangeState<HubState>();
                break;
            case EGameStates.Trench:
                ChangeState<TrenchState>();
                break;
            case EGameStates.ScoreAttackSaloon:
                ChangeState<ScoreAttackSaloonState>();
                break;
            case EGameStates.ScoreAttackStart:
                ChangeState<ScoreAttackStartState>();
                break;
            case EGameStates.ScoreAttackEnd:
                ChangeState<ScoreAttackEndState>();
                break;
        }
    }

    public void ChangeState<T>() where T: GameState
    {
        if (currentGameState != null)
        {
            currentGameState.ExitGamestate();
            Destroy(currentGameState);
        }

        currentGameState = stateObject.AddComponent<T>();
        currentGameState.EnterGamestate();
    }
}
