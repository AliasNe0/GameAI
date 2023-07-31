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
        bool lastIdle = true;
        bool cantFindMovableIdle = false;

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
            AIAnimator.SetTrigger("IdleState");
        }

        void Update()
        {
            if (Detection.CantFindCollectable)
            {
                if (lastIdle)
                {
                    lastIdle = false;
                    firstIdle = true;
                    ChangeToState(typeof(AIIdleState));
                    AIAnimator.ResetTrigger("Walk");
                    AIAnimator.SetTrigger("Idle");
                    AIAnimator.ResetTrigger("Crouch");
                }
            }
            else if (!Detection.CantFindCollectable)
            {
                if (firstIdle && currentState.GetType() == typeof(AIIdleState))
                {
                    firstIdle = false;
                    lastIdle = true;
                    ChangeToState(typeof(AIChaseState));
                    AIAnimator.SetTrigger("Walk");
                    AIAnimator.ResetTrigger("Idle");
                    AIAnimator.ResetTrigger("Crouch");
                }
                if (Detection.HasPathToCollectable)
                {
                    if (currentState.GetType() == typeof(AIChaseState) && currentState.GetType() == typeof(AIIdleState) && currentState.GetType() == typeof(AIPickUpState))
                    {
                        ChangeToState(typeof(AIChaseState));
                        AIAnimator.SetTrigger("Walk");
                        AIAnimator.ResetTrigger("Idle");
                        AIAnimator.ResetTrigger("Crouch");
                    }
                    else if(!ChaseAction.Active && currentState.GetType() == typeof(AIChaseState))
                    {
                        ChangeToState(typeof(AIIdleState));
                        AIAnimator.ResetTrigger("Walk");
                        AIAnimator.SetTrigger("Idle");
                        AIAnimator.ResetTrigger("Crouch");
                    }
                    else if (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState))
                    {
                        ChangeToState(typeof(AIPickUpState));
                        AIAnimator.ResetTrigger("Walk");
                        AIAnimator.ResetTrigger("Idle");
                        AIAnimator.SetTrigger("Crouch");
                    }
                    else if (!PickUpAction.Active && currentState.GetType() == typeof(AIPickUpState))
                    {
                        ChangeToState(typeof(AIChaseState));
                        AIAnimator.SetTrigger("Walk");
                        AIAnimator.ResetTrigger("Idle");
                        AIAnimator.ResetTrigger("Crouch");
                    }
                }
                else
                {
                    if (!Detection.CantFindMovable && Detection.HasPathToMovable)
                    {
                        cantFindMovableIdle = false;
                        if (currentState.GetType() != typeof(AIFollowMovableState) && currentState.GetType() != typeof(AIPushMovableState))
                        {
                            ChangeToState(typeof(AIFollowMovableState));
                            AIAnimator.SetTrigger("Walk");
                            AIAnimator.ResetTrigger("Idle");
                            AIAnimator.ResetTrigger("Crouch");
                        }
                        else if (!FollowMovableAction.Active && currentState.GetType() == typeof(AIFollowMovableState))
                        {
                            ChangeToState(typeof(AIPushMovableState));
                            AIAnimator.SetTrigger("Walk");
                            AIAnimator.ResetTrigger("Idle");
                            AIAnimator.ResetTrigger("Crouch");
                        }
                        else if (!PushMovableAction.Active && currentState.GetType() == typeof(AIPushMovableState))
                        {
                            ChangeToState(typeof(AIFollowMovableState));
                            AIAnimator.SetTrigger("Walk");
                            AIAnimator.ResetTrigger("Idle");
                            AIAnimator.ResetTrigger("Crouch");
                        }
                    }
                    else if (!cantFindMovableIdle)
                    {
                        cantFindMovableIdle = true;
                        ChangeToState(typeof(AIIdleState));
                        AIAnimator.ResetTrigger("Walk");
                        AIAnimator.SetTrigger("Idle");
                        AIAnimator.ResetTrigger("Crouch");
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
