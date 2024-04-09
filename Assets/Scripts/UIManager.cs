using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button FLButton;
    [SerializeField] private Button LButton;
    [SerializeField] private Button MButton;
    [SerializeField] private Button RButton;
    [SerializeField] private Button FRButton;
    [SerializeField] private Button PlacePeice;
    [SerializeField] private Button DestroyPeice;
    int newPieceTurn=45;
    int newPieceRoll=0;
    int newPiecePitch=0;
    bool doubleClickFlag = false;
    // Start is called before the first frame update
    void Awake()
    {
        FLButton.onClick.AddListener(() => {newPieceTurn =-90;});
        LButton.onClick.AddListener(() => {newPieceTurn =-45;});
        MButton.onClick.AddListener(() => {newPieceTurn = 0;});
        RButton.onClick.AddListener(() => {newPieceTurn = 45;});
        FRButton.onClick.AddListener(() => {newPieceTurn = 90;});
        
        PlacePeice.onClick.AddListener(() => 
        {
            if(!doubleClickFlag)
            {
                TrackManager.inst.AddPeiceToTrack(new Vector3(newPieceRoll,newPieceTurn,newPiecePitch));
                doubleClickFlag=true;
            }
        });
        DestroyPeice.onClick.AddListener(() => 
        {
            if(!doubleClickFlag)
            {
                TrackManager.inst.RemovePeiceFromTrack();
                doubleClickFlag=true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(doubleClickFlag)
            doubleClickFlag=false;
    }

    public void UpdateValue(ref int modify, int newValue)
    {
        modify = newValue;
    }

    
}
