using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class SaveModel
    {
        public bool Colocado { set; get; }
        public string Etiqueta { set; get; }
        public Vector3 Posicion { set; get; }
        public Quaternion Rotacion { set; get; }
    }
}
