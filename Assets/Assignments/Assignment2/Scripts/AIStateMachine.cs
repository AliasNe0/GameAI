using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT2
{
    public class AIStateMachine : MonoBehaviour
    {
        AIState currentState;
        public List<AIState> States { get; private set; }
        public Animator AIAnimator { get; private set; }
        public AIDetection Detection { get; private set; }
        public AIChaseAction ChaseAction { get; private set; }
        public AIPickUpAction PickUpAction { get; private set; }
        public AIIdleAction IdleAction { get; private set; }

        void Awake()
        {
            AIAnimator = GetComponentInChildren<Animator>();
            Detection = GetComponent<AIDetection>();
            ChaseAction = GetComponent<AIChaseAction>();
            PickUpAction = GetComponent<AIPickUpAction>();
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
                new AIPickUpState(this)
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
            if (ChaseAction.TargetIsReached && currentState.GetType() == typeof(AIChaseState))
            {
                ChangeToState(typeof(AIIdleState));
                AIAnimator.ResetTrigger("ChaseState");
                AIAnimator.SetTrigger("IdleState");
                AIAnimator.ResetTrigger("PickUpState");
            }
            else if (Detection.CollectableToPickUp && IdleAction.IdleFinished && currentState.GetType() == typeof(AIIdleState))
            {
                ChangeToState(typeof(AIPickUpState));
                AIAnimator.ResetTrigger("ChaseState");
                AIAnimator.ResetTrigger("IdleState");
                AIAnimator.SetTrigger("PickUpState");
            }
            else if (IdleAction.IdleFinished && currentState.GetType() == typeof(AIIdleState))
            {
                ChangeToState(typeof(AIChaseState));
                AIAnimator.SetTrigger("ChaseState");
                AIAnimator.ResetTrigger("IdleState");
                AIAnimator.ResetTrigger("PickUpState");
            }
            else if (PickUpAction.PickedUp && currentState.GetType() == typeof(AIPickUpState))
            {
                ChangeToState(typeof(AIChaseState));
                AIAnimator.SetTrigger("ChaseState");
                AIAnimator.ResetTrigger("IdleState");
                AIAnimator.ResetTrigger("PickUpState");
            }
            currentState.OnUpdate();
        }

        void FixedUpdate()
        {
            currentState.OnFixedUpdate();
        }
    }
}
