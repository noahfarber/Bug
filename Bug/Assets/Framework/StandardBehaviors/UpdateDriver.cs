using System.Collections.Generic;

public interface IUpdateableClass
{
    void DoUpdate();
}

public class UpdateDriver : SingletonMonoBehaviour<UpdateDriver>
{
    private List<IUpdateableClass> dispatchList = new List<IUpdateableClass>();
    private static readonly object _lockObj = new object();

	// Update is called once per frame
	void Update () {
		lock(_lockObj)
        {
            foreach (IUpdateableClass anObj in dispatchList)
            {
                anObj.DoUpdate();
            }
        }
	}

    public void RegisterForUpdate(IUpdateableClass anObj)
    {
        lock(_lockObj)
        {
            if (!dispatchList.Contains(anObj))
            {
                dispatchList.Add(anObj);
            }
        }
    }

    public void UnregisterForUpdate(IUpdateableClass anObj)
    {
        lock(_lockObj)
        {
            dispatchList.Remove(anObj);
        }
    }
}
