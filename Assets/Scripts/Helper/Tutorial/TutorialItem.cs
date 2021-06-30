using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    public GameObject Item;
    public GameObject ItemPosition;
    public bool IsSelected;
    public bool IsPlaced;
    public bool IsEffected;
    public bool IsPainted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            if (OVRInput.Get(OVRInput.RawButton.A))
            {
                IsSelected = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (!other.CompareTag("LeftHand") && !other.CompareTag("RightHand"))
        {
            if (other.transform.name == ItemPosition.name)
            {
                TagPosition(Item.tag);
            }
            if (other.CompareTag("BrushSilicona") || other.CompareTag("PaletaBase"))
            {
                NamePainted(Item.name);
            }
        }
       
        else
        {
            if(Item.tag == "MLuzPalanca")
            {
                IsEffected = true;
                Animator anim = GameObject.Find("Game/Design/InterruptorElectronagmetico/Palanca").GetComponent<Animator>();
                anim.SetTrigger("LuzPalancaTrigger");
            }
          
        }
    }
    //RECURSOS
    public void TagPosition(string tag)
    {
        switch (tag)
        {
            #region PUERTA
            //PUERTA
            case "MPBIzquierdo":
                IsPlaced = true;
                break;
            case "MPBDerecho":
                IsPlaced = true;
                break;
            case "MPBSuperior":
                IsPlaced = true;
                break;
            case "MPBPuerta":
                IsPlaced = true;
                break;
            case "MPBizagra1":
                IsPlaced = true;
                break;
            case "MPBizagra2":
                IsPlaced = true;
                break;
            case "MPBizagra3":
                IsPlaced = true;
                break;
            case "MPTarugoSuperior1":
                IsPlaced = true;
                break;
            case "MPTarugoSuperior2":
                IsPlaced = true;
                break;
            case "MPTarugoLateralI1":
                IsPlaced = true;
                break;
            case "MPTarugoLateralI2":
                IsPlaced = true;
                break;
            case "MPTarugoLateralI3":
                IsPlaced = true;
                break;
            case "MPTarugoLateraD1":
                IsPlaced = true;
                break;
            case "MPTarugoLateraD2":
                IsPlaced = true;
                break;
            case "MPTarugoLateraD3":
                IsPlaced = true;
                break;
            case "MPTornilloSuperior1":
                IsPlaced = true;
                break;
            case "MPTornilloSuperior2":
                IsPlaced = true;
                break;
            case "MPTornilloLateralI1":
                IsPlaced = true;
                break;
            case "MPTornilloLateralI2":
                IsPlaced = true;
                break;
            case "MPTornilloLateralI3":
                IsPlaced = true;
                break;
            case "MPTornilloLateralD1":
                IsPlaced = true;
                break;
            case "MPTornilloLateralD2":
                IsPlaced = true;
                break;
            case "MPTornilloLateralD3":
                IsPlaced = true;
                break;
            case "PuertaTPM1":
                IsPlaced = true;
                break;
            case "PuertaTPM2":
                IsPlaced = true;
                break;
            case "PuertaTPM3":
                IsPlaced = true;
                break;
            case "PuertaTRM1":
                IsPlaced = true;
                break;
            case "PuertaTRM2":
                IsPlaced = true;
                break;
            case "PuertaTRM3":
                IsPlaced = true;
                break;
            case "PuertaTMM1":
                IsPlaced = true;
                break;
            case "PuertaTMM2":
                IsPlaced = true;
                break;
            case "PuertaTMM3":
                IsPlaced = true;
                break;
            case "PuertaTPP1":
                IsPlaced = true;
                break;
            case "PuertaTPP2":
                IsPlaced = true;
                break;
            case "PuertaTPP3":
                IsPlaced = true;
                break;
            case "PuertaTRP1":
                IsPlaced = true;
                break;
            case "PuertaTRP2":
                IsPlaced = true;
                break;
            case "PuertaTRP3":
                IsPlaced = true;
                break;
            case "PuertaTMP1":
                IsPlaced = true;
                break;
            case "PuertaTMP2":
                IsPlaced = true;
                break;
            case "PuertaTMP3":
                IsPlaced = true;
                break;
#endregion
            #region VENTANA
            //VENTANA
            case "MVenTornillo1":
                IsPlaced = true;
                break;
            case "MVenTornillo2":
                IsPlaced = true;
                break;
            case "MVenTornillo3":
                IsPlaced = true;
                break;
            case "MVenTornillo4":
                IsPlaced = true;
                break;
            case "MVenTornillo5":
                IsPlaced = true;
                break;
            case "MVenTornillo6":
                IsPlaced = true;
                break;
            case "MVenTornillo7":
                IsPlaced = true;
                break;
            case "MVenTornillo8":
                IsPlaced = true;
                break;
            case "MVenTornillo9":
                IsPlaced = true;
                break;
            case "MVenTornillo10":
                IsPlaced = true;
                break;
            case "MVenTornillo11":
                IsPlaced = true;
                break;
            case "MVenTornillo12":
                IsPlaced = true;
                break;
            case "MVenTornillo13":
                IsPlaced = true;
                break;
            case "MVenTornillo14":
                IsPlaced = true;
                break;
            case "MVenTornillo15":
                IsPlaced = true;
                break;
            case "MVenTornillo16":
                IsPlaced = true;
                break;
            case "MVenTarugo1":
                IsPlaced = true;
                break;
            case "MVenTarugo2":
                IsPlaced = true;
                break;
            case "MVenTarugo3":
                IsPlaced = true;
                break;
            case "MVenTarugo4":
                IsPlaced = true;
                break;
            case "MVenTarugo5":
                IsPlaced = true;
                break;
            case "MVenTarugo6":
                IsPlaced = true;
                break;
            case "MVenTarugo7":
                IsPlaced = true;
                break;
            case "MVenTarugo8":
                IsPlaced = true;
                break;
            case "MVenTarugo9":
                IsPlaced = true;
                break;
            case "MVenTarugo10":
                IsPlaced = true;
                break;
            case "MVenTarugo11":
                IsPlaced = true;
                break;
            case "MVenTarugo12":
                IsPlaced = true;
                break;
            case "MVenTarugo13":
                IsPlaced = true;
                break;
            case "MVenTarugo14":
                IsPlaced = true;
                break;
            case "MVenTarugo15":
                IsPlaced = true;
                break;
            case "MVenTarugo16":
                IsPlaced = true;
                break;
            case "MVMarco1":
                IsPlaced = true;
                break;
            case "MVMarco2":
                IsPlaced = true;
                break;
            case "MVMarco3":
                IsPlaced = true;
                break;
            case "MVMarco4":
                IsPlaced = true;
                break;
            case "MVMarco5":
                IsPlaced = true;
                break;
            case "MVMarco6":
                IsPlaced = true;
                break;
            case "MVMarco7":
                IsPlaced = true;
                break;
            case "MVMarco8":
                IsPlaced = true;
                break;
            case "MVMarco9":
                IsPlaced = true;
                break;
            case "MVMarco10":
                IsPlaced = true;
                break;
            case "MVProtector1":
                IsPlaced = true;
                break;
            case "MVProtector2":
                IsPlaced = true;
                break;
            case "MVVidrio1":
                IsPlaced = true;
                break;
            case "MVVidrio2":
                IsPlaced = true;
                break;
            case "MVVidrio3":
                IsPlaced = true;
                break;
            #endregion
            #region ALERO
            //ALERO
            case "MAleTarugo1":
                IsPlaced = true;
                break;
            case "MAleTarugo2":
                IsPlaced = true;
                break;
            case "MAleTarugo3":
                IsPlaced = true;
                break;
            case "MAleTarugo4":
                IsPlaced = true;
                break;
            case "MAleTarugo5":
                IsPlaced = true;
                break;
            case "MAleTarugo6":
                IsPlaced = true;
                break;
            case "MAleTarugo7":
                IsPlaced = true;
                break;
            case "MAleTarugo8":
                IsPlaced = true;
                break;
            case "MAleTornillo1":
                IsPlaced = true;
                break;
            case "MAleTornillo2":
                IsPlaced = true;
                break;
            case "MAleTornillo3":
                IsPlaced = true;
                break;
            case "MAleTornillo4":
                IsPlaced = true;
                break;
            case "MAleTornillo5":
                IsPlaced = true;
                break;
            case "MAleTornillo6":
                IsPlaced = true;
                break;
            case "MAleTornillo7":
                IsPlaced = true;
                break;
            case "MAleTornillo8":
                IsPlaced = true;
                break;
            case "MAleTornillo9":
                IsPlaced = true;
                break;
            case "MAleTornillo10":
                IsPlaced = true;
                break;
            case "MAleTornillo11":
                IsPlaced = true;
                break;
            case "MAleTornillo12":
                IsPlaced = true;
                break;
            case "MAleTornillo13":
                IsPlaced = true;
                break;
            case "MAleTornillo14":
                IsPlaced = true;
                break;
            case "MAleTornillo15":
                IsPlaced = true;
                break;
            case "MAleTornillo16":
                IsPlaced = true;
                break;
            case "MAleSombrilla":
                IsPlaced = true;
                break;
            case "MAleParanete1":
                IsPlaced = true;
                break;
            case "MAleParanete2":
                IsPlaced = true;
                break;
            case "MAleBloque1":
                IsPlaced = true;
                break;
            case "MAleBloque2":
                IsPlaced = true;
                break;
            case "MAleBloque3":
                IsPlaced = true;
                break;
            case "MAleBloque4":
                IsPlaced = true;
                break;
            case "MAleBloqueTeho1":
                IsPlaced = true;
                break;
            case "MAleBloqueTeho2":
                IsPlaced = true;
                break;
            case "MAleBloqueTeho3":
                IsPlaced = true;
                break;
            #endregion
            #region LUCES
            //LUCES
            case "MILTornillo1":
                IsPlaced = true;
                break;
            case "MLTornillo2":
                IsPlaced = true;
                break;
            case "MLTornillo3":
                IsPlaced = true;
                break;
            case "MLTornillo4":
                IsPlaced = true;
                break;
            case "MLuzTornillo5":
                IsPlaced = true;
                break;
            case "MLuzTornillo6":
                IsPlaced = true;
                break;
            case "MLuzTornillo7":
                IsPlaced = true;
                break;
            case "MLuzTornillo8":
                IsPlaced = true;
                break;
            case "MLuzTornillo9":
                IsPlaced = true;
                break;
            case "MLuzTornillo10":
                IsPlaced = true;
                break;
            case "MLuzSocketBase":
                IsPlaced = true;
                break;
            case "MILSocket":
                IsPlaced = true;
                break;
            case "MLuzInterruptorBase":
                IsPlaced = true;
                break;
            case "MILInterruptor":
                IsPlaced = true;
                break;
            case "MILFoco":
                IsPlaced = true;
                break;
            case "MLuzCableAzul":
                IsPlaced = true;
                break;
            case "MLuzCableRojo":
                IsPlaced = true;
                break;
            case "MLuzCableNegro":
                IsPlaced = true;
                break;
            #endregion
            #region FREGADERO
            case "FCano":
                IsPlaced = true;
                break;
            case "FLavadero":
                IsPlaced = true;
                break;
            case "FTuberia1":
                IsPlaced = true;
                break;
            case "FTuberia2":
                IsPlaced = true;
                break;
            case "FTapa1":
                IsPlaced = true;
                break;
            case "FTapa2":
                IsPlaced = true;
                break;
            case "FTapa3":
                IsPlaced = true;
                break;
            case "FTapa4":
                IsPlaced = true;
                break;
            #endregion
            #region MUEBLE
            case "TagMuebleMadera1": IsPlaced = true;break;
            case "TagMuebleMadera2": IsPlaced = true; break;
            case "TagMuebleMadera3": IsPlaced = true; break;
            case "TagMuebleMadera4": IsPlaced = true; break;
            case "TagMuebleMadera5": IsPlaced = true; break;
            case "TagMuebleMadera6": IsPlaced = true; break;
            case "TagMuebleMadera7": IsPlaced = true; break;
            case "TagMuebleMadera8": IsPlaced = true; break;
            case "TagMuebleTarugo1": IsPlaced = true; break;
            case "TagMuebleTarugo2": IsPlaced = true; break;
            case "TagMuebleTarugo3": IsPlaced = true; break;
            case "TagMuebleTarugo4": IsPlaced = true; break;
            case "TagMuebleTarugo5": IsPlaced = true; break;
            case "TagMuebleTarugo6": IsPlaced = true; break;
            case "TagMuebleTarugo7": IsPlaced = true; break;
            case "TagMuebleTarugo8": IsPlaced = true; break;
            case "TagMuebleTarugo9": IsPlaced = true; break;
            case "TagMuebleTarugo10": IsPlaced = true; break;
            case "TagMuebleTarugo11": IsPlaced = true; break;
            case "TagMuebleTarugo12": IsPlaced = true; break;
            case "TagMuebleTarugo13": IsPlaced = true; break;
            case "TagMuebleTarugo14": IsPlaced = true; break;
            case "TagMuebleTarugo15": IsPlaced = true; break;
            case "TagMuebleTarugo16": IsPlaced = true; break;
            case "TagMuebleTarugo17": IsPlaced = true; break;
            case "TagMuebleTarugo18": IsPlaced = true; break;
            case "TagMuebleTarugo19": IsPlaced = true; break;
            case "TagMuebleTarugo20": IsPlaced = true; break;
            case "TagMuebleTarugo21": IsPlaced = true; break;
            case "TagMuebleTarugo22": IsPlaced = true; break;
            case "TagMuebleTarugo23": IsPlaced = true; break;
            case "TagMuebleTarugo24": IsPlaced = true; break;
            case "TagMuebleTarugo25": IsPlaced = true; break;
            case "TagMuebleTarugo26": IsPlaced = true; break;
            case "TagMuebleTarugo27": IsPlaced = true; break;
            case "TagMuebleTornillo1": IsPlaced = true; break;
            case "TagMuebleTornillo2": IsPlaced = true; break;
            case "TagMuebleTornillo3": IsPlaced = true; break;
            case "TagMuebleTornillo4": IsPlaced = true; break;
            case "TagMuebleTornillo5": IsPlaced = true; break;
            case "TagMuebleTornillo6": IsPlaced = true; break;
            case "TagMuebleTornillo7": IsPlaced = true; break;
            case "TagMuebleTornillo8": IsPlaced = true; break;
            case "TagMuebleTornillo9": IsPlaced = true; break;
            case "TagMuebleTornillo10": IsPlaced = true; break;
            case "TagMuebleTornillo11": IsPlaced = true; break;
            case "TagMuebleTornillo12": IsPlaced = true; break;
            case "TagMuebleTornillo13": IsPlaced = true; break;
            case "TagMuebleTornillo14": IsPlaced = true; break;
            case "TagMuebleTornillo15": IsPlaced = true; break;
            case "TagMuebleTornillo16": IsPlaced = true; break;
            case "TagMuebleTornillo17": IsPlaced = true; break;
            case "TagMuebleTornillo18": IsPlaced = true; break;
            case "TagMuebleTornillo19": IsPlaced = true; break;
            case "TagMuebleTornillo20": IsPlaced = true; break;
            case "TagMuebleTornillo21": IsPlaced = true; break;
            case "TagMuebleTornillo22": IsPlaced = true; break;
            case "TagMuebleTornillo23": IsPlaced = true; break;
            case "TagMuebleTornillo24": IsPlaced = true; break;
            case "TagMuebleTornillo25": IsPlaced = true; break;
            case "TagMuebleTornillo26": IsPlaced = true; break;
            case "TagMuebleTornillo27": IsPlaced = true; break;
            case "TagMuebleTornillo28": IsPlaced = true; break;
            case "TagMuebleTornillo29": IsPlaced = true; break;
            case "TagMuebleTornillo30": IsPlaced = true; break;
            case "TagMuebleBizagra1": IsPlaced = true; break;
            case "TagMuebleBizagra2": IsPlaced = true; break;
            case "TagMuebleBizagra3": IsPlaced = true; break;
            case "TagMuebleBizagra4": IsPlaced = true; break;
            #endregion
            #region MAYOLICA
            case "PMLoseta1": IsPlaced = true; break;
            case "PMLoseta2": IsPlaced = true; break;
            case "PMLoseta3": IsPlaced = true; break;
            case "PMLoseta4": IsPlaced = true; break;
            case "PMLoseta5": IsPlaced = true; break;
            case "PMLoseta6": IsPlaced = true; break;
            case "PMMayolica1": IsPlaced = true; break;
            case "PMMayolica2": IsPlaced = true; break;
            case "PMMayolica3": IsPlaced = true; break;
            case "PMMayolica4": IsPlaced = true; break;
            case "PMMayolica5": IsPlaced = true; break;
            case "PMMayolica6": IsPlaced = true; break;
            case "PMMayolica7": IsPlaced = true; break;
            case "PMMayolica8": IsPlaced = true; break;
            case "PMMayolica9": IsPlaced = true; break;
            case "PMMayolica10": IsPlaced = true; break;
            case "PMMayolica11": IsPlaced = true; break;
            case "PMMayolica12": IsPlaced = true; break;
            case "PMCruceta1": IsPlaced = true; break;
            case "PMCruceta2": IsPlaced = true; break;
            case "PMCruceta3": IsPlaced = true; break;
            case "PMCruceta4": IsPlaced = true; break;
            case "PMCruceta5": IsPlaced = true; break;
            case "PMCruceta6": IsPlaced = true; break;
            case "PMCruceta7": IsPlaced = true; break;
            case "PMCruceta8": IsPlaced = true; break;
                #endregion
        }
    }
    //RECURSOS
    public void TagEffect(string tag)
    {
        switch (tag)
        {
            #region PUERTA
            //PUERTA
            case "MPBIzquierdo":
                IsEffected = true;
                break;
            case "MPBDerecho":
                IsEffected = true;
                break;
            case "MPBSuperior":
                IsEffected = true;
                break;
            case "MPBPuerta":
                IsEffected = true;
                break;
            case "MPBizagra1":
                IsEffected = true;
                break;
            case "MPBizagra2":
                IsEffected = true;
                break;
            case "MPBizagra3":
                IsEffected = true;
                break;
            case "MPTarugoSuperior1":
                IsEffected = true;
                break;
            case "MPTarugoSuperior2":
                IsEffected = true;
                break;
            case "MPTarugoLateralI1":
                IsEffected = true;
                break;
            case "MPTarugoLateralI2":
                IsEffected = true;
                break;
            case "MPTarugoLateralI3":
                IsEffected = true;
                break;
            case "MPTarugoLateraD1":
                IsEffected = true;
                break;
            case "MPTarugoLateraD2":
                IsEffected = true;
                break;
            case "MPTarugoLateraD3":
                IsEffected = true;
                break;
            case "MPTornilloSuperior1":
                IsEffected = true;
                break;
            case "MPTornilloSuperior2":
                IsEffected = true;
                break;
            case "MPTornilloLateralI1":
                IsEffected = true;
                break;
            case "MPTornilloLateralI2":
                IsEffected = true;
                break;
            case "MPTornilloLateralI3":
                IsEffected = true;
                break;
            case "MPTornilloLateralD1":
                IsEffected = true;
                break;
            case "MPTornilloLateralD2":
                IsEffected = true;
                break;
            case "MPTornilloLateralD3":
                IsEffected = true;
                break;
            case "PuertaTPM1":
                IsEffected = true;
                break;
            case "PuertaTPM2":
                IsEffected = true;
                break;
            case "PuertaTPM3":
                IsEffected = true;
                break;
            case "PuertaTRM1":
                IsEffected = true;
                break;
            case "PuertaTRM2":
                IsEffected = true;
                break;
            case "PuertaTRM3":
                IsEffected = true;
                break;
            case "PuertaTMM1":
                IsEffected = true;
                break;
            case "PuertaTMM2":
                IsEffected = true;
                break;
            case "PuertaTMM3":
                IsEffected = true;
                break;
            case "PuertaTPP1":
                IsEffected = true;
                break;
            case "PuertaTPP2":
                IsEffected = true;
                break;
            case "PuertaTPP3":
                IsEffected = true;
                break;
            case "PuertaTRP1":
                IsEffected = true;
                break;
            case "PuertaTRP2":
                IsEffected = true;
                break;
            case "PuertaTRP3":
                IsEffected = true;
                break;
            case "PuertaTMP1":
                IsEffected = true;
                break;
            case "PuertaTMP2":
                IsEffected = true;
                break;
            case "PuertaTMP3":
                IsEffected = true;
                break;
            #endregion
            #region VENTANA
            //VENTANA
            case "MVenTornillo1":
                IsEffected = true;
                break;
            case "MVenTornillo2":
                IsEffected = true;
                break;
            case "MVenTornillo3":
                IsEffected = true;
                break;
            case "MVenTornillo4":
                IsEffected = true;
                break;
            case "MVenTornillo5":
                IsEffected = true;
                break;
            case "MVenTornillo6":
                IsEffected = true;
                break;
            case "MVenTornillo7":
                IsEffected = true;
                break;
            case "MVenTornillo8":
                IsEffected = true;
                break;
            case "MVenTornillo9":
                IsEffected = true;
                break;
            case "MVenTornillo10":
                IsEffected = true;
                break;
            case "MVenTornillo11":
                IsEffected = true;
                break;
            case "MVenTornillo12":
                IsEffected = true;
                break;
            case "MVenTornillo13":
                IsEffected = true;
                break;
            case "MVenTornillo14":
                IsEffected = true;
                break;
            case "MVenTornillo15":
                IsEffected = true;
                break;
            case "MVenTornillo16":
                IsEffected = true;
                break;
            case "MVenTarugo1":
                IsEffected = true;
                break;
            case "MVenTarugo2":
                IsEffected = true;
                break;
            case "MVenTarugo3":
                IsEffected = true;
                break;
            case "MVenTarugo4":
                IsEffected = true;
                break;
            case "MVenTarugo5":
                IsEffected = true;
                break;
            case "MVenTarugo6":
                IsEffected = true;
                break;
            case "MVenTarugo7":
                IsEffected = true;
                break;
            case "MVenTarugo8":
                IsEffected = true;
                break;
            case "MVenTarugo9":
                IsEffected = true;
                break;
            case "MVenTarugo10":
                IsEffected = true;
                break;
            case "MVenTarugo11":
                IsEffected = true;
                break;
            case "MVenTarugo12":
                IsEffected = true;
                break;
            case "MVenTarugo13":
                IsEffected = true;
                break;
            case "MVenTarugo14":
                IsEffected = true;
                break;
            case "MVenTarugo15":
                IsEffected = true;
                break;
            case "MVenTarugo16":
                IsEffected = true;
                break;
            case "MVVidrio1":
                IsEffected = true;
                break;
            case "MVVidrio2":
                IsEffected = true;
                break;
            case "MVVidrio3":
                IsEffected = true;
                break;
            case "MVMarco1":
                IsEffected = true;
                break;
            case "MVMarco2":
                IsEffected = true;
                break;
            case "MVMarco3":
                IsEffected = true;
                break;
            case "MVMarco4":
                IsEffected = true;
                break;
            case "MVMarco5":
                IsEffected = true;
                break;
            case "MVMarco6":
                IsEffected = true;
                break;
            case "MVMarco7":
                IsEffected = true;
                break;
            case "MVMarco8":
                IsEffected = true;
                break;
            case "MVMarco9":
                IsEffected = true;
                break;
            case "MVMarco10":
                IsEffected = true;
                break;
            case "MVProtector1":
                IsEffected = true;
                break;
            case "MVProtector2":
                IsEffected = true;
                break;
            #endregion
            #region ALERO
            //ALERO
            case "MAleTarugo1":
                IsEffected = true;
                break;
            case "MAleTarugo2":
                IsEffected = true;
                break;
            case "MAleTarugo3":
                IsEffected = true;
                break;
            case "MAleTarugo4":
                IsEffected = true;
                break;
            case "MAleTarugo5":
                IsEffected = true;
                break;
            case "MAleTarugo6":
                IsEffected = true;
                break;
            case "MAleTarugo7":
                IsEffected = true;
                break;
            case "MAleTarugo8":
                IsEffected = true;
                break;
            case "MAleTornillo1":
                IsEffected = true;
                break;
            case "MAleTornillo2":
                IsEffected = true;
                break;
            case "MAleTornillo3":
                IsEffected = true;
                break;
            case "MAleTornillo4":
                IsEffected = true;
                break;
            case "MAleTornillo5":
                IsEffected = true;
                break;
            case "MAleTornillo6":
                IsEffected = true;
                break;
            case "MAleTornillo7":
                IsEffected = true;
                break;
            case "MAleTornillo8":
                IsEffected = true;
                break;
            case "MAleTornillo9":
                IsEffected = true;
                break;
            case "MAleTornillo10":
                IsEffected = true;
                break;
            case "MAleTornillo11":
                IsEffected = true;
                break;
            case "MAleTornillo12":
                IsEffected = true;
                break;
            case "MAleTornillo13":
                IsEffected = true;
                break;
            case "MAleTornillo14":
                IsEffected = true;
                break;
            case "MAleTornillo15":
                IsEffected = true;
                break;
            case "MAleTornillo16":
                IsEffected = true;
                break;
            #endregion
            #region LUCES
            //LUCES
            case "MILTornillo1":
                IsEffected = true;
                break;
            case "MLTornillo2":
                IsEffected = true;
                break;
            case "MLTornillo3":
                IsEffected = true;
                break;
            case "MLTornillo4":
                IsEffected = true;
                break;
            case "MLuzTornillo5":
                IsEffected = true;
                break;
            case "MLuzTornillo6":
                IsEffected = true;
                break;
            case "MLuzTornillo7":
                IsEffected = true;
                break;
            case "MLuzTornillo8":
                IsEffected = true;
                break;
            case "MLuzTornillo9":
                IsEffected = true;
                break;
            case "MLuzTornillo10":
                IsEffected = true;
                break;
            #endregion
            #region FREGADERO
            #endregion
            #region MUEBLE
            case "TagMuebleTarugo1": IsEffected = true; break;
            case "TagMuebleTarugo2": IsEffected = true; break;
            case "TagMuebleTarugo3": IsEffected = true; break;
            case "TagMuebleTarugo4": IsEffected = true; break;
            case "TagMuebleTarugo5": IsEffected = true; break;
            case "TagMuebleTarugo6": IsEffected = true; break;
            case "TagMuebleTarugo7": IsEffected = true; break;
            case "TagMuebleTarugo8": IsEffected = true; break;
            case "TagMuebleTarugo9": IsEffected = true; break;
            case "TagMuebleTarugo10": IsEffected = true; break;
            case "TagMuebleTarugo11": IsEffected = true; break;
            case "TagMuebleTarugo12": IsEffected = true; break;
            case "TagMuebleTarugo13": IsEffected = true; break;
            case "TagMuebleTarugo14": IsEffected = true; break;
            case "TagMuebleTarugo15": IsEffected = true; break;
            case "TagMuebleTarugo16": IsEffected = true; break;
            case "TagMuebleTarugo17": IsEffected = true; break;
            case "TagMuebleTarugo18": IsEffected = true; break;
            case "TagMuebleTarugo19": IsEffected = true; break;
            case "TagMuebleTarugo20": IsEffected = true; break;
            case "TagMuebleTarugo21": IsEffected = true; break;
            case "TagMuebleTarugo22": IsEffected = true; break;
            case "TagMuebleTarugo23": IsEffected = true; break;
            case "TagMuebleTarugo24": IsEffected = true; break;
            case "TagMuebleTarugo25": IsEffected = true; break;
            case "TagMuebleTarugo26": IsEffected = true; break;
            case "TagMuebleTarugo27": IsEffected = true; break;
            case "TagMuebleTornillo1": IsEffected = true; break;
            case "TagMuebleTornillo2": IsEffected = true; break;
            case "TagMuebleTornillo3": IsEffected = true; break;
            case "TagMuebleTornillo4": IsEffected = true; break;
            case "TagMuebleTornillo5": IsEffected = true; break;
            case "TagMuebleTornillo6": IsEffected = true; break;
            case "TagMuebleTornillo7": IsEffected = true; break;
            case "TagMuebleTornillo8": IsEffected = true; break;
            case "TagMuebleTornillo9": IsEffected = true; break;
            case "TagMuebleTornillo10": IsEffected = true; break;
            case "TagMuebleTornillo11": IsEffected = true; break;
            case "TagMuebleTornillo12": IsEffected = true; break;
            case "TagMuebleTornillo13": IsEffected = true; break;
            case "TagMuebleTornillo14": IsEffected = true; break;
            case "TagMuebleTornillo15": IsEffected = true; break;
            case "TagMuebleTornillo16": IsEffected = true; break;
            case "TagMuebleTornillo17": IsEffected = true; break;
            case "TagMuebleTornillo18": IsEffected = true; break;
            case "TagMuebleTornillo19": IsEffected = true; break;
            case "TagMuebleTornillo20": IsEffected = true; break;
            case "TagMuebleTornillo21": IsEffected = true; break;
            case "TagMuebleTornillo22": IsEffected = true; break;
            case "TagMuebleTornillo23": IsEffected = true; break;
            case "TagMuebleTornillo24": IsEffected = true; break;
            case "TagMuebleTornillo25": IsEffected = true; break;
            case "TagMuebleTornillo26": IsEffected = true; break;
            case "TagMuebleTornillo27": IsEffected = true; break;
            case "TagMuebleTornillo28": IsEffected = true; break;
            case "TagMuebleTornillo29": IsEffected = true; break;
            case "TagMuebleTornillo30": IsEffected = true; break;
                #endregion
        }
    }

    public void NamePainted(string name)
    {
        switch (name)
        {
            #region FREGADERO
            case "silicona1":IsPainted = true;break;
            case "silicona2":IsPainted = true;break;
            case "silicona3":IsPainted = true;break;
            case "silicona4":IsPainted = true;break;
            case "silicona5":IsPainted = true;break;
            case "silicona6":IsPainted = true;break;
            case "silicona7":IsPainted = true;break;
            case "silicona8":IsPainted = true; break;
            case "silicona9":IsPainted = true; break;
            case "silicona10":IsPainted = true; break;
            case "silicona11":IsPainted = true;break;
            case "silicona12":IsPainted = true;break;
            case "silicona13":IsPainted = true;break;
            case "silicona14":IsPainted = true;break;
            case "silicona15":IsPainted = true;break;
            case "silicona16":IsPainted = true;break;
            case "silicona17":IsPainted = true;break;
            case "silicona18":IsPainted = true;break;
            case "silicona19":IsPainted = true;break;
            case "silicona20":IsPainted = true;break;
            case "silicona21":IsPainted = true;break;
            case "silicona22":IsPainted = true;break;
            case "silicona23":IsPainted = true;break;
            case "silicona24":IsPainted = true;break;
            case "silicona25":IsPainted = true;break;
            case "silicona26":IsPainted = true;break;
            case "silicona27":IsPainted = true;break;
            case "silicona28":IsPainted = true;break;
            case "silicona29":IsPainted = true; break;
            case "silicona30":IsPainted = true; break;
            case "silicona31":IsPainted = true; break;
            case "silicona32":IsPainted = true; break;
            case "silicona33":IsPainted = true; break;
            case "silicona34":IsPainted = true; break;
            case "silicona35":IsPainted = true; break;
            case "silicona36":IsPainted = true; break;
            case "silicona37":IsPainted = true; break;
            case "silicona38":IsPainted = true; break;
            case "silicona39":IsPainted = true; break;
            case "silicona40":IsPainted = true; break;
            case "silicona41":IsPainted = true; break;
            case "silicona42":IsPainted = true; break;
            case "silicona43":IsPainted = true; break;
            case "silicona44":IsPainted = true; break;
            case "silicona45":IsPainted = true; break;
            case "silicona46":IsPainted = true; break;
            case "silicona47":IsPainted = true; break;
            case "silicona48":IsPainted = true; break;
            case "silicona49":IsPainted = true; break;
            case "silicona50":IsPainted = true; break;
            case "silicona51":IsPainted = true; break;
            case "silicona52":IsPainted = true; break;
            case "silicona53":IsPainted = true; break;
            case "silicona54":IsPainted = true; break;
            case "silicona55":IsPainted = true; break;
            case "silicona56":IsPainted = true; break;
            case "silicona57":IsPainted = true; break;
            case "silicona58":IsPainted = true; break;
            case "silicona59":IsPainted = true; break;
            case "silicona60":IsPainted = true; break;
            case "silicona61":IsPainted = true; break;
            case "silicona62":IsPainted = true; break;
            case "silicona63":IsPainted = true; break;
            case "silicona64":IsPainted = true; break;
            case "silicona65":IsPainted = true; break;
            case "silicona66":IsPainted = true; break;
            case "silicona67":IsPainted = true; break;
            case "silicona68":IsPainted = true; break;
            case "silicona69":IsPainted = true; break;
            case "silicona70":IsPainted = true; break;
            case "silicona71":IsPainted = true; break;
            case "silicona72":IsPainted = true; break;
            case "silicona73":IsPainted = true; break;
            case "silicona74":IsPainted = true; break;
            case "silicona75":IsPainted = true; break;
            case "silicona76":IsPainted = true; break;
            case "silicona77":IsPainted = true; break;
            case "silicona78":IsPainted = true; break;
            case "silicona79":IsPainted = true; break;
            case "silicona80":IsPainted = true; break;
            case "silicona81":IsPainted = true; break;
            case "silicona82":IsPainted = true; break;
            case "silicona83":IsPainted = true; break;
            case "silicona84":IsPainted = true; break;
            case "silicona85":IsPainted = true; break;
            case "silicona86":IsPainted = true; break;
            case "silicona87":IsPainted = true; break;
            case "silicona88":IsPainted = true; break;
            case "silicona89":IsPainted = true; break;
            case "silicona90":IsPainted = true; break;
            case "silicona91":IsPainted = true; break;
            case "silicona92":IsPainted = true; break;
            case "silicona93":IsPainted = true; break;
            case "silicona94":IsPainted = true; break;
            case "silicona95":IsPainted = true; break;
            case "silicona96":IsPainted = true; break;
            case "silicona97":IsPainted = true; break;
            case "silicona98":IsPainted = true; break;
            case "silicona99":IsPainted = true; break;
            #endregion
            #region MAYOLICA
            case "Cube": IsPainted = true; break;
            case "Cube1": IsPainted = true; break;
            case "Cube2": IsPainted = true; break;
            case "Cube3": IsPainted = true; break;
            case "Cube4": IsPainted = true; break;
            case "Cube5": IsPainted = true; break;
            case "Cube6": IsPainted = true; break;
            case "Cube7": IsPainted = true; break;
            case "Cube8": IsPainted = true; break;
            case "Cube9": IsPainted = true; break;
            case "Cube10": IsPainted = true; break;
            case "Cube11": IsPainted = true; break;
            case "Cube12": IsPainted = true; break;
            case "Cube13": IsPainted = true; break;
            case "Cube14": IsPainted = true; break;
            case "Cube15": IsPainted = true; break;
            case "Cube16": IsPainted = true; break;
            case "Cube17": IsPainted = true; break;
            case "Cube18": IsPainted = true; break;
            case "Cube19": IsPainted = true; break;
            case "Cube20": IsPainted = true; break;
            case "Cube21": IsPainted = true; break;
            case "Cube22": IsPainted = true; break;
            case "Cube23": IsPainted = true; break;
            case "Cube24": IsPainted = true; break;
            case "Cube25": IsPainted = true; break;
            case "Cube26": IsPainted = true; break;
            case "Cube27": IsPainted = true; break;
            case "Cube28": IsPainted = true; break;
            case "Cube29": IsPainted = true; break;
            case "Cube30": IsPainted = true; break;
            case "Cube31": IsPainted = true; break;
            case "Cube32": IsPainted = true; break;
            case "Cube33": IsPainted = true; break;
            case "Cube34": IsPainted = true; break;
            case "Cube35": IsPainted = true; break;
            case "Cube36": IsPainted = true; break;
            case "Cube37": IsPainted = true; break;
            case "Cube38": IsPainted = true; break;
            case "Cube39": IsPainted = true; break;
            case "Cube40": IsPainted = true; break;
            case "Cube41": IsPainted = true; break;
            case "Cube42": IsPainted = true; break;
            case "Cube43": IsPainted = true; break;
            case "Cube44": IsPainted = true; break;
            case "Cube45": IsPainted = true; break;
            case "Cube46": IsPainted = true; break;
            case "Cube47": IsPainted = true; break;
            case "Cube48": IsPainted = true; break;
            case "Cube49": IsPainted = true; break;
            case "Cube50": IsPainted = true; break;
            case "Cube51": IsPainted = true; break;
            case "Cube52": IsPainted = true; break;
            case "Cube53": IsPainted = true; break;
            case "Cube54": IsPainted = true; break;
            case "Cube55": IsPainted = true; break;
            case "Cube56": IsPainted = true; break;
            case "Cube57": IsPainted = true; break;
            case "Cube58": IsPainted = true; break;
            case "Cube59": IsPainted = true; break;
            case "Cube60": IsPainted = true; break;
            case "Cube61": IsPainted = true; break;
            case "Cube62": IsPainted = true; break;
            case "Cube63": IsPainted = true; break;
            case "Cube64": IsPainted = true; break;
            case "Cube65": IsPainted = true; break;
            case "Cube66": IsPainted = true; break;
            case "Cube67": IsPainted = true; break;
            case "Cube68": IsPainted = true; break;
            case "Cube69": IsPainted = true; break;
            case "Cube70": IsPainted = true; break;
            case "Cube71": IsPainted = true; break;
            case "Cube72": IsPainted = true; break;
            case "Cube73": IsPainted = true; break;
            case "Cube74": IsPainted = true; break;
            case "Cube75": IsPainted = true; break;
            case "Cube76": IsPainted = true; break;
            case "Cube77": IsPainted = true; break;
            case "Cube78": IsPainted = true; break;
            case "Cube79": IsPainted = true; break;
            case "Cube80": IsPainted = true; break;
            case "Cube81": IsPainted = true; break;
            case "Cube82": IsPainted = true; break;
            case "Cube83": IsPainted = true; break;
            case "Cube84": IsPainted = true; break;
            case "Cube85": IsPainted = true; break;
            case "Cube86": IsPainted = true; break;
            case "Cube87": IsPainted = true; break;
            case "Cube88": IsPainted = true; break;
            case "Cube89": IsPainted = true; break;
            case "Cube90": IsPainted = true; break;
            case "Cube91": IsPainted = true; break;
            case "Cube92": IsPainted = true; break;
            case "Cube93": IsPainted = true; break;
            case "Cube94": IsPainted = true; break;
            case "Cube95": IsPainted = true; break;
            case "Cube96": IsPainted = true; break;
            case "Cube97": IsPainted = true; break;
            case "Cube98": IsPainted = true; break;
            case "Cube99": IsPainted = true; break;
            case "Cube100": IsPainted = true; break;
            case "Cube101": IsPainted = true; break;
            case "Cube102": IsPainted = true; break;
            case "Cube103": IsPainted = true; break;
            case "Cube104": IsPainted = true; break;
            case "Cube105": IsPainted = true; break;
            case "Cube106": IsPainted = true; break;
            case "Cube107": IsPainted = true; break;
            case "Cube108": IsPainted = true; break;
            case "Cube109": IsPainted = true; break;
            case "Cube110": IsPainted = true; break;
            case "Cube111": IsPainted = true; break;
            case "Cube112": IsPainted = true; break;
            case "Cube113": IsPainted = true; break;
            case "Cube114": IsPainted = true; break;
            case "Cube115": IsPainted = true; break;
            case "Cube116": IsPainted = true; break;
            case "Cube117": IsPainted = true; break;
            case "Cube118": IsPainted = true; break;
            case "Cube119": IsPainted = true; break;
            case "Cube120": IsPainted = true; break;
            case "Cube121": IsPainted = true; break;
            case "Cube122": IsPainted = true; break;
            case "Cube123": IsPainted = true; break;
            case "Cube124": IsPainted = true; break;
            case "Cube125": IsPainted = true; break;
            case "Cube126": IsPainted = true; break;
            case "Cube127": IsPainted = true; break;
            case "Cube128": IsPainted = true; break;
            case "Cube129": IsPainted = true; break;
            case "Cube130": IsPainted = true; break;
            case "Cube131": IsPainted = true; break;
            case "Cube132": IsPainted = true; break;
            case "Cube133": IsPainted = true; break;
            case "Cube134": IsPainted = true; break;
            case "Cube135": IsPainted = true; break;
            case "Cube136": IsPainted = true; break;
            case "Cube137": IsPainted = true; break;
            case "Cube138": IsPainted = true; break;
            case "Cube139": IsPainted = true; break;
            case "Cube140": IsPainted = true; break;
            case "Cube141": IsPainted = true; break;
            case "Cube142": IsPainted = true; break;
            case "Cube143": IsPainted = true; break;
                #endregion
        }               
    }                   
}                       
                        
                        
                        