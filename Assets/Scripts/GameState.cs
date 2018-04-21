using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    [System.Serializable]
    public struct TheEvent
    {
        [SerializeField]
        public float Delay;

        [SerializeField]
        public UnityEvent OnPreEvent;
        
        [SerializeField]
        public UnityEvent OnPostEvent;
        
        public void Execute(MonoBehaviour context)
        {
            context.StartCoroutine(CoExecute());
        }

        IEnumerator CoExecute()
        {
            OnPreEvent.Invoke();
            yield return new WaitForSeconds(Delay);
            OnPostEvent.Invoke();
        }
    }

    public TheEvent OnBeginEvent;
    public TheEvent OnEndEvent;

    public void Begin()
    {
        OnBeginEvent.Execute(this);
        OnBegin();
    }

    protected virtual void OnBegin()
    { }

    public void End()
    {
        OnEndEvent.Execute(this);
        OnEnd();
    }
    
    protected virtual void OnEnd()
    { }

    public virtual void OnUpdate()
    { }
    
    public virtual void OnLateUpdate()
    { }
    
    public virtual void OnFixedUpdate()
    { }
}
