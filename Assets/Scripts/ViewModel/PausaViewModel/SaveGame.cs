using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoxelBusters.Serialization;

namespace Assets.Scripts.ViewModel.PausaViewModel
{
    public class SaveGame : MonoBehaviour
    {
        public void Save(List<GameObject> LstObjetos,  String NameFile)
        {
            List<SaveModel> listObjectInPosition = new List<SaveModel>();
            foreach (var item in LstObjetos)
            {
                SnapObject target = item.GetComponent<SnapObject>();

                Vector3 position = GameObject.FindGameObjectWithTag(item.tag).transform.position;
                Quaternion rotation = GameObject.FindGameObjectWithTag(item.tag).transform.rotation;
                listObjectInPosition.Add(new SaveModel { 
                    Colocado = target != null ? target.isSnapped : true,
                    Etiqueta = item.tag,
                    Posicion = position,
                    Rotacion = rotation
                });
            }
            SerializationManager.Serialize<List<SaveModel>>(NameFile, listObjectInPosition);
        }
        public void Load(List<GameObject> LstObjetos, List<GameObject> LstObjectsPosition, String NameFile)
        {
            var LstObjetosLoad = SerializationManager.Deserialize<List<SaveModel>>(NameFile);

            for (int i = 0; i < LstObjetosLoad.Count(); i++)
            {
                SnapObject item1 =  LstObjetos[i].GetComponent<SnapObject>();
                SnapToLocation item2 = LstObjectsPosition[i].GetComponent<SnapToLocation>();
                if(LstObjetosLoad[i].Colocado == true)
                {
                   
                    GameObject.FindGameObjectWithTag(LstObjetosLoad[i].Etiqueta).transform.position = LstObjectsPosition[i].gameObject.transform.position;
                    GameObject.FindGameObjectWithTag(LstObjetosLoad[i].Etiqueta).transform.rotation = LstObjectsPosition[i].gameObject.transform.rotation;
                    GameObject.FindGameObjectWithTag(LstObjetosLoad[i].Etiqueta).transform.localScale = LstObjectsPosition[i].gameObject.transform.localScale;
                    item1.isSnapped = true;
                    item2.Snapped = true;
                    //GameObject.FindGameObjectWithTag(LstObjetosLoad[i].Etiqueta).transform.localScale = new Vector3(1, 1, 1);
                    var test = LstObjectsPosition[i].GetComponentInParent<Animator>();
                    Animator anim = LstObjectsPosition[i].GetComponentInParent<Animator>();
                    MoveAnimation(anim, LstObjetosLoad[i].Etiqueta);
                }
            }
        }
        public void MoveAnimation(Animator animator, string tag)
        {
            switch (tag)
            {
                #region PUERTA
                //PUERTA
                case "MPTarugoSuperior1":
                    animator.SetTrigger("isEfectuar");
                    break;
                case "MPTarugoSuperior2":
                    animator.SetTrigger("TS2Tigger");
                    break;
                case "MPTarugoLateralI1":
                    animator.SetTrigger("TLI1Trigger");
                    break;
                case "MPTarugoLateralI2":
                    animator.SetTrigger("TLI2Trigger");
                    break;
                case "MPTarugoLateralI3":
                    animator.SetTrigger("TLI3Trigger");
                    break;
                case "MPTarugoLateraD1":
                    animator.SetTrigger("TLD1Trigger");
                    break;
                case "MPTarugoLateraD2":
                    animator.SetTrigger("TLD2Trigger");
                    break;
                case "MPTarugoLateraD3":
                    animator.SetTrigger("TLD3Trigger");
                    break;
                case "MPTornilloSuperior1":
                    animator.SetTrigger("ToS1Trigger");
                    break;
                case "MPTornilloSuperior2":
                    animator.SetTrigger("ToS2Trigger");
                    break;
                case "MPTornilloLateralI1":
                    animator.SetTrigger("ToLI1Trigger");
                    break;
                case "MPTornilloLateralI2":
                    animator.SetTrigger("ToLI2Trigger");
                    break;
                case "MPTornilloLateralI3":
                    animator.SetTrigger("ToLI3Trigger");
                    break;
                case "MPTornilloLateralD1":
                    animator.SetTrigger("ToLD1Trigger");
                    break;
                case "MPTornilloLateralD2":
                    animator.SetTrigger("ToLD2Trigger");
                    break;
                case "MPTornilloLateralD3":
                    animator.SetTrigger("ToLD3Trigger");
                    break;
                #endregion
                #region LUCES
                //LUCES
                case "MILTornillo1":
                    animator.SetTrigger("TarugoLuces1Trigger");
                    break;
                case "MLTornillo2":
                    animator.SetTrigger("TarugoLuces2Trigger");
                    break;
                case "MLTornillo3":
                    animator.SetTrigger("TarugoLuces3Trigger");
                    break;
                case "MLTornillo4":
                    animator.SetTrigger("TarugoLuces4Trigger");
                    break;
                case "MLuzTornillo5":
                    animator.SetTrigger("TarugoLuces5Trigger");
                    break;
                case "MLuzTornillo6":
                    animator.SetTrigger("TarugoLuces6Trigger");
                    break;
                case "MLuzTornillo7":
                    animator.SetTrigger("TarugoLuces7Trigger");
                    break;
                case "MLuzTornillo8":
                    animator.SetTrigger("TarugoLuces8Trigger");
                    break;
                case "MLuzTornillo9":
                    animator.SetTrigger("TarugoLuces9Trigger");
                    break;
                case "MLuzTornillo10":
                    animator.SetTrigger("TarugoLuces10Trigger");
                    break;
                case "MLuzPalanca":
                    Animator anim = GameObject.Find("Game/Design/InterruptorElectronagmetico/Palanca").GetComponent<Animator>();
                    anim.SetTrigger("LuzPalancaTrigger");
                    break;
                #endregion
                #region VENTANA
                //VENTANA
                case "MVenTornillo1":
                    animator.SetTrigger("Ven1Trigger");
                    break;
                case "MVenTornillo2":
                    animator.SetTrigger("Ven2Trigger");
                    break;
                case "MVenTornillo3":
                    animator.SetTrigger("Ven3Trigger");
                    break;
                case "MVenTornillo4":
                    animator.SetTrigger("Ven4Trigger");
                    break;
                case "MVenTornillo5":
                    animator.SetTrigger("Ven5Trigger");
                    break;
                case "MVenTornillo6":
                    animator.SetTrigger("Ven6Trigger");
                    break;
                case "MVenTornillo7":
                    animator.SetTrigger("Ven7Trigger");
                    break;
                case "MVenTornillo8":
                    animator.SetTrigger("Ven8Trigger");
                    break;
                case "MVenTornillo9":
                    animator.SetTrigger("Ven9Trigger");
                    break;
                case "MVenTornillo10":
                    animator.SetTrigger("Ven10Trigger");
                    break;
                case "MVenTornillo11":
                    animator.SetTrigger("Ven11Trigger");
                    break;
                case "MVenTornillo12":
                    animator.SetTrigger("Ven12Trigger");
                    break;
                case "MVenTornillo13":
                    animator.SetTrigger("Ven13Trigger");
                    break;
                case "MVenTornillo14":
                    animator.SetTrigger("Ven14Trigger");
                    break;
                case "MVenTornillo15":
                    animator.SetTrigger("Ven15Trigger");
                    break;
                case "MVenTornillo16":
                    animator.SetTrigger("Ven16Trigger");
                    break;
                case "MVenTarugo1":
                    animator.SetTrigger("VenTa1Trigger");
                    break;
                case "MVenTarugo2":
                    animator.SetTrigger("VenTa2Trigger");
                    break;
                case "MVenTarugo3":
                    animator.SetTrigger("VenTa3Trigger");
                    break;
                case "MVenTarugo4":
                    animator.SetTrigger("VenTa4Trigger");
                    break;
                case "MVenTarugo5":
                    animator.SetTrigger("VenTa5Trigger");
                    break;
                case "MVenTarugo6":
                    animator.SetTrigger("VenTa6Trigger");
                    break;
                case "MVenTarugo7":
                    animator.SetTrigger("VenTa7Trigger");
                    break;
                case "MVenTarugo8":
                    animator.SetTrigger("VenTa8Trigger");
                    break;
                case "MVenTarugo9":
                    animator.SetTrigger("VentanaTarugo9Trigger");
                    break;
                case "MVenTarugo10":
                    animator.SetTrigger("VentanaTarugo10Trigger");
                    break;
                case "MVenTarugo11":
                    animator.SetTrigger("VentanaTarugo11Trigger");
                    break;
                case "MVenTarugo12":
                    animator.SetTrigger("VentanaTarugo12Trigger");
                    break;
                case "MVenTarugo13":
                    animator.SetTrigger("VentanaTarugo13Trigger");
                    break;
                case "MVenTarugo14":
                    animator.SetTrigger("VentanaTarugo14Trigger");
                    break;
                case "MVenTarugo15":
                    animator.SetTrigger("VentanaTarugo15Trigger");
                    break;
                case "MVenTarugo16":
                    animator.SetTrigger("VentanaTarugo16Trigger");
                    break;
                #endregion
                #region ALERO
                //ALERO
                case "MAleTarugo1":
                    animator.SetTrigger("AleroTarugo1Trigger");
                    break;
                case "MAleTarugo2":
                    animator.SetTrigger("AleroTarugo2Trigger");
                    break;
                case "MAleTarugo3":
                    animator.SetTrigger("AleroTarugo3Trigger");
                    break;
                case "MAleTarugo4":
                    animator.SetTrigger("AleroTarugo4Trigger");
                    break;
                case "MAleTarugo5":
                    animator.SetTrigger("AleroTarugo5Trigger");
                    break;
                case "MAleTarugo6":
                    animator.SetTrigger("AleroTarugo6Trigger");
                    break;
                case "MAleTarugo7":
                    animator.SetTrigger("AleroTarugo7Trigger");
                    break;
                case "MAleTarugo8":
                    animator.SetTrigger("AleroTarugo8Trigger");
                    break;
                case "MAleTornillo1":
                    animator.SetTrigger("AleT1Trigger");
                    break;
                case "MAleTornillo2":
                    animator.SetTrigger("AleT2Trigger");
                    break;
                case "MAleTornillo3":
                    animator.SetTrigger("AleT3Trigger");
                    break;
                case "MAleTornillo4":
                    animator.SetTrigger("AleT4Trigger");
                    break;
                case "MAleTornillo5":
                    animator.SetTrigger("AleT5Trigger");
                    break;
                case "MAleTornillo6":
                    animator.SetTrigger("AleT6Trigger");
                    break;
                case "MAleTornillo7":
                    animator.SetTrigger("AleT7Trigger");
                    break;
                case "MAleTornillo8":
                    animator.SetTrigger("AleT8Trigger");
                    break;
                case "MAleTornillo9":
                    animator.SetTrigger("AleT9Trigger");
                    break;
                case "MAleTornillo10":
                    animator.SetTrigger("AleT10Trigger");
                    break;
                case "MAleTornillo11":
                    animator.SetTrigger("AleT11Trigger");
                    break;
                case "MAleTornillo12":
                    animator.SetTrigger("AleT12Trigger");
                    break;
                case "MAleTornillo13":
                    animator.SetTrigger("AleT13Trigger");
                    break;
                case "MAleTornillo14":
                    animator.SetTrigger("AleT14Trigger");
                    break;
                case "MAleTornillo15":
                    animator.SetTrigger("AleT15Trigger");
                    break;
                case "MAleTornillo16":
                    animator.SetTrigger("AleT16Trigger");
                    break;
                #endregion
                #region MUEBLE
                case "TagMuebleTarugo1":
                    animator.SetTrigger("MuebleTarugo1Trigger");
                    break;
                case "TagMuebleTarugo2":
                    animator.SetTrigger("MuebleTarugo2Trigger");
                    break;
                case "TagMuebleTarugo3":
                    animator.SetTrigger("MuebleTarugo3Trigger");
                    break;
                case "TagMuebleTarugo4":
                    animator.SetTrigger("MuebleTarugo4Trigger");
                    break;
                case "TagMuebleTarugo5":
                    animator.SetTrigger("MuebleTarugo5Trigger");
                    break;
                case "TagMuebleTarugo6":
                    animator.SetTrigger("MuebleTarugo6Trigger");
                    break;
                case "TagMuebleTarugo7":
                    animator.SetTrigger("MuebleTarugo7Trigger");
                    break;
                case "TagMuebleTarugo8":
                    animator.SetTrigger("MuebleTarugo8Trigger");
                    break;
                case "TagMuebleTarugo9":
                    animator.SetTrigger("MuebleTarugo9Trigger");
                    break;
                case "TagMuebleTarugo10":
                    animator.SetTrigger("MuebleTarugo10Trigger");
                    break;
                case "TagMuebleTarugo11":
                    animator.SetTrigger("MuebleTarugo11Trigger");
                    break;
                case "TagMuebleTarugo12":
                    animator.SetTrigger("MuebleTarugo12Trigger");
                    break;
                case "TagMuebleTarugo13":
                    animator.SetTrigger("MuebleTarugo13Trigger");
                    break;    
                case "TagMuebleTarugo14":
                    animator.SetTrigger("MuebleTarugo14Trigger");
                    break;   
                case "TagMuebleTarugo15":
                    animator.SetTrigger("MuebleTarugo15Trigger");
                    break;   
                case "TagMuebleTarugo16":
                    animator.SetTrigger("MuebleTarugo16Trigger");
                    break;  
                case "TagMuebleTarugo17":
                    animator.SetTrigger("MuebleTarugo17Trigger");
                    break; 
                case "TagMuebleTarugo18":
                    animator.SetTrigger("MuebleTarugo18Trigger");
                    break;   
                case "TagMuebleTarugo19":
                    animator.SetTrigger("MuebleTarugo19Trigger");
                    break; 
                case "TagMuebleTarugo20":
                    animator.SetTrigger("MuebleTarugo20Trigger");
                    break; 
                case "TagMuebleTarugo21":
                    animator.SetTrigger("MuebleTarugo21Trigger");
                    break; 
                case "TagMuebleTarugo22":
                    animator.SetTrigger("MuebleTarugo22Trigger");
                    break; 
                case "TagMuebleTarugo23":
                    animator.SetTrigger("MuebleTarugo23Trigger");
                    break; 
                case "TagMuebleTarugo24":
                    animator.SetTrigger("MuebleTarugo24Trigger");
                    break;
                case "TagMuebleTarugo25":
                    animator.SetTrigger("MuebleTarugo25Trigger");
                    break;
                case "TagMuebleTarugo26":
                    animator.SetTrigger("MuebleTarugo26Trigger");
                    break;
                case "TagMuebleTarugo27":
                    animator.SetTrigger("MuebleTarugo27Trigger");
                    break;
                case "TagMuebleTornillo1":
                    animator.SetTrigger("MuebleTornillo1Trigger");
                    break;
                case "TagMuebleTornillo2":
                    animator.SetTrigger("MuebleTornillo2Trigger");
                    break;
                case "TagMuebleTornillo3":
                    animator.SetTrigger("MuebleTornillo3Trigger");
                    break;
                case "TagMuebleTornillo4":
                    animator.SetTrigger("MuebleTornillo4Trigger");
                    break;
                case "TagMuebleTornillo5":
                    animator.SetTrigger("MuebleTornillo5Trigger");
                    break;
                case "TagMuebleTornillo6":
                    animator.SetTrigger("MuebleTornillo6Trigger");
                    break;
                case "TagMuebleTornillo7":
                    animator.SetTrigger("MuebleTornillo7Trigger");
                    break;
                case "TagMuebleTornillo8":
                    animator.SetTrigger("MuebleTornillo8Trigger");
                    break;
                case "TagMuebleTornillo9":
                    animator.SetTrigger("MuebleTornillo9Trigger");
                    break;
                case "TagMuebleTornillo10":
                    animator.SetTrigger("MuebleTornillo10Trigger");
                    break;
                case "TagMuebleTornillo11":
                    animator.SetTrigger("MuebleTornillo11Trigger");
                    break;
                case "TagMuebleTornillo12":
                    animator.SetTrigger("MuebleTornillo12Trigger");
                    break;
                case "TagMuebleTornillo15":
                    animator.SetTrigger("MuebleTornillo15Trigger");
                    break;
                case "TagMuebleTornillo16":
                    animator.SetTrigger("MuebleTornillo16Trigger");
                    break;
                case "TagMuebleTornillo17":
                    animator.SetTrigger("MuebleTornillo17Trigger");
                    break;
                case "TagMuebleTornillo18":
                    animator.SetTrigger("MuebleTornillo18Trigger");
                    break;
                case "TagMuebleTornillo19":
                    animator.SetTrigger("MuebleTornillo19Trigger");
                    break;
                case "TagMuebleTornillo20":
                    animator.SetTrigger("MuebleTornillo20Trigger");
                    break;
                case "TagMuebleTornillo21":
                    animator.SetTrigger("MuebleTornillo21Trigger");
                    break;
                case "TagMuebleTornillo22":
                    animator.SetTrigger("MuebleTornillo22Trigger");
                    break;
                case "TagMuebleTornillo23":
                    animator.SetTrigger("MuebleTornillo23Trigger");
                    break;
                case "TagMuebleTornillo24":
                    animator.SetTrigger("MuebleTornillo24Trigger");
                    break;
                case "TagMuebleTornillo25":
                    animator.SetTrigger("MuebleTornillo25Trigger");
                    break;
                case "TagMuebleTornillo26":
                    animator.SetTrigger("MuebleTornillo26Trigger");
                    break;
                case "TagMuebleTornillo27":
                    animator.SetTrigger("MuebleTornillo27Trigger");
                    break;
                case "TagMuebleTornillo28":
                    animator.SetTrigger("MuebleTornillo28Trigger");
                    break;
                case "TagMuebleTornillo29":
                    animator.SetTrigger("MuebleTornillo29Trigger");
                    break;
                case "TagMuebleTornillo30":
                    animator.SetTrigger("MuebleTornillo30Trigger");
                    break;



                    #endregion
                    #region FREGADERO
                    #endregion

                    #region MAYOLICA
                    #endregion
            }
        }
    }
}
