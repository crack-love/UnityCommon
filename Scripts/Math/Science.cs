using UnityEngine;

namespace UnityCommon
{
    public static class Science
    {
        public static float InitialVelocityFromDesireHeight(float gravityMps2, float desireHeightM)
        {
            return gravityMps2 * Mathf.Pow(2f * desireHeightM / gravityMps2, 0.5f);
        }
    }
}
