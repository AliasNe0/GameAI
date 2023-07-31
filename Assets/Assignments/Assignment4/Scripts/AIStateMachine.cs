using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ASSIGNMENT4
{
    public class AIStateMachine : MonoBehaviour
    {
        [SerializeField] public NavMeshSurface navSurface;

        public List<AIState> States { get; private set; }
        public Animator AIAnimator { get; private set; }
        public NavMeshAgent Navigation { get; private set; }
        public AIDetection Detection { get; private set; }
        public AIChaseAction ChaseAction { get; private set; }
        public AIPickUpAction PickUpAction { get; private set; }
        public AIIdleAction IdleAction { get; private set; }
        public AIFollowMovableAction FollowMovableAction { get; private set; }
        public AIPushMovableAction PushMovableAction { get; private set; }

        AIState currentState;
        bool firstIdle = true;

        void Awake()
        {
            AIAnimator = GetComponentInChildren<Animator>();
            Navigation = GetComponent<NavMeshAgent>();
            Detection = GetComponent<AIDetection>();
            ChaseAction = GetComponent<AIChaseAction>();
            PickUpAction = GetComponent<AIPickUpAction>();
            IdleAction = GetComponent<AIIdleAction>();
            FollowMovableAction = GetComponent<AIFollowMovableAction>();
            PushMovableAction = GetComponent<AIPushMovableAction>();
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
                new AIPickUpState(this),
                new AIFollowMovableState(this),
                new AIPushMovableState(this)
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
            if (Detection.CantFindCollectable)
            {
                if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
                {
                    firstIdle = true;
                    ChangeToState(typeof(AIIdleState));
                }
            }
            else if (!Detection.CantFindCollectable)
            {
                if (firstIdle && currentState.GetType() == typeof(AIIdleState))
                {
                    firstIdle = false;
                    ChangeToState(typeof(AIChaseState));
                }
                if (Detection.HasPathToCollectable)
                {
                    if (currentState.GetType() != typeof(AIChaseState) && currentState.GetType() != typeof(AIIdleState) && currentState.GetType() != typeof(AIPickUpState))
                    {
                        ChangeToState(typeof(AIChaseState));
                    }
                    else if (!ChaseAction.Active && currentState.GetType() == typeof(AIChaseState))
                    {
                        ChangeToState(typeof(AIIdleState));
                    }
                    else if (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState))
                    {
                        ChangeToState(typeof(AIPickUpState));
                    }
                    else if (!PickUpAction.Active && currentState.GetType() == typeof(AIPickUpState))
                    {
                        ChangeToState(typeof(AIChaseState));
                    }
                }
                else
                {
                    if (!Detection.CantFindMovable && Detection.HasPathToMovable)
                    {
                        if (currentState.GetType() != typeof(AIFollowMovableState) && currentState.GetType() != typeof(AIPushMovableState))
                        {
                            ChangeToState(typeof(AIFollowMovableState));
                        }
                        else if (!FollowMovableAction.Active && currentState.GetType() == typeof(AIFollowMovableState))
                        {
                            ChangeToState(typeof(AIPushMovableState));
                        }
                        else if (!PushMovableAction.Active && currentState.GetType() == typeof(AIPushMovableState))
                        {
                            ChangeToState(typeof(AIFollowMovableState));
                        }
                    }
                    else
                    {
                        if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
                        {
                            ChangeToState(typeof(AIIdleState));
                        }
                    }
                }
            }

            currentState.OnUpdate();
        }

        void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }
    }
}
