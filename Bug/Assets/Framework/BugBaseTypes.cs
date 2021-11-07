using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug
{
    public enum EntityType
    {
        Player,
        Host
    }

    public struct EntityLocation
    {
        public int x;
        public int y;
        public int z;

        public EntityLocation(int anX, int aY, int aZ)
        {
            x = anX;
            y = aY;
            z = aZ;
        }

        public double DistanceTo(EntityLocation aPoint)  //  Simple distance calc to another location in 3 dimensions
        {
            double rtn = Math.Sqrt(Math.Pow((x - aPoint.x), 2) + Math.Pow((y - aPoint.y), 2) + Math.Pow((z - aPoint.z), 2));

            return rtn;
        }
    }

    public interface IEntity
    {
        public EntityType Type { get; }
        public string Label { get; }
        public EntityLocation Location { get; }
    }



}
