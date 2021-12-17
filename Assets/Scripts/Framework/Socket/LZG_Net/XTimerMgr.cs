using System;
using System.Collections.Generic;
using UnityEngine;

namespace XUtliPoolLib
{
	public sealed class XTimerMgr : XSingleton<XTimerMgr>
	{
		public delegate void ElapsedEventHandler(object param);

		public delegate void AccurateElapsedEventHandler(object param, float delay);

		public delegate void ElapsedIDEventHandler(object param, int id);

		private sealed class XTimer : IComparable<XTimer>, IHere
		{
			private double _triggerTime;

			private object _param;

			private object _handler;

			private bool _global = false;

			private uint _token = 0u;

			public double TriggerTime
			{
				get
				{
					return _triggerTime;
				}
			}

			public bool IsGlobaled
			{
				get
				{
					return _global;
				}
			}

			public bool IsInPool
			{
				get;
				set;
			}

			public uint Token
			{
				get
				{
					return _token;
				}
			}

			public int Here
			{
				get;
				set;
			}

			public int Id
			{
				get;
				set;
			}

			public XTimer(double trigger, object handler, object parma, uint token, bool global, int id)
			{
				Refine(trigger, handler, parma, token, global, id);
			}

			public void Refine(double trigger, object handler, object parma, uint token, bool global, int id)
			{
				_triggerTime = trigger;
				_handler = handler;
				_param = parma;
				_global = global;
				_token = token;
				Here = -1;
				IsInPool = false;
				Id = id;
			}

			public void Refine(double trigger)
			{
				_triggerTime = trigger;
			}

			public void Fire(float delta)
			{
				if (_handler is AccurateElapsedEventHandler)
				{
					(_handler as AccurateElapsedEventHandler)(_param, delta);
				}
				else if (_handler is ElapsedIDEventHandler)
				{
					(_handler as ElapsedIDEventHandler)(_param, Id);
				}
				else
				{
					(_handler as ElapsedEventHandler)(_param);
				}
			}

			public int CompareTo(XTimer other)
			{
				return (_triggerTime == other._triggerTime) ? ((int)(_token - other.Token)) : ((!(_triggerTime < other._triggerTime)) ? 1 : (-1));
			}

			public double TimeLeft()
			{
				return _triggerTime - XSingleton<XTimerMgr>.singleton.Elapsed;
			}
		}

		private uint _token = 0u;

		private double _elapsed = 0.0;

		private Queue<XTimer> _pool = new Queue<XTimer>();

		private XHeap<XTimer> _timers = new XHeap<XTimer>();

		private Dictionary<uint, XTimer> _dict = new Dictionary<uint, XTimer>(20);

		private float _intervalTime = 0f;

		private float _updateTime = 0.1f;

		private bool _fixedUpdate = false;

		public bool update = true;

		public float updateStartTime = 0f;

		public double Elapsed
		{
			get
			{
				return _elapsed;
			}
		}

		public bool NeedFixedUpdate
		{
			get
			{
				return _fixedUpdate;
			}
		}

		public uint SetTimer(float interval, ElapsedEventHandler handler, object param)
		{
			_token++;
			if (interval <= 0f)
			{
				handler(param);
				_token++;
			}
			else
			{
				double trigger = _elapsed + Math.Round(interval, 3);
				XTimer timer = GetTimer(trigger, handler, param, _token, false);
				_timers.PushHeap(timer);
				_dict.Add(_token, timer);
			}
			return _token;
		}

		public uint SetTimer<TEnum>(float interval, ElapsedIDEventHandler handler, object param, TEnum e) where TEnum : struct
		{
			_token++;
			//int id = XFastEnumIntEqualityComparer<TEnum>.ToInt(e);
			//if (interval <= 0f)
			//{
			//	handler(param, id);
			//	_token++;
			//}
			//else
			//{
			//	double trigger = _elapsed + Math.Round(interval, 3);
			//	XTimer timer = GetTimer(trigger, handler, param, _token, false, id);
			//	_timers.PushHeap(timer);
			//	_dict.Add(_token, timer);
			//}
			return _token;
		}

