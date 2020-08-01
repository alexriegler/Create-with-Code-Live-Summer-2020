using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    // The speed of all scrolling objects
    public float ScrollSpeed { get; private set; }

    private PlayerController player;
    private float defaultScrollSpeed = 20;

    void Start()
    {
        ScrollSpeed = defaultScrollSpeed;

        player = FindObjectOfType<PlayerController>();

        player.OnPlayerStartDash += () => SpeedUpScrollSpeed(player.DashMultiplier);
        player.OnPlayerEndDash += ResetScrollSpeed;
        player.OnPlayerDeath += StopScrolling;
    }

    // Sets the scroll speed to the default scroll speed
    /// <summary>
    /// Sets the scroll speed to the default scroll speed.
    /// </summary>
    public void ResetScrollSpeed()
    {
        ScrollSpeed = defaultScrollSpeed;
    }

    // Multiplies the current scroll speed by a multiplier
    /// <summary>
    /// Multiplies the current scroll speed by a multiplier.
    /// </summary>
    /// <param name="multiplier">The factor to multiply the scroll speed by.</param>
    private void SpeedUpScrollSpeed(float multiplier)
    {
        ScrollSpeed *= multiplier;
    }

    // Sets the scroll speed to zero
    /// <summary>
    /// Sets the scroll speed to zero.
    /// </summary>
    private void StopScrolling()
    {
        ScrollSpeed = 0;
    }
}
