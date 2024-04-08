using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
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
    [SerializeField] private Transform trackParent;
    // Start is called before the first frame update
    void Start()
    {
        MakeTrack(Vector3.right*10,Vector3.zero,new Vector3(0,0,0));
        MakeTrack(MakeTrack(Vector3.left*10,Vector3.zero,new Vector3(0,90,0)),new Vector3(0,90,0));
        MakeTrack(Vector3.forward*10,Vector3.zero,new Vector3(0,90,0),4f);
        MakeTrack(Vector3.forward*20,Vector3.zero,new Vector3(0,0,90),16f);
        MakeTrack(Vector3.forward*-10,Vector3.zero,new Vector3(90,0,90),1);
        MakeTrack(Vector3.forward*-20,Vector3.zero,new Vector3(180,0,90),1);
        MakeTrack(MakeTrack(MakeTrack(MakeTrack(Vector3.forward*-50,Vector3.zero,new Vector3(0,1,90)),new Vector3(0,2,180)),new Vector3(0,1,270)),new Vector3(0,0,360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PieceData MakeTrack(Vector3 startLocation, Vector3 startRotation, Vector3 newRotation, float length = 8, bool isChain = false)
    {
        Vector3 partialRotation = (newRotation-startRotation)/(subDivides-1);
        Vector3 nextLocation = startLocation;
        float scaleFactor = length / subDivides;
        for(int i = 0;i<subDivides;i++)
        {
            Quaternion nextRotationQuatr = Quaternion.Euler(startRotation+(partialRotation*i));
            GameObject newTrack = Instantiate(trackPiecePrefab,nextLocation,nextRotationQuatr,trackParent);
            newTrack.transform.localScale=new Vector3(scaleFactor*newTrack.transform.localScale.x,newTrack.transform.localScale.y,newTrack.transform.localScale.z);
            nextLocation+=nextRotationQuatr*Vector3.right*scaleFactor;
        }
        return new PieceData(nextLocation,newRotation);
    }

    public PieceData MakeTrack(PieceData trackStart, Vector3 newRotation, int length = 8, bool isChain = false)
    {
        return MakeTrack(trackStart.location,trackStart.rotation,newRotation,length,isChain);
    }   
}
