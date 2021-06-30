using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> Tutorials = new List<Tutorial>();
    public Text expText;
    public Text ErroresData;
    public Text CorrectosData;
    public Text TranscurridoData;
    public Text ErroresDetalle;
    public Text CorrectosDetalle;
    public Text TranscurridoDetalle;
    public Text TutorialNum;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();
            if (instance == null)
                Debug.Log("there is no tutorial manager");
            return instance;
        }
    }
    private Tutorial currentTutorial;
    void Start()
    {
        SetNextTutorial(0);
    }

    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }
    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.Order + 1);
    }
    public void SetNextTutorial(int currentOrder)
    {
        TutorialNum.text = (currentOrder + 1).ToString();
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }
        expText.text = currentTutorial.Explanation;
    }
    public void CompletedAllTutorials()
    {
        expText.text = "TUTORIAL COMPLETADO";
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasDetalleFinal").SetActive(true);
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador").SetActive(false);
        ErroresDetalle.text = ErroresData.text;
        CorrectosDetalle.text = CorrectosData.text;
        TranscurridoDetalle.text = TranscurridoData.text;
    }
    public Tutorial GetTutorialByOrder(int Order)
    {
        for (int i = 0;i<Tutorials.Count;i++)
        {
            if (Tutorials[i].Order == Order)
                return Tutorials[i];
        }
        return null;
    }
}
