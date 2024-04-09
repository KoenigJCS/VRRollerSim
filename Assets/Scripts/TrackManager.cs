using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TrackSegment
{
    public Vector3 locationStart;
    public Vector3 locationEnd;
    public Vector3 rotationStart;
    public Vector3 rotationEnd;
    public TrackSegment(Vector3 n_locationStart, Vector3 n_locationEnd, Vector3 n_rotation)
    {
        location = n_location;
        rotation = n_rotation;
    }
}


public class TrackManager : MonoBehaviour
{
    public List<TrackSegment> trackList
    // Start is called before the first frame update
    public static TrackManager inst;
    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
