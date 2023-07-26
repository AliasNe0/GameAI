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
        public Animator AIAnimator { get; private set; }
        public AIDetection Detection { get; private set; }
        public AIPatrolAction PatrolAction { get; private set; }
        public AIChaseAction ChaseAction { get; private set; }
        public AIPickUpAction PickUpAction { get; private set; }
        public AIIdleAction IdleAction { get; private set; }

        void Awake()
        {
            AIAnimator = GetComponentInChildren<Animator>();
            Detection = GetComponent<AIDetection>();
            PatrolAction = GetComponent<AIPatrolAction>();
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
                new AIPatrolState(this),
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
            if (!Detection.CollectableToPickUp)
            {
                if (currentState.GetType() == typeof(AIIdleState) || (!PickUpAction.Active && currentState.GetType() == typeof(AIPickUpState)))
                {
                    ChangeToState(typeof(AIPatrolState));
                    AIAnimator.SetTrigger("PatrolState");
                    AIAnimator.ResetTrigger("ChaseState");
                    AIAnimator.ResetTrigger("IdleState");
                    AIAnimator.ResetTrigger("PickUpState");
                }
            }
            else if (Detection.CollectableToPickUp)
            {
                if (currentState.GetType() == typeof(AIPatrolState))
                {
                    ChangeToState(typeof(AIChaseState));
                    AIAnimator.ResetTrigger("PatrolState");
                    AIAnimator.SetTrigger("ChaseState");
                    AIAnimator.ResetTrigger("IdleState");
                    AIAnimator.ResetTrigger("PickUpState");
                }
                else if (!ChaseAction.Active && currentState.GetType() == typeof(AIChaseState))
                {
                    ChangeToState(typeof(AIIdleState));
                    AIAnimator.ResetTrigger("PatrolState");
                    AIAnimator.ResetTrigger("ChaseState");
                    AIAnimator.SetTrigger("IdleState");
                    AIAnimator.ResetTrigger("PickUpState");
                }
                else if (!IdleAction.Active && currentState.GetType() == typeof(AIIdleState))
                {
                    ChangeToState(typeof(AIPickUpState));
                    AIAnimator.ResetTrigger("PatrolState");
                    AIAnimator.ResetTrigger("ChaseState");
                    AIAnimator.ResetTrigger("IdleState");
                    AIAnimator.SetTrigger("PickUpState");
                }
                else if (!PickUpAction.Active && currentState.GetType() == typeof(AIPickUpState))
                {
                    ChangeToState(typeof(AIPatrolState));
                    AIAnimator.SetTrigger("PatrolState");
                    AIAnimator.ResetTrigger("ChaseState");
                    AIAnimator.ResetTrigger("IdleState");
                    AIAnimator.ResetTrigger("PickUpState");
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
