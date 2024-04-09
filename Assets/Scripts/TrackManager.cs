using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;




public class TrackManager : MonoBehaviour
{
    public List<SegmentDataHolder> trackList;
    public SegmentDataHolder lastPeice;
    // Start is called before the first frame update
    public static TrackManager inst;
    public int curTrackIndex = 0;
    private void Awake() {
        inst = this;
    }
    void Start()
    {
        trackList=new();
        TrackCreator.inst.MakeTrack(Vector3.zero,Vector3.zero,new Vector3(0,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int AddToTrackList(SegmentDataHolder newSeg)
    {
        trackList.Add(newSeg);
        lastPeice = newSeg;
        return curTrackIndex++;
    }

    public void AddPeiceToTrack(Vector3 relativeAngleChange)
    {
        TrackCreator.inst.MakeTrack(lastPeice.GetEnd(),relativeAngleChange+lastPeice.GetEnd().rotation);
    }

    public void RemovePeiceFromTrack()
    {
        trackList.RemoveAt(trackList.Count-1);
        Destroy(lastPeice.gameObject);
        curTrackIndex--;
    }
}
