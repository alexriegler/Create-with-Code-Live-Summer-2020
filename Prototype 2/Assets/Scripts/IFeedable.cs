using UnityEngine;

public interface IFeedable
{
    void Feed(int feedPower, RaycastHit hit);
}