using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity
{
    [Serializable]
    public class StatisticsEntity
    {
        public Int32 Correctas;
        public Int32 Incorrectas;
        public String Titulo;
        public Int32 TiempoTranscurrido;
        public Int32 StepTutorial;
    }
}
