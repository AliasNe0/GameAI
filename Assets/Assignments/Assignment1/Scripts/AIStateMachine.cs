using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT1
{
    public class AIStateMachine : MonoBehaviour
    {
        AIState currentState;
        public List<AIState> States { get; private set; }
        public AIDetection Detection { get; private set; }
        public AIPatrolAction PatrolAction { get; private set; }
        public AIChaseAction ChaseAction { get; private set; }
        public AIIdleAction IdleAction { get; private set; }

        void Awake()
        {
            Detection = GetComponent<AIDetection>();
            IdleAction = GetComponent<AIIdleAction>();
            PatrolAction = GetComponent<AIPatrolAction>();
            ChaseAction = GetComponent<AIChaseAction>();
        }

        public void ChangeToState(Type stateType)
        {
            AIState newState = States.Find(st => st.GetType() == stateType);
            if (newState == null)
            {
                Debug.Log("Cannot find state " + stateType);
            }
            else
            {
                currentState.OnExit();
                currentState = newState;
                currentState.OnEnter();
            }
        }

        void CreateStates()
        {
            States = new()
            {
                new AIIdleState(this),
                new AIPatrolState(this),
                new AIChaseState(this)
            };
            currentState = States.Find(st => st.GetType() == typeof(AIPatrolState));
        }

        void Start()
        {
            CreateStates();

            if (States.Count == 0)
            {
                throw new ArgumentException("You did not specify any states. Create at least one state in CreateStates method.");
            }

            foreach (AIState state in States)
            {
                state.OnStart();
            }

            if (currentState == null)
            {
                Debug.Log("Initial state not set. Defaulting to first state.");
                currentState = States[0];
            }

            currentState.OnEnter();
        }

        void Update()
        {
            if (Detection.CollectableToPickUp && currentState.GetType() != typeof(AIChaseState))
            {
                ChangeToState(typeof(AIChaseState));
            }
            else if (ChaseAction.TargetIsReached && currentState.GetType() == typeof(AIChaseState))
            {
                ChangeToState(typeof(AIIdleState));
            }
            //else if (!Detection.CollectableToPickUp && currentState.GetType() == typeof(AIChaseState))
            //{
            //    ChangeToState(typeof(AIPatrolState));
            //}
            currentState.OnUpdate();
        }

        void OnEnable()
        {
            foreach (AIState state in States)
            {
                state.OnEnable();
            }
        }

        void OnDisable()
        {
            foreach (AIState state in States)
            {
                state.OnDisable();
            }
        }

        void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }

        void LateUpdate()
        {
            currentState.OnLateUpdate();
        }
    }
}
