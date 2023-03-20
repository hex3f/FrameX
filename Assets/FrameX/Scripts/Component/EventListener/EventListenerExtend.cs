using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace FrameX
{
    public static class EventListenerExtend
    {
        #region 工具函数
        private static EventListener GetOrAddEventListener(Component com)
        {
            EventListener lis = com.GetComponent<EventListener>();
            if (lis == null) return com.gameObject.AddComponent<EventListener>();
            else return lis;
        }
        public static void AddEventListener<T>(this Component com, EventType eventType, Action<T, object[]> action, params object[] args)
        {
            EventListener lis = GetOrAddEventListener(com);
            lis.AddListener(eventType, action, args);
        }
        public static void RemoveEventListener<T>(this Component com, EventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
        {
            EventListener lis = GetOrAddEventListener(com);
            lis.RemoveListener(eventType, action, checkArgs, args);
        }
        public static void RemoveAllListener(this Component com, EventType eventType)
        {
            EventListener lis = GetOrAddEventListener(com);
            lis.RemoveAllListener(eventType);
        }
        public static void RemoveAllListener(this Component com)
        {
            EventListener lis = GetOrAddEventListener(com);
            lis.RemoveAllListener();
        }
        #endregion


        #region 鼠标相关事件
        public static void OnMouseEnter(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnMouseEnter, action, args);
        }
        public static void OnMouseExit(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnMouseExit, action, args);
        }
        public static void OnClick(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnClick, action, args);
        }
        public static void OnClickDown(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnClickDown, action, args);
        }
        public static void OnClickUp(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnClickUp, action, args);
        }
        public static void OnDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnDrag, action, args);
        }
        public static void OnBeginDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnBeginDrag, action, args);
        }
        public static void OnEndDrag(this Component com, Action<PointerEventData, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnEndDrag, action, args);
        }
        public static void RemoveClick(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnClick, action, checkArgs, args);
        }
        public static void RemoveClickDown(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnClickDown, action, checkArgs, args);
        }
        public static void RemoveClickUp(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnClickUp, action, checkArgs, args);
        }
        public static void RemoveDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnDrag, action, checkArgs, args);
        }
        public static void RemoveBeginDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnBeginDrag, action, checkArgs, args);
        }
        public static void RemoveEndDrag(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnEndDrag, action, checkArgs, args);
        }

        public static void RemoveMouseEnter(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnMouseEnter, action, checkArgs, args);
        }
        public static void RemoveMouseExit(this Component com, Action<PointerEventData, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnMouseExit, action, checkArgs, args);
        }


        #endregion

        #region 碰撞相关事件

        public static void OnCollisionEnter(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            com.AddEventListener(EventType.OnCollisionEnter, action, args);
        }


        public static void OnCollisionStay(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnCollisionStay, action, args);
        }
        public static void OnCollisionExit(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnCollisionExit, action, args);
        }
        public static void OnCollisionEnter2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnCollisionEnter2D, action, args);
        }
        public static void OnCollisionStay2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnCollisionStay2D, action, args);
        }
        public static void OnCollisionExit2D(this Component com, Action<Collision, object[]> action, params object[] args)
        {
            AddEventListener(com, EventType.OnCollisionExit2D, action, args);
        }
        public static void RemoveCollisionEnter(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionEnter, action, checkArgs, args);
        }
        public static void RemoveCollisionStay(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionStay, action, checkArgs, args);
        }
        public static void RemoveCollisionExit(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionExit, action, checkArgs, args);
        }
        public static void RemoveCollisionEnter2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionEnter2D, action, checkArgs, args);
        }
        public static void RemoveCollisionStay2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionStay2D, action, checkArgs, args);
        }
        public static void RemoveCollisionExit2D(this Component com, Action<Collision2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnCollisionExit2D, action, checkArgs, args);
        }
        #endregion

        #region 触发相关事件
        public static void OnTriggerEnter(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerEnter, action, checkArgs, args);
        }
        public static void OnTriggerStay(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerStay, action, checkArgs, args);
        }
        public static void OnTriggerExit(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerExit, action, checkArgs, args);
        }
        public static void OnTriggerEnter2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerEnter2D, action, checkArgs, args);
        }
        public static void OnTriggerStay2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerStay2D, action, checkArgs, args);
        }
        public static void OnTriggerExit2D(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            AddEventListener(com, EventType.OnTriggerExit2D, action, checkArgs, args);
        }
        public static void RemoveTriggerEnter(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerEnter, action, checkArgs, args);
        }
        public static void RemoveTriggerStay(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerStay, action, checkArgs, args);
        }
        public static void RemoveTriggerExit(this Component com, Action<Collider, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerExit, action, checkArgs, args);
        }
        public static void RemoveTriggerEnter2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerEnter2D, action, checkArgs, args);
        }
        public static void RemoveTriggerStay2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerStay2D, action, checkArgs, args);
        }
        public static void RemoveTriggerExit2D(this Component com, Action<Collider2D, object[]> action, bool checkArgs = false, params object[] args)
        {
            RemoveEventListener(com, EventType.OnTriggerExit2D, action, checkArgs, args);
        }
        #endregion


    }
}
