using UnityEngine;
using Bug;

public class RegisteredEntity : MonoBehaviour
{
    public EntityType EntType;
    public float MoveSpeed = 5.0f;

    private void Awake()
    {
        Entities.Instance.RegisterEntity(EntType, gameObject);
        Entities.Instance.SetEntitySpeed(gameObject, MoveSpeed);
    }

}
