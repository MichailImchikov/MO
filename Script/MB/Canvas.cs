using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CanvasMB : MonoBehaviour
{
    [HideInInspector] public Canvas _thisCanvas;
    virtual public void Open()
    {
        _thisCanvas.enabled = true;
    }
    virtual public void Close()
    {
        _thisCanvas.enabled = false;
    }
}
