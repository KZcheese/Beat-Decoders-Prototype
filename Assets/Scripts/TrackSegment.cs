using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    private List<bool> _beats;
    public List<bool> sol;
    private AudioSource _instrument;

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
        _beats = new List<bool>(sol.Count);
    }
    
    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < _beats.Count; i++)
        {
            _instrument.PlayScheduled(startTime + (i * bpm));
        }
    }

    public void SetBeats(List<bool> beats)
    {
        _beats = beats;
    }

    public int GetBeatCount()
    {
        return _beats.Count;
    }
}
