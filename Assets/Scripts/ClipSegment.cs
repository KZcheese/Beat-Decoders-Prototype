using UnityEngine;

[RequireComponent(typeof(SegmentUI))]
public class ClipSegment : DraggableSegment
{
    private Vector2 _startLocation;

    public override void Pickup()
    {
        _startLocation = transform.position;
    }

    public override void Drop()
    {
        base.Drop();
        transform.position = _startLocation;
    }
}