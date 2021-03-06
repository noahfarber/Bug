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
        ViewDistance,
        AlertTime,
        PathPauseTime,

        FinalAttribute  //  Always add new attribs before this one!
    }

    public class EntityBase
    {
        public GameObject GO { get { return _go; } }
        public EntityType aType = EntityType.Unknown;

        public EntityBase(GameObject anObj)
        {
            _go = anObj;
        }

        public float this[EntityAttribute attr] 
        {
            get
            {
                float rtn = 0f;
                if (_attrs.ContainsKey(attr))
                {
                    rtn = _attrs[attr];
                }
                return rtn;
            }
            set
            {
                _attrs[attr] = value;
            }
        }

        public float DistanceTo(EntityBase aTarget)
        {
            float rtn = float.NaN;
            if (aTarget != null)
            {
                if ((GO != null) && (aTarget.GO != null))
                {
                    rtn = Vector2.Distance(pos, aTarget.pos);
                }
            }
            return rtn;
        }

        public Vector2 pos 
        {
            get
            {
                Vector2 rtn = new Vector2(0, 0);
                if (GO != null)
                {
                    rtn.x = GO.transform.position.x;
                    rtn.y = GO.transform.position.y;
                }
                return rtn;
            }
        }

        protected GameObject _go = null;
        protected Dictionary<EntityAttribute, float> _attrs = new Dictionary<EntityAttribute, float>();
    }

    public class EntityMovable : EntityBase
    {
        public EntityMovable(GameObject anObj) : base(anObj)
        {
            //  Set default attributes for this type of entity...
            this[EntityAttribute.ViewDistance] = 10f;
            this[EntityAttribute.AlertTime] = 0f;  //  This attribute tracks how much longer the entity will be on "alert" status, seeking to chase the player if visible... 0 = no alert

        }

        public bool IsAlert()
        {
            return this[EntityAttribute.AlertTime] > 0f;
        }

        public void RefreshAlert()
        {
            this[EntityAttribute.AlertTime] = 5.0f;  //  Reset alert status to 5 seconds of chasing player... this should be decremented regularly when the player is not visible...
        }

        public void ClearAlert()
        {
            this[EntityAttribute.AlertTime] = 0f;  //  Reset alert status to 0 seconds to stop chasing player
        }

        public void UpdateAlert(float deltaTime)
        {
            if (this[EntityAttribute.AlertTime] > 0f)
            {
                if (Entities.Instance.PlayerIsVisibleFrom(GO))
                {
                    RefreshAlert();  //  If the player is in sight, reset the clock!
                }
                else
                {
                    this[EntityAttribute.AlertTime] = Mathf.Max(0f, this[EntityAttribute.AlertTime] - deltaTime);  //  Tick off the clock, clamped at 0
                }
            }
        }

        private bool shownWarning = false;
        public void SeekPlayer()
        {
            //  If the entity is on alert and the player is visible, pause the path and set the temp destination to the player location
            if (Entities.Instance.PlayerIsVisibleFrom(GO))
            {
                SuspicionMeterSingleton.Instance.AddSuspicion(Time.deltaTime / 50);

                if (SuspicionMeterSingleton.Instance.Meter.Amount >= .9f)
                {
                    RefreshAlert();

                    if(!shownWarning)
                    {
                        InteractionTextSingleton.Instance.SetText("RUN!\nYou've been spotted!");
                        //DialogSingleton.Instance.SpawnDialog("SYSTEM", "You've been suspected! Run from sight to get away. \n And don't get caught!");
                        shownWarning = true;
                    }
                }

                if ((this[EntityAttribute.AlertTime] > 0f) && GO.GetComponent<RegisteredEntity>() != null && GO.GetComponent<RegisteredEntity>().HostType != HostCharactersType.Bug)
                {
                    PausedPath = true;  //  First pause this entity's path, if it isn't already
                    SetDestination(Entities.Instance.GetPlayerPosition());
                }
            }

            if (SuspicionMeterSingleton.Instance.Meter.Amount < .9f)
            {
                shownWarning = false;
                //InteractionTextSingleton.Instance.ClearText();
            }
        }

        public void SetDestination(Vector2 aLoc)
        {
            if (_isPaused)
            {
                //  Special behavior, while paused SetDestination creates a temporary destination that doesn't impact the saved path
                _tempDest = aLoc;
            }
            else
            {
                _path.Clear();
                _path.Add((aLoc, 0f));
                _curTarget = 0;
            }
        }

        public void AddWaypoint(Vector2 aLoc, float PauseAt = 0f)
        {
            _path.Add((aLoc, PauseAt));
            if (_curTarget < 0)
            {
                _curTarget = 0;
            }
        }

        public bool HasPath 
        {
            get
            {
                return (_path.Count > 0);
            }
        }

        public bool PausedPath 
        {
            get
            {
                return _isPaused;
            }
            set
            {
                if (value != _isPaused)  //  We're changing the value
                {
                    _isPaused = value;
                    if (_isPaused)
                    {
                        ClearPath();  //  If we're setting the flag then start by assuming we don't need to move
                    }
                }
            }
        }

        public void ClearPath()
        {
            if (_isPaused)  //  Set temp destination to current location
            {
                _tempDest = _go.transform.position;
            }
            else  //  Clear the entire path
            {
                _path.Clear();
                _curTarget = -1;
            }
        }

        public bool AtTarget(float radius = 0.01f)
        {
            bool rtn = false;
            float dist = 0.0f;
            if (_isPaused)
            {
                dist = Vector2.Distance(_tempDest, (Vector2)_go.transform.position);
            }
            else
            {
                if ((_curTarget > -1) && (_curTarget < _path.Count))
                    dist = Vector2.Distance(_path[_curTarget].Item1, (Vector2)_go.transform.position);
            }
            if (dist < radius)
            {
                rtn = true;
            }
            return rtn;
        }

        public void NextWaypoint()
        {
            if (_isPaused)  //  Moving to temp dest so clear the path...
            {
                ClearPath();
            }
            else
            {
                if (Loop)  //  Leave the current path alone and just increment the target pointer...
                {
                    this[EntityAttribute.PathPauseTime] = (_path[_curTarget].Item2 > 0f) ? _path[_curTarget].Item2 : 0f;  //  Establish the time to pause at this point if any, or 0 if none
                    _curTarget++;
                    if (_curTarget >= _path.Count)
                    {
                        _curTarget = Mathf.Min(0, _path.Count-1);
                    }
                }
                else
                {
                    while (_curTarget > 0)  //  If we've been saving the path so far for some reason, trim the front...
                    {
                        _path.RemoveAt(0);
                        _curTarget--;
                    }
                    //  Now the first point on the path is the point we're moving past...
                    if ((_curTarget == 0) && (_curTarget < _path.Count))
                    {
                        this[EntityAttribute.PathPauseTime] = (_path[0].Item2 > 0f) ? _path[0].Item2 : 0f;  //  Establish the time to pause at this point if any, or 0 if none
                        _path.RemoveAt(0);
                        _curTarget = Mathf.Min(0, _path.Count - 1);
                    }
                }
            }
        }

        public Vector2 GetMovementVector(float deltaTime)
        {
            Vector2 nextMove = (Vector2)_go.transform.position;  //  Start by assuming we're moving to our current location (no movement)
            if (_isPaused)
            {
                nextMove = _tempDest;
            }
            else
            {
                this[EntityAttribute.PathPauseTime] = Mathf.Max(0f, this[EntityAttribute.PathPauseTime] - deltaTime);
                if ((_curTarget > -1) && (_curTarget < _path.Count) && (this[EntityAttribute.PathPauseTime] == 0f))
                {
                    nextMove = _path[_curTarget].Item1;
                }
            }

            Vector2 rtn = nextMove - (Vector2)_go.transform.position;  //  First get actual movement to destination
            if (rtn.sqrMagnitude > 0.01f)  //  We are far enough away to normalize the movement
            {
                rtn.Normalize();
                //  Snap to 45 degree motion...
/*                if (Mathf.Abs(rtn.x) >= 0.5)  //  We're strongly moving in the x direction so kill the Y component
                {
                    rtn.y = 0.0f;
                }
                else if (Mathf.Abs(rtn.y) >= 0.5)  //  We're strongly moving in the y direction so kill the X component
                {
                    rtn.x = 0.0f;
                }

                //  Now make x/y full magnitude of 1 or 0
                rtn.x = (rtn.x < 0) ? -1 : (rtn.x > 0) ? 1 : 0;
                rtn.y = (rtn.y < 0) ? -1 : (rtn.y > 0) ? 1 : 0;*/
                rtn *= (Speed * deltaTime);
            }

            return rtn;
        }

        public bool Loop { get; set; }
        public float Speed { get; set; }

        private List<(Vector2, float)> _path = new List<(Vector2, float)>();
        private Vector2 _tempDest;
        private int _curTarget = -1;
        private bool _isPaused = false;

    }

}
