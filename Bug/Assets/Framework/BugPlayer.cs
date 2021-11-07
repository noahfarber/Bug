using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    class Player : IEntity
    {
        public static void MakePlayer()
        {
            if (EntityList.Instance.Find("Player") == null)  //  We don't already have the player...
            {
                Player thePlayer = new Player();
                EntityList.Instance.AddEntity(thePlayer);
            }
        }

        public EntityType Type => EntityType.Player;
        public string Label => "Player";

        public EntityLocation Location => _loc;

        public Player()
        {
            //  Initialize player-specific code here
        }

        private EntityLocation _loc = new EntityLocation(0, 0, 0);

    }
}
