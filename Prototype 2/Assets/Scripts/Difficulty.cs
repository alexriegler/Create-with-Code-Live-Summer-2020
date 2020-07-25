using UnityEngine;

public static class Difficulty
{
    [SerializeField]
    private static float secondsToMaxDifficulty = 40;

    // Returns the current difficulty percent
    /// <summary>
    /// Calculates the difficulty percent, which is between 0 and 1.
    /// </summary>
    /// <returns>The difficulty percent.</returns>
    public static float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
