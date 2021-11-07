using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    class Host : IEntity
    {
        /*
         * 
         * STATIC methods
         * 
         */

        public static Host AddHost()  //  Registers & returns a new host entity
        {
            Host theHost = new Host();
            EntityList.Instance.AddEntity(theHost);
            return theHost;
        }

        public static Host AddHost(int X, int Y, int Z)  //  Registers & returns a new host entity at the specified location
        {
            Host theHost = AddHost();
            theHost.Location.X = X;
            theHost.Location.Y = Y;
            theHost.Location.Z = Z;
            return theHost;
        }

        /*
         * 
         * PUBLIC properties
         * 
         */

        public EntityType Type => EntityType.Host;
        public string Label => _label;

        public EntityLocation Location => _loc;


        /*
         * 
         * PUBLIC methods
         * 
         */

        public void SetLabel(string aLabel)
        {
            _label = (aLabel != null) ? aLabel : "";
        }

        /*
         * 
         * PRIVATE fields
         * 
         */

        private string _label = "Host";
        private EntityLocation _loc = new EntityLocation();

        /*
         * 
         * PRIVATE methods
         * 
         */

        private Host()  //  Constructor is private, should always be created via AddHost in any case...
        {
            //  Initialize player-specific code here
        }

    }

}
