using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimationHelper : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var objeto = other.tag;

        if (objeto == "Taladro")
        {
            var test = 0;
        }
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0) {
            Animator anim = this.GetComponentInParent<Animator>();
            switch (this.tag)
            {
                //PUERTA
                case "MPTarugoSuperior1":
                    anim.SetTrigger("isEfectuar");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoSuperior2":
                    anim.SetTrigger("TS2Tigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateralI1":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLI1Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateralI2":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLI2Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateralI3":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLI3Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateraD1":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLD1Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateraD2":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLD2Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTarugoLateraD3":
                    if (objeto == "Taladro")
                    {
                        var test = 0;
                    }
                    anim.SetTrigger("TLD3Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloSuperior1":
                    anim.SetTrigger("ToS1Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloSuperior2":
                    anim.SetTrigger("ToS2Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralI1":
                    anim.SetTrigger("ToLI1Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralI2":
                    anim.SetTrigger("ToLI2Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralI3":
                    anim.SetTrigger("ToLI3Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralD1":
                    anim.SetTrigger("ToLD1Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralD2":
                    anim.SetTrigger("ToLD2Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                case "MPTornilloLateralD3":
                    anim.SetTrigger("ToLD3Trigger");
                    OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.RTouch);
                    break;
                //LUCES
                case "MILTornillo1":
                    anim.SetTrigger("TLu1Trigger");
                    break;
                case "MLTornillo2":
                    anim.SetTrigger("TLu2Trigger");
                    break;
                case "MLTornillo3":
                    anim.SetTrigger("TLu3Trigger");
                    break;
                case "MLTornillo4":
                    anim.SetTrigger("TLu4Trigger");
                    break;
                //VENTANA
                case "MVenTornillo1":
                    anim.SetTrigger("Ven1Trigger");
                    break;
                case "MVenTornillo2":
                    anim.SetTrigger("Ven2Trigger");
                    break;
                case "MVenTornillo3":
                    anim.SetTrigger("Ven3Trigger");
                    break;
                case "MVenTornillo4":
                    anim.SetTrigger("Ven4Trigger");
                    break;
                case "MVenTornillo5":
                    anim.SetTrigger("Ven5Trigger");
                    break;
                case "MVenTornillo6":
                    anim.SetTrigger("Ven6Trigger");
                    break;
                case "MVenTornillo7":
                    anim.SetTrigger("Ven7Trigger");
                    break;
                case "MVenTornillo8":
                    anim.SetTrigger("Ven8Trigger");
                    break;
                case "MVenTornillo9":
                    anim.SetTrigger("Ven9Trigger");
                    break;
                case "MVenTornillo10":
                    anim.SetTrigger("Ven10Trigger");
                    break;
                case "MVenTornillo11":
                    anim.SetTrigger("Ven11Trigger");
                    break;
                case "MVenTornillo12":
                    anim.SetTrigger("Ven12Trigger");
                    break;
                case "MVenTornillo13":
                    anim.SetTrigger("Ven13Trigger");
                    break;
                case "MVenTornillo14":
                    anim.SetTrigger("Ven14Trigger");
                    break;
                case "MVenTornillo15":
                    anim.SetTrigger("Ven15Trigger");
                    break;
                case "MVenTornillo16":
                    anim.SetTrigger("Ven16Trigger");
                    break;
                case "MVenTarugo1":
                    anim.SetTrigger("VenTa1Trigger");
                    break;
                case "MVenTarugo2":
                    anim.SetTrigger("VenTa2Trigger");
                    break;
                case "MVenTarugo3":
                    anim.SetTrigger("VenTa3Trigger");
                    break;
                case "MVenTarugo4":
                    anim.SetTrigger("VenTa4Trigger");
                    break;
                case "MVenTarugo5":
                    anim.SetTrigger("VenTa5Trigger");
                    break;
                case "MVenTarugo6":
                    anim.SetTrigger("VenTa6Trigger");
                    break;
                case "MVenTarugo7":
                    anim.SetTrigger("VenTa7Trigger");
                    break;
                case "MVenTarugo8":
                    anim.SetTrigger("VenTa8Trigger");
                    break;
                case "MVenTarugo9":
                    anim.SetTrigger("VentanaTarugo9Trigger");
                    break;
                case "MVenTarugo10":
                    anim.SetTrigger("VentanaTarugo10Trigger");
                    break;
                case "MVenTarugo11":
                    anim.SetTrigger("VentanaTarugo11Trigger");
                    break;
                case "MVenTarugo12":
                    anim.SetTrigger("VentanaTarugo12Trigger");
                    break;
                case "MVenTarugo13":
                    anim.SetTrigger("VentanaTarugo13Trigger");
                    break;
                case "MVenTarugo14":
                    anim.SetTrigger("VentanaTarugo14Trigger");
                    break;
                case "MVenTarugo15":
                    anim.SetTrigger("VentanaTarugo15Trigger");
                    break;
                case "MVenTarugo16":
                    anim.SetTrigger("VentanaTarugo16Trigger");
                    break;
                #region ALERO
                //ALERO
                case "MAleTarugo1":
                    anim.SetTrigger("AleroTarugo1Trigger");
                    break;
                case "MAleTarugo2":
                    anim.SetTrigger("AleroTarugo2Trigger");
                    break;
                case "MAleTarugo3":
                    anim.SetTrigger("AleroTarugo3Trigger");
                    break;
                case "MAleTarugo4":
                    anim.SetTrigger("AleroTarugo4Trigger");
                    break;
                case "MAleTarugo5":
                    anim.SetTrigger("AleroTarugo5Trigger");
                    break;
                case "MAleTarugo6":
                    anim.SetTrigger("AleroTarugo6Trigger");
                    break;
                case "MAleTarugo7":
                    anim.SetTrigger("AleroTarugo7Trigger");
                    break;
                case "MAleTarugo8":
                    anim.SetTrigger("AleroTarugo8Trigger");
                    break;
                case "MAleTornillo1":
                    anim.SetTrigger("AleT1Trigger");
                    break;
                case "MAleTornillo2":
                    anim.SetTrigger("AleT2Trigger");
                    break;
                case "MAleTornillo3":
                    anim.SetTrigger("AleT3Trigger");
                    break;
                case "MAleTornillo4":
                    anim.SetTrigger("AleT4Trigger");
                    break;
                case "MAleTornillo5":
                    anim.SetTrigger("AleT5Trigger");
                    break;
                case "MAleTornillo6":
                    anim.SetTrigger("AleT6Trigger");
                    break;
                case "MAleTornillo7":
                    anim.SetTrigger("AleT7Trigger");
                    break;
                case "MAleTornillo8":
                    anim.SetTrigger("AleT8Trigger");
                    break;
                case "MAleTornillo9":
                    anim.SetTrigger("AleT9Trigger");
                    break;
                case "MAleTornillo10":
                    anim.SetTrigger("AleT10Trigger");
                    break;
                case "MAleTornillo11":
                    anim.SetTrigger("AleT11Trigger");
                    break;
                case "MAleTornillo12":
                    anim.SetTrigger("AleT12Trigger");
                    break;
                case "MAleTornillo13":
                    anim.SetTrigger("AleT13Trigger");
                    break;
                case "MAleTornillo14":
                    anim.SetTrigger("AleT14Trigger");
                    break;
                case "MAleTornillo15":
                    anim.SetTrigger("AleT15Trigger");
                    break;
                case "MAleTornillo16":
                    anim.SetTrigger("AleT16Trigger");
                    break;
                    #endregion
            }
        }
    }
}
