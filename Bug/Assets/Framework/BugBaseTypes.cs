using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug
{
    public enum EntityType
    {
        Unknown = -1,  //  Establishes the minimum value, ensures valid types start with 0 and up
        Player,   //  Single instance with info about the player character
        Host,     //  Multi instance for entities that *may* become hosts
        NonHost,  //  Multi instance for entities that may *not* become hosts

        FinalEntity  //  Always add new types before this one!
    }

    public enum EntityAttribute
    {
        Unknown = -1,
        Charisma,
        Dexterity,
        Endurance,
        Intelligence,
        Luck,
        Perception,
        Speed,
        Strength,

        FinalAttribute  //  Always add new attribs before this one!
    }

    public class EntityLocation
    {
        //  Maintains 3D coordinates in full (float) world resolution with convenience references in normal (int) screen resolution
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;
        public float Z { get; set; } = 0;

        public int NormX
        {
            get
            {
                return (int)Math.Round(X);
            }
        }

        public int NormY 
        {
            get
            {
                return (int)Math.Round(Y);
            }
        }

        public int NormZ 
        {
            get
            {
                return (int)Math.Round(Z);
            }
        }

        public float DistanceTo(EntityLocation aPoint)  //  Simple distance calc to another location in 3 dimensions
        {
            return DistanceTo(aPoint.X, aPoint.Y, aPoint.Z);
        }

        public float DistanceTo(float anX, float aY, float aZ)  //  Simple distance calc to an arbitrary 3D coordinate
        {
            float rtn = (float)Math.Sqrt(Math.Pow((X - anX), 2) + Math.Pow((Y - aY), 2) + Math.Pow((Z - aZ), 2));

            return rtn;
        }

        public void Move(float ofsX, float ofsY, float ofsZ = 0.0f)  //  Move the location in world terms by the distance specified in each axis (usually these won't move in the Z-axis so defaults to 0)
        {
            X = X + ofsX;
            Y = Y + ofsY;
            Z = Z + ofsZ;
        }

        public float RotationTo(EntityLocation aPoint)  //  Angular rotation in the xy-plane from this location to the referenced point
        {
            float rtn = 0.0f;
            float dX = aPoint.X - X;
            float dY = -(aPoint.Y - Y);  //  Screen space coords are reversed "polarity"
            float inRads = (float)Math.Atan2(dY, dX);
            if (inRads < 0)
                inRads = Math.Abs(inRads);
            else
                inRads = (float)((2 * Math.PI) - inRads);

            rtn = (float)(inRads * (180 / Math.PI));

            return rtn;
        }

        public float AltitudeTo(EntityLocation aPoint)  //  Angular rotation from the z-origin to the referenced point
        {
            float rtn = 0.0f;

            //  First we're going to assume a "normalized" distance in the XY-plane as a baseline
            float dXY = DistanceTo(aPoint.X, aPoint.Y, Z);  //  Assume the target is on the same Z-plane for a baseline
            float dZ = aPoint.Z - Z;
            float inRads = (float)Math.Atan2(dZ, dXY);
            if (inRads < 0)
                inRads = Math.Abs(inRads);
            else
                inRads = (float)((2 * Math.PI) - inRads);

            rtn = (float)(inRads * (180 / Math.PI));

            return rtn;
        }
    }

    public interface IEntity
    {
        public EntityType Type { get; }
        public string Label { get; }
        public EntityLocation Location { get; }

        public bool Enabled { get; set; }

        public float AttributeValue(EntityAttribute aType);  //  Returns the current value of a given attribute (should default to 0 if the attribute does not exist for the entity)
    }

    public interface ICollidableEntity : IEntity
    {
        public float Radius { get; }
        public bool Collided(ICollidableEntity aTarget);
    }

    public interface IProcessingEntity : IEntity
    {
        public void DoProcess(float deltaTime);  // Used to process a time slice for this entity
    }




    /*
     * 
     * Standard base implementations of interfaces
     * 
     */

    public delegate void EntityProcess(float deltaTime);

    public class BaseEntity : ICollidableEntity, IProcessingEntity
    {
        /*
         * 
         * PUBLIC properties
         * 
         */

        public EntityType Type { get; protected set; } = EntityType.Unknown;
        public virtual string Label { get; protected set; } = "Unknown";
        public virtual bool Enabled { get; set; } = true;
        public EntityLocation Location { get; protected set; } = new EntityLocation();
        public virtual float Radius { get; set; } = 0.0f;

        public event EntityProcess OnProcess;


        /*
         * 
         * PUBLIC methods
         * 
         */

        public void SetLabel(string aLabel)
        {
            Label = (aLabel != null) ? aLabel : "";  //  Ensure that label is never null
        }

        public float AttributeValue(EntityAttribute aType)
        {
            float rtn = 0.0f;
            lock(_lockObj)
            {
                if (_attribs.ContainsKey(aType))
                {
                    rtn = _attribs[aType];
                }
            }
            return rtn;
        }

        public void SetAttribute(EntityAttribute aType, float aValue)
        {
            lock(_lockObj)
            {
                _attribs[aType] = aValue;
            }
        }

        public void AdjustAttribute(EntityAttribute aType, float anOffset)
        {
            float curVal = AttributeValue(aType);  //  Calling the getter ensures that if the attrib doesn't already exist, we start with 0
            lock (_lockObj)
            {
                _attribs[aType] = curVal + anOffset;  
            }
        }

        public bool Collided(ICollidableEntity aTarget)   // Very simple circular/spherical collision detection
        {
            bool rtn = false;
            if (aTarget != null)
            {
                float minDist = Radius + aTarget.Radius;
                float actDist = Location.DistanceTo(aTarget.Location);
                rtn = (actDist <= minDist);
            }
            return rtn;
        }

        public void DoProcess(float deltaTime)
        {
            OnProcess?.Invoke(deltaTime);
        }

        /*
         * 
         * PRIVATE fields
         * 
         */

        private readonly object _lockObj = new object();
        private Dictionary<EntityAttribute, float> _attribs = new Dictionary<EntityAttribute, float>();
    }


}
