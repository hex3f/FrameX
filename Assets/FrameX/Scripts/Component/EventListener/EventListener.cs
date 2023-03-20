using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace FrameX
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        OnMouseEnter,
        OnMouseExit,
        OnClick,
        OnClickDown,
        OnClickUp,
        OnDrag,
        OnBeginDrag,
        OnEndDrag,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnCollisionEnter2D,
        OnCollisionStay2D,
        OnCollisionExit2D,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit,
        OnTriggerEnter2D,
        OnTriggerStay2D,
        OnTriggerExit2D,
    }

    public interface IMouseEvent : IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    { }

    /// <summary>
    /// 事件工具
    /// 可以添加 鼠标、碰撞、触发等事件
    /// </summary>
    public class EventListener : MonoBehaviour, IMouseEvent
    {

        #region 内部类、接口等
        /// <summary>
        /// 某个事件中一个时间的数据包装类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class EventListenerEventInfo<T>
        {
            // T：事件本身的参数（PointerEventData、Collision）
            // object[]:事件的参数
            public Action<T, object[]> action;
            public object[] args;
            public void Init(Action<T, object[]> action, object[] args)
            {
                this.action = action;
                this.args = args;
            }
            public void Destory()
            {
                this.action = null;
                this.args = null;
                this.ObjectPushPool();
            }
            public void TriggerEvent(T eventData)
            {
                action?.Invoke(eventData, args);
            }
        }

        interface IEventListenerEventInfos
        {
            void RemoveAll();

        }

        /// <summary>
        /// 一类事件的数据包装类型：包含多个EventListenerEventInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class EventListenerEventInfos<T> : IEventListenerEventInfos
        {

            // 所有的事件
            private List<EventListenerEventInfo<T>> eventList = new List<EventListenerEventInfo<T>>();

            /// <summary>
            /// 添加事件
            /// </summary>
            public void AddListener(Action<T, object[]> action, params object[] args)
            {
                EventListenerEventInfo<T> info = PoolManager.Instance.GetObject<EventListenerEventInfo<T>>();
                info.Init(action, args);
                eventList.Add(info);
            }

            /// <summary>
            /// 移除事件
            /// </summary>
            public void RemoveListener(Action<T, object[]> action, bool checkArgs = false, params object[] args)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    // 找到这个事件
                    if (eventList[i].action.Equals(action))
                    {
                        // 是否需要检查参数
                        if (checkArgs && args.Length > 0)
                        {
                            // 参数如果相等
                            if (args.ArraryEquals(eventList[i].args))
                            {
                                // 移除
                                eventList[i].Destory();
                                eventList.RemoveAt(i);
                                return;
                            }
                        }
                        else
                        {
                            // 移除
                            eventList[i].Destory();
                            eventList.RemoveAt(i);
                            return;
                        }
                    }
                }
            }

            /// <summary>
            /// 移除全部，全部放进对象池
            /// </summary>
            public void RemoveAll()
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].Destory();
                }
                eventList.Clear();
                this.ObjectPushPool();
            }

            public void TriggerEvent(T evetData)
            {
                for (int i = 0; i < eventList.Count; i++)
                {
                    eventList[i].TriggerEvent(evetData);
                }
            }

        }

        /// <summary>
        /// 枚举比较器
        /// </summary>
        private class EventTypeEnumComparer : Singleton<EventTypeEnumComparer>, IEqualityComparer<EventType>
        {
            public bool Equals(EventType x, EventType y)
            {
                return x == y;
            }

            public int GetHashCode(EventType obj)
            {
                return (int)obj;
            }
        }
        #endregion

        private Dictionary<EventType, IEventListenerEventInfos> eventInfoDic = new Dictionary<EventType, EventListener.IEventListenerEventInfos>(EventTypeEnumComparer.Instance);

        #region 外部的访问
        /// <summary>
        /// 添加事件
        /// </summary>
        public void AddListener<T>(EventType eventType, Action<T, object[]> action, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as EventListenerEventInfos<T>).AddListener(action, args);
            }
            else
            {
                EventListenerEventInfos<T> infos = PoolManager.Instance.GetObject<EventListenerEventInfos<T>>();
                infos.AddListener(action, args);
                eventInfoDic.Add(eventType, infos);
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        public void RemoveListener<T>(EventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as EventListenerEventInfos<T>).RemoveListener(action, checkArgs, args);
            }
        }

        /// <summary>
        /// 移除某一个事件类型下的全部事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventType"></param>
        public void RemoveAllListener(EventType eventType)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                eventInfoDic[eventType].RemoveAll();
                eventInfoDic.Remove(eventType);
            }
        }
        /// <summary>
        /// 移除全部事件
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (IEventListenerEventInfos infos in eventInfoDic.Values)
            {
                infos.RemoveAll();
            }

            eventInfoDic.Clear();
        }
        #endregion

        /// <summary>
        /// 触发事件
        /// </summary>
        private void TriggerAction<T>(EventType eventType, T eventData)
        {
            if (eventInfoDic.ContainsKey(eventType))
            {
                (eventInfoDic[eventType] as EventListenerEventInfos<T>).TriggerEvent(eventData);
            }
        }

        #region 鼠标事件
        public void OnPointerEnter(PointerEventData eventData)
        {
            TriggerAction(EventType.OnMouseEnter, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TriggerAction(EventType.OnMouseExit, eventData);
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            TriggerAction(EventType.OnBeginDrag, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TriggerAction(EventType.OnDrag, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            TriggerAction(EventType.OnEndDrag, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            TriggerAction(EventType.OnClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            TriggerAction(EventType.OnClickDown, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TriggerAction(EventType.OnClickUp, eventData);
        }
        #endregion

        #region 碰撞事件
        private void OnCollisionEnter(Collision collision)
        {
            TriggerAction(EventType.OnCollisionEnter, collision);
        }
        private void OnCollisionStay(Collision collision)
        {
            TriggerAction(EventType.OnCollisionStay, collision);
        }
        private void OnCollisionExit(Collision collision)
        {
            TriggerAction(EventType.OnCollisionExit, collision);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TriggerAction(EventType.OnCollisionEnter2D, collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TriggerAction(EventType.OnCollisionStay2D, collision);
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            TriggerAction(EventType.OnCollisionExit2D, collision);
        }
        #endregion

        #region 触发事件
        private void OnTriggerEnter(Collider other)
        {
            TriggerAction(EventType.OnTriggerEnter, other);
        }
        private void OnTriggerStay(Collider other)
        {
            TriggerAction(EventType.OnTriggerStay, other);
        }
        private void OnTriggerExit(Collider other)
        {
            TriggerAction(EventType.OnTriggerExit, other);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerAction(EventType.OnTriggerEnter2D, collision);
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            TriggerAction(EventType.OnTriggerStay2D, collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerAction(EventType.OnTriggerExit2D, collision);
        }


        #endregion
    }
}