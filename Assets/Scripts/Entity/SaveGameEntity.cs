using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity
{
    [Serializable]
    public class SaveGameEntity
    {
        public Int32 UserActivityId;
        public Int32? StepId;
        public String Description;
        public String Status;
        public Int32 TimeElapse;
        public int WrongSteps;
        public int CorrectSteps;
        public int StepTutorial;
        public string StatusActivity;
    }
}
