using System;
using UnityEngine;

namespace Sources.StaticData.Levels
{
    [Serializable]
    public class LevelSettings
    {
        [Header("Line Settings")]
        [SerializeField] [Range(0.5f, 2.6f)] private float _successLineRange = 2f;
        [Header("Bucket Settings")]
        [SerializeField] [Range(10f, 80f)] private float _bucketMaxSpeed = 20f;
        [SerializeField] [Range(2f, 32f)] private float _bucketAcceleration = 10f;

        public float SuccessLineRange => _successLineRange;
        public float BucketMaxSpeed => _bucketMaxSpeed;
        public float BucketAcceleration => _bucketAcceleration;
    }
}