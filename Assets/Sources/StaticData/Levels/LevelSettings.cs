using System;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class LevelSettings
    {
        [Header("Line Settings")]
        [SerializeField] [Range(0.1f, 1.5f)] private float _successLineRange = 0.5f;
        [Header("Bucket Settings")]
        [SerializeField] [Range(10f, 80f)] private float _bucketMaxSpeed = 20f;
        [SerializeField] [Range(2f, 32f)] private float _bucketAcceleration = 10f;

        public float SuccessLineRange => _successLineRange;
        public float BucketMaxSpeed => _bucketMaxSpeed;
        public float BucketAcceleration => _bucketAcceleration;
    }
}