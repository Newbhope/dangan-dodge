#pragma warning disable 0414 // variable assigned but not used.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OnlineMenuUiController : NetworkBehaviour {

    public Button LeftButton;
    public Button RightButton;

    [SyncVar(hook = "OnChangeLeftState")]
    public bool LeftButtonSelected = false;
    [SyncVar(hook = "OnChangeRightState")]
    public bool RightButtonSelected = false;

    //TODO: handle un selecting, players joining at different times, etc.

    private void OnChangeLeftState(bool buttonSelected) {
        if (buttonSelected) {
            LeftButton.GetComponent<Image>().color = Color.red;
            LeftButton.GetComponentInChildren<Text>().text = "Please don't click me";
        } else {
            LeftButton.GetComponent<Image>().color = Color.white;
            LeftButton.GetComponentInChildren<Text>().text = "Pick Left Side";
        }
    }

    private void OnChangeRightState(bool buttonSelected) {
        if (buttonSelected) {
            RightButton.GetComponent<Image>().color = Color.blue;
            RightButton.GetComponentInChildren<Text>().text = "Please don't click me";
        }
        else {
            RightButton.GetComponent<Image>().color = Color.white;
            RightButton.GetComponentInChildren<Text>().text = "Pick Right Side";
        }
    }

}
