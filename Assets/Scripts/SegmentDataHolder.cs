using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentDataHolder : MonoBehaviour
{
    private Vector3 locationStart;
    private Vector3 locationEnd;
    private Vector3 rotationStart;
    private Vector3 rotationEnd;
    public int ID = -1;
    public void Init(Vector3 n_locationStart, Vector3 n_locationEnd, Vector3 n_rotationStart, Vector3 n_rotationEnd)
    {
        locationStart = n_locationStart;
        locationEnd = n_locationEnd;
        rotationStart = n_rotationStart;
        rotationEnd = n_rotationEnd;
        ID = TrackManager.inst.AddToTrackList(this);
    }
    
    public PieceData GetStart()
    {
        return new PieceData(locationStart,rotationStart);
    }

    public PieceData GetEnd()
    {
        return new PieceData(locationEnd,rotationEnd);
    }
}
