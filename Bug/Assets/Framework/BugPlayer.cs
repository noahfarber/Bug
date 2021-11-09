using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    class Player : MoveableEntity
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

        public override bool Enabled 
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = true;  //  Player object must ALWAYS be enabled!
            }
        }


        public IEntity CurrentHost { get; protected set; } = null;  //  Start out with no host at all!

        /*
         * 
         * PUBLIC methods
         * 
         */

        public void SetHost(IEntity newHost)
        {
            if ((newHost != null) && (newHost.Type == EntityType.Host))  //  First validate the target host
            {
                CurrentHost = newHost;
            }
        }

        public double EffectiveAttribute(EntityAttribute attrib)
        {
            double rtn = AttributeValue(attrib);
            if (CurrentHost != null)
            {
                rtn += CurrentHost.AttributeValue(attrib);  //  Player's attribute is additive to the current host's attribute
            }
            return rtn;
        }

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

        private Player()  //  Constructor is private, should always be created via InitPlayer in any case...
        {
            //  Initialize player-specific code here

            //  Set up initial attributes
            foreach (EntityAttribute a in Enum.GetValues(typeof(EntityAttribute)))
            {
                if ((a != EntityAttribute.Unknown) && (a != EntityAttribute.FinalAttribute))  //  Player gets all attributes!
                {
                    SetAttribute(a, 0.0f);
                }
            }
            SetAttribute(EntityAttribute.Intelligence, 10.0f);  //  Alien bug is hyper-intelligent!
        }

    }
}
