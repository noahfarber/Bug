using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug;

namespace Bug
{
    public interface IManagedInput
    {
        public void ProcessInput();
    }

    public interface ISystemInput
    {
        public bool ProcessInput();  //  Returns TRUE if this function captured input and should not call the managed input on that update cycle
    }

    public class InputManager : Singleton<InputManager>, IUpdateableClass
    {
        public bool GamePaused 
        {
            get
            {
                return (Time.timeScale == 0f);
            }
        }

        public void PauseGame()
        {
            Debug.Log("Pausing Game");
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }

        public void UnpauseGame()
        {
            Debug.Log("Unpausing Game");
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }

        public void ActivateInput(IManagedInput anObj)
        {
            lock(_lockObj)
            {
                _inStack.Push(anObj);
            }
        }

        public void DeactivateInput(IManagedInput anObj)
        {
            lock(_lockObj)
            {
                //  This should normally be called only for the current object at the top of the stack, but just in case we'll unwind the stack until we find the desired object, then rewind the stack after
                Stack<IManagedInput> unwind = new Stack<IManagedInput>();
                while ((_inStack.Count > 0) && (_inStack.Peek() != anObj))
                {
                    unwind.Push(_inStack.Pop());  //  Pop the item off the stack and push onto the unwind holder
                }
                if ((_inStack.Count > 0) && (_inStack.Peek() == anObj))  //  We found the object!
                {
                    _inStack.Pop();
                }
                while (unwind.Count > 0)
                {
                    _inStack.Push(unwind.Pop());  //  Pop the item off the unwind holder and back onto the stack
                }
            }
        }

        public void RegisterSystemInput(ISystemInput aSysObj)
        {
            lock(_lockObj)
            {
                _sysObj = aSysObj;
            }
        }

        public void DoUpdate()
        {
            if (_sysObj != null)
            {
                _sysObj.ProcessInput();
            }
            if ((_inStack.Count > 0) && (Time.timeScale != 0f))  //  If timescale is 0, game is paused so no managed input routines should be called!
            {
                _inStack.Peek().ProcessInput();
            }
        }

        public InputManager() : base()
        {
            UpdateDriver.Instance.RegisterForUpdate(this);
        }

        private readonly object _lockObj = new object();
        private ISystemInput _sysObj = null;
        private Stack<IManagedInput> _inStack = new Stack<IManagedInput>();
    }

}

