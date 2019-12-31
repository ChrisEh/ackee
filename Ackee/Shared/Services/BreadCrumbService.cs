using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Shared.Services;

namespace Ackee.Shared.Services
{
    public class BreadCrumbService
    {
        private StringManipulationService stringManipulationService;
        public BreadCrumbService(StringManipulationService stringManipulationService)
        {
            this.stringManipulationService = stringManipulationService;
        }
 
        public List<BreadCrumb> GetBreadCrumbs(string route, List<int> indexesWithCustomNames, List<string> customNames)
        {
            List<BreadCrumb> breadCrumbs = new List<BreadCrumb>();

            var routeParts = route.Split('/', StringSplitOptions.RemoveEmptyEntries);
            int nameIndex = 0;

            for (var i = 0 ; i < routeParts.Length ; i++ )
            {
                BreadCrumb bc = new BreadCrumb();                
                if (indexesWithCustomNames.Count > 0 && customNames.Count > 0 && indexesWithCustomNames.Contains(i))
                {
                    bc.Name = customNames[nameIndex++];                    
                }
                else
                {
                    bc.Name = stringManipulationService.CapitalizeFirstLetterOfString(routeParts[i]);
                }

                string bcRoute = "";

                for (var j = 0; j <= i; j++)
                {
                    bcRoute += $"/{routeParts[j]}";
                }

                bc.Route = bcRoute;
                breadCrumbs.Add(bc);
            }

            return breadCrumbs;
        }
    }
}
