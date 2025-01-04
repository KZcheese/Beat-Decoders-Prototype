using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private List<bool> _notes;
    public List<bool> sol;
    private AudioSource _instrument;

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
        _notes = new List<bool>(sol.Count);
    }
    
    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < _notes.Count; i++)
        {
            _instrument.PlayScheduled(startTime + (i * bpm));
        }
    }

    public void SetBeats(List<bool> beats)
    {
        _notes = beats;
    }

    public int GetBeatCount()
    {
        return _notes.Count;
    }
}
