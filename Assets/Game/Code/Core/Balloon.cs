using System;
using Game.Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Code.Core
{
    public class Balloon : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Balloon> OnClick; 
        public BalloonData Data;
        public Vector2 StartPosition;
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public float Rotation
        {
            get => transform.rotation.eulerAngles.z;
            set => transform.rotation = Quaternion.Euler(0, 0, value);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
    }
}