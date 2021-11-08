using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bug
{
    public sealed class EntityList
    {
        public static EntityList Instance { get { return _instance; } }

        public int Count 
        {
            get
            {
                int rtn = 0;
                lock (_lockObj)
                {
                    rtn = _list.Count;
                }
                return rtn;
            }
        }
        public IEntity this[int index] 
        { 
            get 
            {
                return _GetItemByIndex(index);
            } 
        }

        public void AddEntity(IEntity anEnt)
        {
            if (anEnt != null)
            {
                lock (_lockObj)
                {
                    _list.Add(anEnt);
                }
            }
        }

        public void RemoveEntity(IEntity anEnt)
        {
            if (anEnt != null)
            {
                lock(_lockObj)
                {
                    _list.Remove(anEnt);
                }
            }
        }

        public IEntity Find(string aLabel)
        {
            IEntity rtn = null;
            lock(_lockObj)
            {
                foreach (IEntity ent in _list)
                {
                    if ((ent != null) && (ent.Label.Equals(aLabel)))
                    {
                        rtn = ent;
                        break;
                    }
                }
            }
            return rtn;
        }

        public IList<IEntity> Matching(Predicate<IEntity> aFilter)
        {
            List<IEntity> rtn = new List<IEntity>();
            lock(_lockObj)
            {
                foreach (IEntity ent in _list)
                {
                    if ((ent != null) && (aFilter(ent)))
                    {
                        rtn.Add(ent);
                    }
                }
            }
            return rtn;
        }

        public void ProcessEntities(float deltaTime)
        {
            IList<IEntity> procList = Matching(
                (x) =>
                {
                    bool rtn = (x is IProcessingEntity);
                    rtn = (rtn && (x != null) && x.Enabled);
                    return rtn;
                }
                ).OrderBy(f => f.Type).ToList();
            foreach (IEntity e in procList)
            {
                IProcessingEntity pe = e as IProcessingEntity;
                if (pe != null)
                {
                    pe.DoProcess(deltaTime);
                }
            }
        }

        private static readonly EntityList _instance = new EntityList();
        private readonly object _lockObj = new object();

        private List<IEntity> _list = new List<IEntity>();

        private EntityList()
        {
            _list.Clear();
        }

        private IEntity _GetItemByIndex(int anIDX)
        {
            IEntity rtn = null;
            lock(_lockObj)
            {
                if ((anIDX >= 0) && (anIDX < _list.Count))
                {
                    rtn = _list[anIDX];
                }
            }
            return rtn;
        }

    }
}
