using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    class Host : BaseEntity
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


        /*
         * 
         * PUBLIC methods
         * 
         */

        /*
         * 
         * PRIVATE fields
         * 
         */

        /*
         * 
         * PRIVATE methods
         * 
         */

        private Host()  //  Constructor is private, should always be created via AddHost in any case...
        {
            //  Initialize player-specific code here

            //  Set up initial attributes
            foreach (EntityAttribute a in Enum.GetValues(typeof(EntityAttribute)))
            {
                if ((a != EntityAttribute.Unknown) && (a != EntityAttribute.FinalAttribute))  //  Host gets all attributes
                {
                    SetAttribute(a, 0.0f);
                }
            }
        }

    }

}
