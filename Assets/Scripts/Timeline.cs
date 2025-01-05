using UnityEngine;

public class Timeline : MonoBehaviour
{
    private Track[] _tracks;
    public float startDelay = 1.0f;
    public int bpm = 80;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _tracks = GetComponentsInChildren<Track>();
    }

    public void PlayTimeLine()
    {
        double startTime = AudioSettings.dspTime + startDelay;
        foreach (Track track in _tracks)
            track.PlayTrack(bpm, startTime);
    }
}
