using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationHelper : MonoBehaviour
{
    void OnCollisionStay(Collision other)
    {
        var tagObjeto = other.gameObject.tag;
        Debug.Log(other.gameObject.tag);
        // if (OVRInput.Get(OVRInput.Button.Two))
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0)
        {
            Animator anim = this.GetComponentInParent<Animator>();
            if (tagObjeto == "Martillo" || tagObjeto == "Taladro" || tagObjeto == "RightHand")
            {
                this.GetComponent<TutorialItem>().IsEffected = true;
                switch (this.tag)
                {
                    #region PUERTA
                    //PUERTA
                    case "MPTarugoSuperior1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("isEfectuar");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoSuperior2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TS2Tigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateralI1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLI1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateralI2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLI2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateralI3":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLI3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateraD1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLD1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateraD2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLD2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTarugoLateraD3":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("TLD3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloSuperior1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToS1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloSuperior2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToS2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralI1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLI1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralI2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLI2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralI3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLI3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralD1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLD1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralD2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLD2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MPTornilloLateralD3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("ToLD3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    /****/
                    case "PuertaTPM1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPM1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTPM2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPM2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTPM3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPM3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRM1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRM1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRM2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRM2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRM3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRM3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMM1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMM1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMM2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMM2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMM3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMM3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTPP1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPP1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTPP2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPP2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTPP3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TPP3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRP1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRP1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRP2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRP2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTRP3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TRP3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMP1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMP1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMP2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMP2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "PuertaTMP3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TMP3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    #endregion
                    /****/
                    #region LUCES
                    //LUCES
                    case "MILTornillo1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLTornillo2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLTornillo3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLTornillo4":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces4Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo5":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces5Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo6":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces6Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo7":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces7Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo8":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces8Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo9":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces9Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzTornillo10":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("TarugoLuces10Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MLuzPalanca":
                        if (tagObjeto == "RightHand")
                        {
                            anim.SetTrigger("LuzPalancaTrigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    #endregion
                    #region VENTANA
                    //VENTANA
                    case "MVenTornillo1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo4":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven4Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo5":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven5Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo6":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven6Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo7":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven7Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo8":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven8Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo9":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven9Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo10":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven10Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo11":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven11Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo12":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven12Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo13":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven13Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo14":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven14Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo15":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven15Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTornillo16":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("Ven16Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa1Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa2Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo3":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa3Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo4":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa4Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo5":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa5Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo6":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa6Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo7":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa7Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo8":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VenTa8Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo9":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo9Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo10":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo10Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo11":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo11Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo12":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo12Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo13":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo13Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo14":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo14Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo15":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo15Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MVenTarugo16":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("VentanaTarugo16Trigger");
                            OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    #endregion
                    #region ALERO
                    //ALERO
                    case "MAleTarugo1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo1Trigger");
                        }
                        break;
                    case "MAleTarugo2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo2Trigger");
                        }
                        break;
                    case "MAleTarugo3":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo3Trigger");
                        }
                        break;
                    case "MAleTarugo4":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo4Trigger");
                        }
                        break;
                    case "MAleTarugo5":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo5Trigger");
                        }
                        break;
                    case "MAleTarugo6":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo6Trigger");
                        }
                        break;
                    case "MAleTarugo7":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo7Trigger");
                        }
                        break;
                    case "MAleTarugo8":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("AleroTarugo8Trigger");
                        }
                        break;
                    case "MAleTornillo1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT1Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT2Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT3Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo4":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT4Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo5":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT5Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo6":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT6Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo7":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT7Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo8":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT8Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo9":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT9Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo10":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT10Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo11":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT11Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo12":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT12Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo13":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT13Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo14":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT14Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo15":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT15Trigger");
                            // OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    case "MAleTornillo16":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("AleT16Trigger");
                            //OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                        }
                        break;
                    #endregion
                    #region Mueble
                    case "TagMuebleTarugo1":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo1Trigger");
                        }
                        break;
                
                    case "TagMuebleTarugo2":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo2Trigger");
                        }
                        break;
                    case "TagMuebleTarugo3":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo3Trigger");
                        }
                        break;
                    case "TagMuebleTarugo4":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo4Trigger");
                        }
                        break;
                    case "TagMuebleTarugo5":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo5Trigger");
                        }
                        break;
                    case "TagMuebleTarugo6":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo6Trigger");
                        }
                        break;
                    case "TagMuebleTarugo7":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo7Trigger");
                        }
                        break;
                    case "TagMuebleTarugo8":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo8Trigger");
                        }
                        break;
                    case "TagMuebleTarugo9":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo9Trigger");
                        }
                        break;
                    case "TagMuebleTarugo10":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo10Trigger");
                        }
                        break;
                    case "TagMuebleTarugo11":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo11Trigger");
                        }
                        break;
                    case "TagMuebleTarugo12":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo12Trigger");
                        }
                        break;
                    case "TagMuebleTarugo13":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo13Trigger");
                        }
                        break;
                    case "TagMuebleTarugo14":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo14Trigger");
                        }
                        break;
                    case "TagMuebleTarugo15":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo15Trigger");
                        }
                        break;
                    case "TagMuebleTarugo16":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo16Trigger");
                        }
                        break;
                    case "TagMuebleTarugo17":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo17Trigger");
                        }
                        break;
                    case "TagMuebleTarugo18":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo18Trigger");
                        }
                        break;
                    case "TagMuebleTarugo19":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo19Trigger");
                        }
                        break;
                    case "TagMuebleTarugo20":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo20Trigger");
                        }
                        break;
                    case "TagMuebleTarugo21":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo21Trigger");
                        }
                        break;
                    case "TagMuebleTarugo22":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo22Trigger");
                        }
                        break;
                    case "TagMuebleTarugo23":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo23Trigger");
                        }
                        break;
                    case "TagMuebleTarugo24":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo24Trigger");
                        }
                        break;
                    case "TagMuebleTarugo25":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo25Trigger");
                        }
                        break;
                    case "TagMuebleTarugo26":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo26Trigger");
                        }
                        break;
                    case "TagMuebleTarugo27":
                        if (tagObjeto == "Martillo")
                        {
                            anim.SetTrigger("MuebleTarugo27Trigger");
                        }
                        break;
                    case "TagMuebleTornillo1":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo1Trigger");
                        }
                        break;
                    case "TagMuebleTornillo2":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo2Trigger");
                        }
                        break;
                    case "TagMuebleTornillo3":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo3Trigger");
                        }
                        break;
                    case "TagMuebleTornillo4":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo4Trigger");
                        }
                        break;
                    case "TagMuebleTornillo5":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo5Trigger");
                        }
                        break;
                    case "TagMuebleTornillo6":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo6Trigger");
                        }
                        break;
                    case "TagMuebleTornillo7":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo7Trigger");
                        }
                        break;
                    case "TagMuebleTornillo8":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo8Trigger");
                        }
                        break;
                    case "TagMuebleTornillo9":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo9Trigger");
                        }
                        break;
                    case "TagMuebleTornillo10":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo10Trigger");
                        }
                        break;
                    case "TagMuebleTornillo11":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo11Trigger");
                        }
                        break;  
                    case "TagMuebleTornillo12":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo12Trigger");
                        }
                        break;  
                    case "TagMuebleTornillo15":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo15Trigger");
                        }
                        break;  
                    case "TagMuebleTornillo16":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo16Trigger");
                        }
                        break; 
                    case "TagMuebleTornillo17":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo17Trigger");
                        }
                        break;     
                    case "TagMuebleTornillo18":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo18Trigger");
                        }
                        break; 
                    case "TagMuebleTornillo19":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo19Trigger");
                        }
                        break; 
                    case "TagMuebleTornillo20":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo20Trigger");
                        }
                        break;     
                    case "TagMuebleTornillo21":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo21Trigger");
                        }
                        break;     
                    case "TagMuebleTornillo22":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo22Trigger");
                        }
                        break; 
                    case "TagMuebleTornillo23":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo23Trigger");
                        }
                        break;      
                    case "TagMuebleTornillo24":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo24Trigger");
                        }
                        break;
                    case "TagMuebleTornillo25":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo25Trigger");
                        }
                        break;    
                    case "TagMuebleTornillo26":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo26Trigger");
                        }
                        break;      
                    case "TagMuebleTornillo27":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo27Trigger");
                        }
                        break;  
                    case "TagMuebleTornillo28":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo28Trigger");
                        }
                        break;            
                    case "TagMuebleTornillo29":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo29Trigger");
                        }
                        break; 
                    case "TagMuebleTornillo30":
                        if (tagObjeto == "Taladro")
                        {
                            anim.SetTrigger("MuebleTornillo30Trigger");
                        }
                        break; 
                    
                    #endregion
                }
            }
        }
    }
}
