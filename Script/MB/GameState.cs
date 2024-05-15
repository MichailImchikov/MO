using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState
{
    public EcsWorld ecsWorld;
    public static GameState Instance;
    public int CountVariables;
    public TextMeshProUGUI answer;
    private List<CanvasMB> canvases;
    public static void Init(EcsWorld ecsWorld)
    {
        Instance= new GameState();
        Instance.ecsWorld= ecsWorld;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("UI");
        Instance.canvases = new List<CanvasMB>();
        foreach(var canvas in objectsWithTag)
        {
            var ThisCanvasMB = canvas.GetComponent<CanvasMB>();
            ThisCanvasMB._thisCanvas = canvas.GetComponent<Canvas>();
            Instance.canvases.Add(ThisCanvasMB);
            
        }
    }
    public CanvasMB GetCanvas<CanvasType>() where CanvasType:CanvasMB
    {
        foreach(var canvas in canvases)
        {
            if (canvas is CanvasType) return canvas as CanvasType;
        }
        return null;
    }
}
