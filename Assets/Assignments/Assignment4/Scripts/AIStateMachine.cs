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
                new AIFollowMovableState(this)
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
            if (Detection.CollectableToPickUp)
            {
                if (!FollowMovableAction.Active && !ChaseAction.Active && !IdleAction.Active && !PickUpAction.Active) Navigation.SetDestination(Detection.CollectableToPickUp.transform.position);
                if (!FollowMovableAction.Active && Navigation.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    if (firstIdle && currentState.GetType() == typeof(AIIdleState))
                    {
                        firstIdle = false;
                        ChangeToState(typeof(AIChaseState));
                    }
                    else if (currentState.GetType() != typeof(AIChaseState) && currentState.GetType() != typeof(AIIdleState) && currentState.GetType() != typeof(AIPickUpState))
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
                }
                else if (Detection.Movable)
                {
                    if (!FollowMovableAction.Active) Navigation.SetDestination(Detection.Movable.transform.position);
                    if (Navigation.pathStatus != NavMeshPathStatus.PathInvalid)
                    {
                        if (!FollowMovableAction.Active) ChangeToState(typeof(AIFollowMovableState));
                    }
                    else
                    {
                        if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
                        {
                            firstIdle = true;
                            ChangeToState(typeof(AIIdleState));
                        }
                    }
                }
                else
                {
                    if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
                    {
                        firstIdle = true;
                        ChangeToState(typeof(AIIdleState));
                    }
                }
            }
            else
            {
                if (currentState.GetType() != typeof(AIIdleState) || (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState)))
                {
                    firstIdle = true;
                    ChangeToState(typeof(AIIdleState));
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
