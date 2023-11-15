using Sources.Infrastructure.StateMachines.Level;
using Sources.Infrastructure.StateMachines.Level.States;
using Sources.Services.LevelResult;
using UnityEngine;
using Zenject;

namespace Sources.Behaviour
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _winSound;
        
        private ILevelStateMachine _levelStateMachine;
        private ILevelResultService _levelResult;
        private People _people;

        [Inject]
        public void Construct(ILevelStateMachine levelStateMachine, ILevelResultService levelResult, People people)
        {
            _levelStateMachine = levelStateMachine;
            _levelResult = levelResult;
            _people = people;
        }

        public void CatchBucket(float height)
        {
            if (_levelResult.IsWin(height))
            {
                _levelStateMachine.Enter<WinState, int>(_levelResult.GetStarsCount(height));
                
                _audioSource.PlayOneShot(_winSound);
            }
            else
            {
                _levelStateMachine.Enter<LoseState>();
            }
            
            _people.PlayWinEmotion();
        }
        
        public void PeopleHit()
        {
            _levelStateMachine.Enter<LoseState>();
            
            _people.PlayLoseEmotion();
        }
    }
}