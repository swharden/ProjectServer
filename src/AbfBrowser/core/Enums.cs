using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public enum RequestAction
    {
        doNothing,
        scanFolderFast,
        scanFolderFull,
        modifyCell,
        modifyExperiment,
        analyzeAbf,
        analyzeTif,
    };

    public class ActionTools
    {
        public static string[] GetActionNames()
        {
            return Enum.GetNames(typeof(RequestAction));
        }

        public static RequestAction GetActionByName(string actionName)
        {
            string[] enumNames = Enum.GetNames(typeof(RequestAction));
            for (int i = 0; i < enumNames.Length; i++)
            {
                if (actionName == enumNames[i])
                    return (RequestAction)i;
            }
            throw new Exception($"{actionName} is not a valid RequestAction");
        }
    }

}
