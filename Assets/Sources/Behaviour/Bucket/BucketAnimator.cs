using UnityEngine;

namespace Sources.Behaviour.Bucket
{
    [RequireComponent(typeof(Animator))]
    public class BucketAnimator : MonoBehaviour
    {
        private static readonly int HitHash = Animator.StringToHash("Hit");
        
        private Animator _animator;
        
        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void Hit() =>
            _animator.SetBool(HitHash, true);
    }
}