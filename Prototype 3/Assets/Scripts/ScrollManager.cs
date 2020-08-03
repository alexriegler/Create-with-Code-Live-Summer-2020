using System.Collections;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    // The speed of all scrolling objects
    public float ScrollSpeed { get; private set; }

    private PlayerController player;
    private GameManager gm;
    private float defaultScrollSpeed = 20;
    private float scrollAcceleration = 20;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnPlayerStartDash += () => SpeedUpScrollSpeed(player.DashMultiplier);
        player.OnPlayerEndDash += SetScrollSpeed;

        gm = FindObjectOfType<GameManager>();
        gm.OnGameStart += () => StartCoroutine(StartScroll());
        gm.OnGameOver += StopScrolling;
    }

    // Starts the scroll gradually, following a curve
    private IEnumerator StartScroll()
    {
        float x = 0;
        while (ScrollSpeed < defaultScrollSpeed)
        {
            // cx^3 curve, c is a constant
            ScrollSpeed = Mathf.Clamp(scrollAcceleration * Mathf.Pow(x, 3), 0, defaultScrollSpeed);
            x += Time.deltaTime;
            yield return null;
        }
    }

    // Sets the scroll speed to the default scroll speed
    /// <summary>
    /// Sets the scroll speed to the default scroll speed.
    /// </summary>
    private void SetScrollSpeed()
    {
        ScrollSpeed = defaultScrollSpeed;
    }

    // Sets the scroll speed to a specific scroll speed greater than zero
    /// <summary>
    /// Sets the scroll speed to a specific scroll speed greater than zero.
    /// </summary>
    /// <param name="scrollSpeed">The speed of the scroll.</param>
    private void SetScrollSpeed(float scrollSpeed)
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
