using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public class TypedEvent : UnityEvent<object> { }

    public static class Events
    {
		public static readonly string RECYCLE_GROUND = "recycle_ground";
        public static readonly string GROUND_PLACED = "ground_placed";
        public static readonly string RECYCLE_AIR_CELL = "recycle_air_cell";
        public static readonly string PLAYER_BODY_HIT = "body_hit";
        public static readonly string PLAYER_FEET_HIT = "feet_hit";
    } //end inner class Events

    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, TypedEvent> typedDictionary;

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
            eventDictionary = new Dictionary<string, UnityEvent>();

        if (typedDictionary == null)
            typedDictionary = new Dictionary<string, TypedEvent>();

    } //end method Init

    #endregion

    #region Public static methods

    public static void AddEventListener(string eventName, UnityAction listener)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        } //end else
    }

    public static void AddEventListener(string eventName, UnityAction<object> listener)
    {
        if (Instance.typedDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            thisEvent.AddListener(listener);
        else
        {
            thisEvent = new TypedEvent();
            thisEvent.AddListener(listener);
            Instance.typedDictionary.Add(eventName, thisEvent);
        } //end else
    } //end method AddEventListener

    public static void RemoveEventListener(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.RemoveListener(listener);
    }

    public static void RemoveEventListener(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;

        if (Instance.typedDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            thisEvent.RemoveListener(listener);
    } //end method RemoveEventListener

	public static void RemoveAllListeners(string eventName)
	{
		if (eventManager == null) return;

        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.RemoveAllListeners();

		if (Instance.typedDictionary.TryGetValue(eventName, out TypedEvent typedEvent))
            typedEvent.RemoveAllListeners();
	} //end method RemoveAllListeners

    public static void Broadcast(string eventName)
    {
        if (Instance.eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            thisEvent.Invoke();
    }

    public static void Broadcast(string eventName, object data)
    {
        if (Instance.typedDictionary.TryGetValue(eventName, out TypedEvent thisEvent))
            thisEvent.Invoke(data);
    } //end method Broadcast

    #endregion
} //end class EventManager
