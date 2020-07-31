using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public float ScrollSpeed { get; private set; } = 20;

    // Sets the scroll speed to zero
    public void StopScrolling() => ScrollSpeed = 0;
}
