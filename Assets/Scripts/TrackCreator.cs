using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PieceData
{
    public Vector3 location;
    public Vector3 rotation;
    public PieceData(Vector3 n_location, Vector3 n_rotation)
    {
        location = n_location;
        rotation = n_rotation;
    }
}

public class TrackCreator : MonoBehaviour
{
    public int subDivides = 8;
    [SerializeField] private GameObject trackPiecePrefab;
    [SerializeField] private GameObject subParent;
    [SerializeField] private Transform trackParent;
    public static TrackCreator inst;
    private void Awake() {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PieceData MakeTrack(Vector3 startLocation, Vector3 startRotation, Vector3 newRotation, float length = 8, bool isChain = false)
    {
        Vector3 partialRotation = (newRotation-startRotation)/(subDivides-1);
        Vector3 nextLocation = startLocation;
        float scaleFactor = 4*length / subDivides;
        GameObject segment = Instantiate(subParent,startLocation,Quaternion.identity,trackParent);
        for(int i = 0;i<subDivides;i++)
        {
            Quaternion nextRotationQuatr = Quaternion.Euler(startRotation+(partialRotation*i));
            GameObject newTrack = Instantiate(trackPiecePrefab,nextLocation,nextRotationQuatr,segment.transform);
            newTrack.transform.localScale=new Vector3(scaleFactor*newTrack.transform.localScale.x,4*newTrack.transform.localScale.y,4*newTrack.transform.localScale.z);
            nextLocation+=nextRotationQuatr*Vector3.right*scaleFactor;
        }
        segment.GetComponent<SegmentDataHolder>().Init(startLocation,startRotation,nextLocation,newRotation);
        
        return new PieceData(nextLocation,newRotation);
    }

    public PieceData MakeTrack(PieceData trackStart, Vector3 newRotation, int length = 8, bool isChain = false)
    {
        return MakeTrack(trackStart.location,trackStart.rotation,newRotation,length,isChain);
    }   
}
