using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity
{
    [Serializable]
    public class LoadGameEntity
    {
        public Int32 UserActivityId;
        public Int32? StepId;
        public String Description;
        public String Status;
        public Int32 TimeElapse;
        public Int32 CorrectSteps;
        public Int32 WrongSteps;
        public String StatusActivity;
        public Int32 StepTutorial;
    }
}
