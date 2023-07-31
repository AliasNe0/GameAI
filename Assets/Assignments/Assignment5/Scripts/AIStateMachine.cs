using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ASSIGNMENT5
{
    public class AIStateMachine : MonoBehaviour
    {
        //[SerializeField] public NavMeshSurface navSurface;

        public List<AIState> States { get; private set; }
        public Animator AIAnimator { get; private set; }
        public NavMeshAgent Navigation { get; private set; }
        public AIDetection Detection { get; private set; }
        public AIChaseAction ChaseAction { get; private set; }
        public AIChopAction ChopAction { get; private set; }
        public AIIdleAction IdleAction { get; private set; }

        AIState currentState;
        bool firstIdle = true;

        void Awake()
        {
            AIAnimator = GetComponentInChildren<Animator>();
            Navigation = GetComponent<NavMeshAgent>();
            Detection = GetComponent<AIDetection>();
            ChaseAction = GetComponent<AIChaseAction>();
            ChopAction = GetComponent<AIChopAction>();
            IdleAction = GetComponent<AIIdleAction>();
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
                new AIChaseState(this),
                new AIChopState(this),
            };
            currentState = States.Find(st => st.GetType() == typeof(AIIdleState));
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

            currentState.OnEnter();
            AIAnimator.SetTrigger("Idle");
        }

        void Update()
        {
            if (Detection.Tree)
            {
                if (firstIdle && currentState.GetType() == typeof(AIIdleState))
                {
                    firstIdle = false;
                    ChangeToState(typeof(AIChaseState));
                }
                else if (Detection.HasPathToTree)
                {
                    if (!ChaseAction.Active && currentState.GetType() == typeof(AIChaseState))
                    {
                        ChangeToState(typeof(AIIdleState));
                    }
                    else if (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState))
                    {
                        ChangeToState(typeof(AIChopState));
                    }
                    else if (!ChopAction.Active && currentState.GetType() == typeof(AIChopState))
                    {
                        ChangeToState(typeof(AIChaseState));
                    }
                }
            }
            else if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
            {
                firstIdle = true;
                Navigation.isStopped = true;
                ChangeToState(typeof(AIIdleState));
            }

            currentState.OnUpdate();
        }

        void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }
    }
}
