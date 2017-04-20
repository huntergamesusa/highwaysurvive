using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EveryplayRecButtonsNate : MonoBehaviour
{

	public GameObject[] recordButtons;
	public GameObject[] stopButtons;

    private bool faceCamPermissionGranted = false;
    private bool startFaceCamWhenPermissionGranted = false;

	public bool showGUI;










    void Awake()
    {
 
        // Set initially
        if(!Everyplay.IsRecordingSupported()) {
//            startRecordingButton.enabled = false;
//            stopRecordingButton.enabled = false;
        }

        Everyplay.RecordingStarted += RecordingStarted;
        Everyplay.RecordingStopped += RecordingStopped;
        Everyplay.ReadyForRecording += ReadyForRecording;
        Everyplay.FaceCamRecordingPermission += FaceCamRecordingPermission;
    }

    void Destroy()
    {
        Everyplay.RecordingStarted -= RecordingStarted;
        Everyplay.RecordingStopped -= RecordingStopped;
        Everyplay.ReadyForRecording -= ReadyForRecording;
        Everyplay.FaceCamRecordingPermission -= FaceCamRecordingPermission;
    }

  



    public void StartRecording()
    {
		if (Everyplay.IsRecording()) {
			Everyplay.StopRecording();
			RecordingStopped ();

		} else {
			Everyplay.StartRecording ();
			RecordingStarted ();
		}
    }

  

    public void RecordingStarted()
    {
		for (int i = 0; i < recordButtons.Length; i++) {
			recordButtons[i].SetActive (false);

		}
		for (int i = 0; i < stopButtons.Length; i++) {
			stopButtons[i].SetActive (true);

		}
//        ReplaceVisibleButton(startRecordingButton, stopRecordingButton);
//
//        SetButtonVisible(shareVideoButton, false);
//        SetButtonVisible(editVideoButton, false);
//        SetButtonVisible(faceCamToggleButton, false);
    }

    private void RecordingStopped()
    {
		for (int i = 0; i < recordButtons.Length; i++) {
			recordButtons[i].SetActive (false);

		}
		for (int i = 0; i < stopButtons.Length; i++) {
			stopButtons[i].SetActive (true);

		}
//        ReplaceVisibleButton(stopRecordingButton, startRecordingButton);
//
//        SetButtonVisible(shareVideoButton, true);
//        SetButtonVisible(editVideoButton, true);
//        SetButtonVisible(faceCamToggleButton, true);
    }

    private void ReadyForRecording(bool enabled)
    {
		for (int i = 0; i < recordButtons.Length; i++) {
			recordButtons[i].SetActive (false);

		}
		for (int i = 0; i < stopButtons.Length; i++) {
			stopButtons[i].SetActive (true);

		}
//        startRecordingButton.enabled = enabled;
//        stopRecordingButton.enabled = enabled;
    }

    private void FaceCamRecordingPermission(bool granted)
    {
        faceCamPermissionGranted = granted;

        if(startFaceCamWhenPermissionGranted) {
//            faceCamToggleButton.toggled = granted;
            Everyplay.FaceCamStartSession();
            if(Everyplay.FaceCamIsSessionRunning()) {
                startFaceCamWhenPermissionGranted = false;
            }
        }
    }

    public void FaceCamToggle()
    {
        if(faceCamPermissionGranted) {
//            faceCamToggleButton.toggled = !faceCamToggleButton.toggled;

//            if(faceCamToggleButton.toggled) {
                if(!Everyplay.FaceCamIsSessionRunning()) {
                    Everyplay.FaceCamStartSession();
                }
//            }
            else {
                if(Everyplay.FaceCamIsSessionRunning()) {
                    Everyplay.FaceCamStopSession();
                }
            }
        }
        else {
            Everyplay.FaceCamRequestRecordingPermission();
            startFaceCamWhenPermissionGranted = true;
        }
    }

    public void OpenEveryplay()
    {
        Everyplay.Show();
    }

    public void EditVideo()
    {
        Everyplay.PlayLastRecording();
    }

    public void ShareVideo()
    {
        Everyplay.ShowSharingModal();
    }


}
