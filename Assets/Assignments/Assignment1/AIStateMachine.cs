using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AIStateMachine : MonoBehaviour
    {
        protected AIState currentState;
        public List<AIState> states = new();

        public void ChangeState(int index)
        {
            if (states.Count > index)
            {
                currentState.OnExit();
                currentState = states[index];
                currentState.OnEnter();
            }
        }

        void CreateStates()
        {
            AIState steering = new AISteeringState(this);
            currentState = steering;
            states.Add(steering);
        }

        void Start()
        {
            CreateStates();

            if (states.Count == 0)
            {
                throw new ArgumentException("You did not specify any states. Create at least one state in CreateStates method.");
            }

            foreach (AIState state in states)
            {
                state.OnStart();
            }

            if (currentState == null)
            {
                Debug.Log("Initial state not set. Defaulting to first state.");
                currentState = states[0];
            }

            currentState.OnEnter();
        }

        void Update()
        {
            currentState.OnUpdate();
        }

        void OnEnable()
        {
            foreach (AIState state in states)
            {
                state.OnEnable();
            }
        }

        void OnDisable()
        {
            foreach (AIState state in states)
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
