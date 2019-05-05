using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public enum Action
    {
        doNothing,
        scanFolderFast,
        scanFolderFull,
        modifyCell,
        modifyExperiment,
        analyzeAbf,
        analyzeTif,
        error,
    };

    public class ActionTools
    {
        public static string[] GetActionNames()
        {
            return Enum.GetNames(typeof(Action));
        }

        public static Action GetActionByName(string actionName)
        {
            string[] enumNames = Enum.GetNames(typeof(Action));
            for (int i = 0; i < enumNames.Length; i++)
            {
                if (actionName == enumNames[i])
                    return (Action)i;
            }
            return Action.error;
        }
    }

}
