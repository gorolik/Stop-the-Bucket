using UnityEngine;

namespace Sources.Behaviour.UI
{
    public class LoadingProgressIndicator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 180;

        private void Update() => 
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
