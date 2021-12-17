using System;

namespace XUtliPoolLib
{
	public abstract class XSingleton<T> : XBaseSingleton where T : new()
	{
		private static readonly T _instance = new T();

		public static T singleton
		{
			get
			{
				return _instance;
			}
		}

		protected XSingleton()
		{
			if (_instance != null)
			{
				throw new Exception(_instance.ToString() + " can not be created again.");
			}
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
