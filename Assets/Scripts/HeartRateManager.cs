using UnityEngine;

public class HeartRateManager : MonoBehaviour
{
    public float heartrate = 100;

    public float GetHeartRate()
    {
        return heartrate;
    }

    public void SetHeartRate(float rate)
    {
        heartrate = rate;
    }
}