		public uint SetGlobalTimer(float interval, ElapsedEventHandler handler, object param)
		{
			_token++;
			if (interval <= 0f)
			{
				handler(param);
				_token++;
			}
			else
			{
				double trigger = _elapsed + Math.Round(interval, 3);
				XTimer timer = GetTimer(trigger, handler, param, _token, true);
				_timers.PushHeap(timer);
				_dict.Add(_token, timer);
			}
			return _token;
		}

		public uint SetTimerAccurate(float interval, AccurateElapsedEventHandler handler, object param)
		{
			_token++;
			if (interval <= 0f)
			{
				handler(param, 0f);
				_token++;
			}
			else
			{
				double trigger = _elapsed + Math.Round(interval, 3);
				XTimer timer = GetTimer(trigger, handler, param, _token, false);
				_timers.PushHeap(timer);
				_dict.Add(_token, timer);
			}
			return _token;
		}

		public void AdjustTimer(float interval, uint token, bool closed = false)
		{
			XTimer value = null;
			if (_dict.TryGetValue(token, out value) && !value.IsInPool)
			{
				double trigger = closed ? (_elapsed - (double)(Time.deltaTime * 0.5f) + Math.Round(interval, 3)) : (_elapsed + Math.Round(interval, 3));
				double triggerTime = value.TriggerTime;
				value.Refine(trigger);
				_timers.Adjust(value, triggerTime < value.TriggerTime);
			}
		}

		public void KillTimerAll()
		{
			List<XTimer> list = new List<XTimer>();
			foreach (XTimer value in _dict.Values)
			{
				if (!value.IsGlobaled)
				{
					list.Add(value);
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				KillTimer(list[i]);
			}
			list.Clear();
		}

		private void KillTimer(XTimer timer)
		{
			if (timer != null)
			{
				_timers.PopHeapAt(timer.Here);
				Discard(timer);
			}
		}

		public void KillTimer(uint token)
		{
			if (token != 0)
			{
				XTimer value = null;
				if (_dict.TryGetValue(token, out value))
				{
					KillTimer(value);
				}
			}
		}

		public double TimeLeft(uint token)
		{
			XTimer value = null;
			if (_dict.TryGetValue(token, out value))
			{
				return value.TimeLeft();
			}
			return 0.0;
		}

		public void Update(float fDeltaT)
		{
			_elapsed += fDeltaT;
			_intervalTime += fDeltaT;
			if (_intervalTime > _updateTime)
			{
				_intervalTime = 0f;
				_fixedUpdate = true;
			}
			TriggerTimers();
		}

		public void PostUpdate()
		{
			_fixedUpdate = false;
		}

		private void TriggerTimers()
		{
			while (_timers.HeapSize > 0)
			{
				XTimer xTimer = _timers.Peek();
				float num = (float)(_elapsed - xTimer.TriggerTime);
				if (num >= 0f)
				{
					ExecuteTimer(_timers.PopHeap(), num);
					continue;
				}
				break;
			}
		}

		private void ExecuteTimer(XTimer timer, float delta)
		{
			Discard(timer);
			timer.Fire(delta);
		}

		private void Discard(XTimer timer)
		{
			if (!timer.IsInPool && _dict.Remove(timer.Token))
			{
				timer.IsInPool = true;
				_pool.Enqueue(timer);
			}
		}

		private XTimer GetTimer(double trigger, object handler, object parma, uint token, bool global, int id = -1)
		{
			if (_pool.Count > 0)
			{
				XTimer xTimer = _pool.Dequeue();
				xTimer.Refine(trigger, handler, parma, token, global, id);
				return xTimer;
			}
			return new XTimer(trigger, handler, parma, token, global, id);
		}

		public override bool Init()
		{
			return true;
		}

		public override void Uninit()
		{
		}
	}
}
