using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class vrDeviceDetector : MonoBehaviour
{
    private void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name'{0}' and role '{1}'", device.name, device.role.ToString()));
            if (device.name == "Oculus Quest2")
            {
                Debug.Log("detected device");
            }
        }
    }
}
