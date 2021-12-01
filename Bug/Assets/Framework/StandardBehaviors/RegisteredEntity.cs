using UnityEngine;
using Bug;

public class RegisteredEntity : MonoBehaviour
{
    public EntityType EntType;
    public float MoveSpeed = 5.0f;
    public int ClearanceLevel = 0;
    [HideInInspector] public HostCharactersType HostType;

    public void Register()
    {
        Entities.Instance.RegisterEntity(EntType, gameObject);
        Entities.Instance.SetEntitySpeed(gameObject, MoveSpeed);
    }
}
