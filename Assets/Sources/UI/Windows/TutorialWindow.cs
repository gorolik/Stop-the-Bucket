using System;
using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.UI.Windows
{
    public class TutorialWindow : WindowBase
    {
        [SerializeField] private TutorStep[] _steps;
        [SerializeField] private Button _clickableArea;

        private ILevelStateMachine _levelStateMachine;
        
        private int _currentStep;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine) => 
            _levelStateMachine = levelStateMachine;

        protected override void OnStart()
        {
            ClearSteps();
            OnClicked();
        }

        protected override void SubscribeUpdates() => 
            _clickableArea.onClick.AddListener(OnClicked);

        protected override void Cleanup() => 
            _clickableArea.onClick.RemoveListener(OnClicked);

        private void OnClicked()
        {
            if (_currentStep < _steps.Length)
            {
                ShowStepObjects(_currentStep);
                _currentStep++;
            }
            else
            {
                TutorCompleted();
            }
        }

        private void ClearSteps()
        {
            foreach (TutorStep tutorStep in _steps)
                foreach (GameObject stepObject in tutorStep.StepObjects)
                    stepObject.SetActive(false);
        }

        private void ShowStepObjects(int step)
        {
            foreach (GameObject stepObject in _steps[step].StepObjects) 
                stepObject.SetActive(true);
        }

        private void TutorCompleted()
        {
            _levelStateMachine.Enter<CountingState>();
            Close();
        }

        [Serializable]
        public struct TutorStep
        {
            [SerializeField] private GameObject[] _stepObjects;

            public GameObject[] StepObjects => _stepObjects;
        }
    }
}