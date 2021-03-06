using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug
{
    public class Entities : Singleton<Entities>
    {
        public bool FullAlert = false;

        public int CameraMinSize { get; set; } = 3;

        public int CameraMaxSize { get; set; } = 12;

        public Camera PlayerCam 
        {
            get
            {
                return playerCam;
            }
            set
            {
                if (playerCam != null)
                {
                    playerCam.transform.SetParent(null, false);
                }
                playerCam = value;
                _UpdateCamParent();
            }
        }

        public GameObject PlayerHost
        {
            get
            {
                return (playerHost != null) ? playerHost.GO : null;
            }
            set
            {
                if (value != null)
                {
                    playerHost = FindEntityForObject(value);
                }
                else
                {
                    playerHost = defaultPlayerObject;
                }
                _UpdateCamParent();
            }
        }

        public void RegisterEntity(EntityType aType, GameObject anEntity)
        {
            if (anEntity != null)
            {
                EntityBase existing = FindEntityForObject(anEntity);
                if (existing != null)  // This object is already being tracked...
                {
                    ents.Remove(existing);
                }
                if ((aType == EntityType.Player) && (defaultPlayerObject != null))  // Only one player entity allowed so remove any existing one...
                {
                    ents.Remove(defaultPlayerObject);
                    defaultPlayerObject = null;
                }

                //  Now we're ready to add a new entry for this entity...
                EntityBase newEnt = null;
                if ((aType == EntityType.Host) || (aType == EntityType.NonHost))  // Moveable types!
                {
                    newEnt = new EntityMovable(anEntity);
                }
                else
                {
                    newEnt = new EntityBase(anEntity);
                }
                newEnt.aType = aType;
                ents.Add(newEnt);
                if (aType == EntityType.Player)
                {
                    defaultPlayerObject = newEnt;
                }
                ents.Sort((a, b) =>
                {
                    int rtn = a.aType.CompareTo(b.aType);
                    return rtn;
                }
                );
            }
        }

        public void SetEntityAttribute(GameObject anEntity, EntityAttribute anAttr, float aValue)
        {
            EntityBase e = FindEntityForObject(anEntity);
            if (e != null)
            {
                e[anAttr] = aValue;
            }
        }

        public float GetEntityAttribute(GameObject anEntity, EntityAttribute anAttr)
        {
            float rtn = 0f;
            EntityBase e = FindEntityForObject(anEntity);
            if (e != null)
            {
                rtn = e[anAttr];
            }
            return rtn;
        }

        public void SetEntitySpeed(GameObject anEntity, float aSpeed)
        {
            EntityMovable e = FindEntityForObject(anEntity) as EntityMovable;
            if (e != null)
            {
                e.Speed = aSpeed;
            }
        }

        public void SetEntityLoopPath(GameObject anEntity, bool isLooping)
        {
            EntityMovable e = FindEntityForObject(anEntity) as EntityMovable;
            if (e != null)
            {
                e.Loop = isLooping;
            }
        }

        public void ClearEntityPath(GameObject anEntity)
        {
            EntityMovable e = FindEntityForObject(anEntity) as EntityMovable;
            if (e != null)
            {
                e.ClearPath();
            }
        }

        public void SetEntityDestination(GameObject anEntity, Vector2 aDest)
        {
            EntityMovable e = FindEntityForObject(anEntity) as EntityMovable;
            if (e != null)
            {
                e.SetDestination(aDest);
            }
        }

        public void AddEntityWaypoint(GameObject anEntity, Vector2 aDest, float PauseAt = 0f)
        {
            EntityMovable e = FindEntityForObject(anEntity) as EntityMovable;
            if (e != null)
            {
                e.AddWaypoint(aDest, PauseAt);
            }
        }


        public GameObject FindHostAtPoint(Vector3 anOrigin, bool exactMatch = true)
        {
            float matchTolerance = 1.0f;
            GameObject aTarget = null;
            if (exactMatch)
            {
                Collider2D[] colliders;
                colliders = Physics2D.OverlapCircleAll(new Vector2(anOrigin.x, anOrigin.y), matchTolerance);
                if (colliders.Length > 0)
                {
                    foreach (EntityBase e in ents)
                    {
                        if ((e.aType == EntityType.Host) && (e.GO != PlayerHost))
                        {
                            foreach (Collider2D c in colliders)
                            {
                                if (c.gameObject == e.GO)
                                {
                                    aTarget = e.GO;
                                    break;
                                }
                            }
                        }
                        if (aTarget != null)
                            break;
                    }
                }
            }
            else  //  Find the closest...
            {
                float curDist = float.MaxValue;
                foreach (EntityBase e in ents)
                {
                    if ((e.aType == EntityType.Host) && (e.GO != PlayerHost))  //  This is an entity eligible for takeover!
                    {
                        float thisDist = Vector3.Distance(e.GO.transform.position, anOrigin);
                        if (thisDist < curDist)
                        {
                            aTarget = e.GO;
                            curDist = thisDist;
                        }
                    }
                }
            }
            return aTarget;
        }

        public void JumpToNearest(GameObject anOrigin)
        {
            if (anOrigin == null)
            {
                anOrigin = PlayerHost;
            }
            if (anOrigin != null)
            {
                JumpToNearest(anOrigin.transform.position);
            }
            else
            {
                JumpToNearest(new Vector3(0, 0, 0));
            }
        }

        public void JumpToNearest(Vector3 anOrigin)
        {
            GameObject aTarget = FindHostAtPoint(anOrigin, false); //  Not an exact match, find the closest instead
            PlayerHost = aTarget;
        }

        public void ZoomInCamera()
        {
            if (playerCam != null)
            {                
                float newSize = Mathf.Max((playerCam.orthographicSize / 2), CameraMinSize);
                playerCam.orthographicSize = newSize;
            }
        }

        public void ZoomOutCamera()
        {
            if (playerCam != null)
            {
                float newSize = Mathf.Min((playerCam.orthographicSize * 2), CameraMaxSize);
                playerCam.orthographicSize = newSize;
            }
        }

        private RaycastHit2D[] moveHits = new RaycastHit2D[3];

        public void SetPlayerDestination(Vector2 aPos)
        {
            if (PlayerHost != null)
            {
                EntityMovable ent = FindEntityForObject(PlayerHost) as EntityMovable;
                if (ent != null)
                {
                    ent.SetDestination(aPos);
                }
            }
        }

        public void SetPlayerDestinationOffset(float xDelta, float yDelta)
        {
            if (PlayerHost != null)
            {
                Vector2 aPos = new Vector2(PlayerHost.transform.position.x + xDelta, PlayerHost.transform.position.y + yDelta);
                SetPlayerDestination(aPos);
            }
        }

        public void ProcessAllMovement(float deltaTime)
        {
            foreach (EntityBase eb in ents)
            {
                EntityMovable ent = eb as EntityMovable;
                if (ent != null)
                {
                    ent.SeekPlayer();
                    Vector2 MV = ent.GetMovementVector(deltaTime);
                    ApplyMovement(ent.GO, MV);
                    if (ent.AtTarget())
                    {
                        ent.NextWaypoint();
                    }
                    ent.UpdateAlert(deltaTime);  //  Update the alert status of this entity

                    if (EntityDistanceToPlayer(ent.GO) < 1 && ent.GO != PlayerHost && ent.IsAlert())
                    {
                        GameManagerSingleton.Instance.GameOver(false);
                    }
                }
            }
            ZSort();
        }

        private void ZSort()
        {
            //  Sort all entities by the min Y value of collider bounds -- this will determine Z order of the attached objects (items lower on screen will appear in front of those higher on screen)
            List<EntityMovable> sorters = new List<EntityMovable>();
            foreach (EntityBase eb in ents)
            {
                EntityMovable ent = eb as EntityMovable;
                if (ent != null)
                {
                    sorters.Add(ent);
                }
            }
            sorters.Sort((a, b) => 
            {
                int rtn = 0;
                Collider2D c1 = a.GO.GetComponent<Collider2D>();
                Collider2D c2 = b.GO.GetComponent<Collider2D>();

                if ((c1 != null) && (c2 != null))
                {
                    rtn = c1.bounds.min.y.CompareTo(c2.bounds.min.y);  //  Compare lowest points of each extent
                }
                return rtn;
            }
            );

            float newZ = 1.0f;
            foreach (EntityMovable e in sorters)
            {
                e.GO.transform.position = new Vector3(e.GO.transform.position.x, e.GO.transform.position.y, newZ);
                newZ += 1.0f;
            }
        }

        public bool IsPlayerInSight()
        {
            bool rtn = false;

            foreach (EntityBase eb in ents)
            {
                EntityMovable ent = eb as EntityMovable;
                if ((ent != null) && (ent != playerHost))
                {
                    if (PlayerIsVisibleFrom(ent.GO))
                    {
                        rtn = true;
                    }
                }
            }

            return rtn;
        }

        public void AlertAll()
        {
            FullAlert = true;

            foreach (EntityBase eb in ents)
            {
                EntityMovable ent = eb as EntityMovable;
                if ((ent != null) && (ent != playerHost))
                {
                    ent.RefreshAlert();
                }
            }
        }

        public void ClearAlerts()
        {
            FullAlert = false;

            foreach (EntityBase eb in ents)
            {
                EntityMovable ent = eb as EntityMovable;
                if ((ent != null) && (ent != playerHost))
                {
                    ent.ClearAlert();
                }
            }
        }

        public void ProcessMovement(GameObject anObject, float deltaTime)
        {
            EntityMovable ent = FindEntityForObject(anObject) as EntityMovable;
            if (ent != null)
            {
                Vector2 MV = ent.GetMovementVector(deltaTime);
                ApplyMovement(anObject, MV);
                if (ent.AtTarget())
                {
                    ent.NextWaypoint();
                }
            }
        }

        public void ApplyMovement(GameObject anObject, Vector2 aVector)
        {
            if (anObject != null)
            {
                Vector3 newPos = anObject.transform.position + new Vector3(aVector.x, aVector.y, 0);
                Rigidbody2D rb = anObject.GetComponent<Rigidbody2D>();
                if (rb != null)  //  We have a rigidbody so use that for movement checking...
                {
                    int numHits = rb.Cast(aVector, moveHits, aVector.magnitude);
                    if (numHits > 0)
                    {
                        if(!moveHits[0].collider.isTrigger)
                        {
                            if (Mathf.Abs(moveHits[0].distance) > 0.05)
                            {
                                Vector3 partMove = new Vector3(aVector.x, aVector.y, 0) * moveHits[0].fraction * 0.9f;
                                newPos = anObject.transform.position + partMove;
                            }
                            else
                            {
                                newPos = anObject.transform.position;
                            }
                        }
                    }

                }
                Vector3 actDelta = (newPos - anObject.transform.position);
                actDelta.Normalize();
                Animator a = anObject.GetComponent<Animator>();
                if (a != null)
                {
                    a.SetFloat("Horizontal", actDelta.x);
                    a.SetFloat("Vertical", actDelta.y);
                    a.SetFloat("Speed", (actDelta.sqrMagnitude > 0.1) ? 1 : 0);
                    if(GetAnimationDirection(aVector.x, aVector.y) != -1)
                    {
                        a.SetFloat("Direction", GetAnimationDirection(aVector.x, aVector.y));
                    }
                }
                anObject.transform.position = newPos;
            }
        }

        public void SetPlayerPosition(float anX, float aY)
        {
            if (playerHost != null)
            {
                playerHost.GO.transform.position = new Vector3(anX, aY, playerHost.GO.transform.position.z);
            }
        }

        public Vector2 GetPlayerPosition()
        {
            return playerHost.pos;
        }

        public void SetPlayerPosition(Vector2 aPos)
        {
            SetPlayerPosition(aPos.x, aPos.y);
        }

        public float EntityDistanceToPlayer(GameObject anEntity)
        {
            float rtn = float.NaN;
            if (playerHost != null)
            {
                rtn = playerHost.DistanceTo(FindEntityForObject(anEntity));
            }
            return rtn;
        }

        public bool PlayerIsVisibleFrom(GameObject anEntity)
        {
            bool rtn = false;
            if (playerHost != null)  //  There's a player to be visible...
            {
                EntityBase e = FindEntityForObject(anEntity);
                if ((e != null) && (e != playerHost))  //  We're talking about an actual entity other than the player...
                {
                    if (playerHost.DistanceTo(e) <= e[EntityAttribute.ViewDistance] && e.GO.GetComponent<RegisteredEntity>().HostType != HostCharactersType.Bug)  //  The player is within the entity's viewing distance so *might* be visible...
                    {
                        rtn = true;  //  Assume player in range is visible unless we hit a collider between points...
                        List<RaycastHit2D> hits = new List<RaycastHit2D>();
                        ContactFilter2D filter = new ContactFilter2D().NoFilter();
                        filter.useTriggers = false;
                        int numHits = Physics2D.Linecast(e.pos, playerHost.pos, filter, hits);

                        //RaycastHit2D[] hits = Physics2D.LinecastAll(e.pos, playerHost.pos);
                        foreach (RaycastHit2D hit in hits)
                        {
                            if (hit.collider != null)
                            {
                                if ((hit.rigidbody == null) || ((hit.rigidbody.gameObject != e.GO) && (hit.rigidbody.gameObject != playerHost.GO)))  //  We hit 
                                {
                                    rtn = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return rtn;
        }

        public int PlayerVisibleList(List<GameObject> VisibleList)
        {
            if (VisibleList != null)
            {
                VisibleList.Clear();
            }
            else
            {
                VisibleList = new List<GameObject>();
            }

            foreach (EntityBase e in ents)
            {
                if ((e.GO != null) && (PlayerIsVisibleFrom(e.GO)))
                {
                    VisibleList.Add(e.GO);
                }
                if(e == playerHost)
                {
                    VisibleList.Add(e.GO);
                }
            }

            return VisibleList.Count;
        }

        public int PlayerVisibleCount()
        {
            int rtn = 0;
            foreach (EntityBase e in ents)
            {
                if ((e.GO != null) && (PlayerIsVisibleFrom(e.GO)))
                {
                    rtn++;
                }
            }
            return rtn;
        }

        public void Reset()
        {
            ents.Clear();
            playerHost = null;
            defaultPlayerObject = null;
            PlayerCam = Camera.main;
            playerCam.transform.position = new Vector3(0f, 0f, -100f);
        }

        private Camera playerCam = null;
        private EntityBase playerHost = null;
        private EntityBase defaultPlayerObject = null;
        private List<EntityBase> ents = new List<EntityBase>();

        private float GetAnimationDirection(float x, float y)
        {
            float rtn = -1;

            if (y > 0 && x == 0) // Up
            {
                rtn = 5f;
            }
            if (y > 0 && x > 0) // Up right
            {
                rtn = 7f;
            }
            if (y == 0 && x > 0) // Right
            {
                rtn = 4f;
            }
            if (y < 0 && x > 0) // Down Right
            {
                rtn = 2f;
            }
            if (y < 0 && x == 0) // Down
            {
                rtn = 0f;
            }
            if (y < 0 && x < 0) // Down Left
            {
                rtn = 1f;
            }
            if (y == 0 && x < 0) // Left
            {
                rtn = 3f;
            }
            if (y > 0 && x < 0) // Up Left
            {
                rtn = 6f;
            }
            return rtn;
        }
        private EntityBase FindEntityForObject(GameObject anObj)
        {
            EntityBase rtn = null;
            foreach (EntityBase eb in ents)
            {
                if (eb.GO == anObj)
                {
                    rtn = eb;
                    break;
                }
            }
            return rtn;
        }

        public void _UpdateCamParent()
        {
            if (playerCam != null)
            {
                playerCam.transform.SetParent((playerHost != null) ? playerHost.GO.transform : null, false);
            }
        }

    }


}
