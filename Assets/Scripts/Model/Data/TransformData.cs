using UnityEngine;

namespace Model.Data
{
    public struct TransformData
    {
        public readonly Quaternion IncrementRotation;
        public readonly float Scale;

        public TransformData(Quaternion incrementRotation, float scale)
        {
            IncrementRotation = incrementRotation;
            Scale = scale;
        }
    }
}