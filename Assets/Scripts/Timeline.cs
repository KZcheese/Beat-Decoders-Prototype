using UnityEngine;

public class Timeline : MonoBehaviour
{
    private Track[] _tracks;
    public float startDelay = 0.1f;
    public int bpm = 80;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _tracks = GetComponentsInChildren<Track>();
    }

    public void PlayTimeLine()
    {
        double startTime = AudioSettings.dspTime + startDelay;
        double interval = 1.0 / bpm * 60 / 4.0; // assumes 4/4 for convenience
        Debug.Log("Start Time: " + startTime);
        Debug.Log("Interval: " + interval);
        foreach (Track track in _tracks)
            track.PlayTrack(interval, startTime);
    }
}
