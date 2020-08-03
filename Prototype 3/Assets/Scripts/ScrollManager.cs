using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    // The speed of all scrolling objects
    public float ScrollSpeed { get; private set; }

    private PlayerController player;
    private float defaultScrollSpeed = 20;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        player.OnPlayerStartDash += () => SpeedUpScrollSpeed(player.DashMultiplier);
        player.OnPlayerEndDash += SetScrollSpeed;
        player.OnPlayerDeath += StopScrolling;
    }

    // Sets the scroll speed to the default scroll speed
    /// <summary>
    /// Sets the scroll speed to the default scroll speed.
    /// </summary>
    public void SetScrollSpeed()
    {
        ScrollSpeed = defaultScrollSpeed;
    }

    // Sets the scroll speed to a specific scroll speed greater than zero
    /// <summary>
    /// Sets the scroll speed to a specific scroll speed greater than zero.
    /// </summary>
    /// <param name="scrollSpeed">The speed of the scroll.</param>
    public void SetScrollSpeed(float scrollSpeed)
    {
        if (scrollSpeed > 0)
        {
            ScrollSpeed = scrollSpeed;
        }
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
