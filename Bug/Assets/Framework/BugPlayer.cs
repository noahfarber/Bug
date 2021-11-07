using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    class Player : IEntity
    {
        /*
         * 
         * STATIC methods
         * 
         */

        public static Player InitPlayer()  //  Ensures that one player entity is registered, returns the reference to that entity
        {
            Player thePlayer = FindPlayer() as Player;
            if (thePlayer == null)  //  We don't already have the player...
            {
                thePlayer = new Player();
                EntityList.Instance.AddEntity(thePlayer);
            }
            return thePlayer;
        }

        public static Player InitPlayer(int X, int Y, int Z)  //  Ensures that one player entity is registered at the (normalized) location specified
        {
            Player thePlayer = InitPlayer();
            thePlayer.Location.X = X;
            thePlayer.Location.Y = Y;
            thePlayer.Location.Z = Z;
            return thePlayer;
        }

        public static IEntity FindPlayer()
        {
            IList<IEntity> pList = EntityList.Instance.Matching((x) => x.Type == EntityType.Player);  //  *should* only ever return 0 or 1 items, but in any case return the first found if any
            return (pList.Count > 0) ? pList[0] : null;
        }

        /*
         * 
         * PUBLIC properties
         * 
         */

        public EntityType Type => EntityType.Player;
        public string Label => "Player";

        public EntityLocation Location => _loc;


        /*
         * 
         * PRIVATE fields
         * 
         */

        private EntityLocation _loc = new EntityLocation();

        /*
         * 
         * PRIVATE methods
         * 
         */

        private Player()  //  Constructor is private, should always be created via InitPlayer in any case...
        {
            //  Initialize player-specific code here
        }

    }
}
