using System;
using System.Collections;
using UnityEngine;

namespace Game.Code.Core
{
    public class BalloonExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _lifetime = 0.5f;
        
        public void PlayAt(Vector2 position, Action onComplete = null)  
        {
            transform.position = position;
            _particleSystem.Play();
            StartCoroutine(ExecuteCallback(onComplete));
        }

        private IEnumerator ExecuteCallback(Action onComplete)
        {
            yield return new WaitForSeconds(_lifetime);
            onComplete?.Invoke();
        }
    }
}