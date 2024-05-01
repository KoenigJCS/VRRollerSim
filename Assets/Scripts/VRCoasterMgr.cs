using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.XR.Management;
using UnityEngine.InputSystem;
using Dreamteck.Splines;
using UnityEngine.XR.Management;

public class VRCoasterMgr : MonoBehaviour
{
    [SerializeField] private SplineComputer coasterSpline;
    Dictionary<int, Node> nodelist;
    
    private void Awake() {
        if( XRGeneralSettings.Instance.Manager.activeLoader != null ){
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
