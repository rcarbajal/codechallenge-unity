using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
    public class Events
    {
		public static readonly string RECYCLE_GROUND = "recycle_ground";
    } //end inner class Events

    private class EventListenersData
	{
        public UnityEvent Event;
        public int ListenerCount = 0;
	}

    private Dictionary<string, EventListenersData> eventDictionary;

    private static EventManager eventManager;

    private static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                    Debug.LogError("No active EventManager component available in the scene.");
                else
                    eventManager.Init();
            } //end if

            return eventManager;
        } //end get
    } //end Instance property

    #region Private methods

    private void Init()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<string, EventListenersData>();
    } //end method Init

    #endregion

    #region Public static methods

    public static void AddEventListener(string eventName, UnityAction listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out EventListenersData thisData))
        {
            thisData.Event.AddListener(listener);
            thisData.ListenerCount += 1;
        }
        else
        {
            thisData = new EventListenersData();
            thisData.Event = new UnityEvent();
            thisData.Event.AddListener(listener);
            thisData.ListenerCount += 1;

            Instance.eventDictionary.Add(eventName, thisData);
        } //end else
    } //end method AddEventListener

    public static void RemoveEventListener(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out EventListenersData thisData))
        {
            thisData.Event.RemoveListener(listener);
            thisData.ListenerCount -= 1;
        }
    } //end method RemoveEventListener

	public static void RemoveAllListeners(string eventName)
	{
		if (eventManager == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out EventListenersData thisData))
        {
            thisData.Event.RemoveAllListeners();
            thisData.ListenerCount = 0;
        }
	} //end method RemoveAllListeners

    public static void Broadcast(string eventName)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out EventListenersData thisData))
        {
            if (thisData.ListenerCount > 0)
                thisData.Event.Invoke();
            else
                throw new Exception("No listeners exist for event type: " + eventName);
        } //end if
        else
            throw new Exception("No events exist for event type: " + eventName);
    } //end method Broadcast

    #endregion
} //end class EventManager
