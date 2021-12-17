using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace XUtliPoolLib
{
	// Token: 0x0200019B RID: 411
	public class XCommon : XSingleton<XCommon>
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0002F48C File Offset: 0x0002D68C
		public XCommon()
		{
			this._idx = 5;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002F4FC File Offset: 0x0002D6FC
		public static float XEps
		{
			get
			{
				return XCommon._eps;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0002F514 File Offset: 0x0002D714
		public int New_id
		{
			get
			{
				int num = this._new_id + 1;
				this._new_id = num;
				return num;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002F538 File Offset: 0x0002D738
		public long UniqueToken
		{
			get
			{
				return DateTime.Now.Ticks + (long)this.New_id;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002F560 File Offset: 0x0002D760
		public uint XHash(string str)
		{
			bool flag = str == null;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				uint num = 0U;
				for (int i = 0; i < str.Length; i++)
				{
					num = (num << this._idx) + num + (uint)str[i];
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		public uint XHashLowerRelpaceDot(uint hash, string str)
		{
			bool flag = str == null;
			uint result;
			if (flag)
			{
				result = hash;
			}
			else
			{
				for (int i = 0; i < str.Length; i++)
				{
					char c = char.ToLower(str[i]);
					bool flag2 = c == '/' || c == '\\';
					if (flag2)
					{
						c = '.';
					}
					hash = (hash << this._idx) + hash + (uint)c;
				}
				result = hash;
			}
			return result;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002F61C File Offset: 0x0002D81C
		public uint XHash(uint hash, string str)
		{
			bool flag = str == null;
			uint result;
			if (flag)
			{
				result = hash;
			}
			else
			{
				foreach (char c in str)
				{
					hash = (hash << this._idx) + hash + (uint)c;
				}
				result = hash;
			}
			return result;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002F670 File Offset: 0x0002D870
		public uint XHash(StringBuilder str)
		{
			bool flag = str == null;
			uint result;
			if (flag)
			{
				result = 0U;
			}
			else
			{
				uint num = 0U;
				for (int i = 0; i < str.Length; i++)
				{
					num = (num << this._idx) + num + (uint)str[i];
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		public bool IsEqual(float a, float b)
		{
			return a == b;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002F6D8 File Offset: 0x0002D8D8
		public bool IsLess(float a, float b)
		{
			return a < b;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		public int Range(int value, int min, int max)
		{
			value = Math.Max(value, min);
			return Math.Min(value, max);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002F714 File Offset: 0x0002D914
		public bool IsGreater(float a, float b)
		{
			return a > b;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002F72C File Offset: 0x0002D92C
		public bool IsEqualLess(float a, float b)
		{
			return a <= b;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002F748 File Offset: 0x0002D948
		public bool IsEqualGreater(float a, float b)
		{
			return a >= b;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002F764 File Offset: 0x0002D964
		public uint GetToken()
		{
			return (uint)DateTime.Now.Millisecond;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002F784 File Offset: 0x0002D984
		public void ProcessValueDamp(ref float values, float target, ref float factor, float deltaT)
		{
			bool flag = XSingleton<XCommon>.singleton.IsEqual(values, target);
			if (flag)
			{
				values = target;
				factor = 0f;
			}
			else
			{
				values += (target - values) * Mathf.Min(1f, deltaT * factor);
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		public void ProcessValueEvenPace(ref float value, float target, float speed, float deltaT)
		{
			float num = target - value;
			float num2 = target - (num - speed * deltaT);
			bool flag = XSingleton<XCommon>.singleton.IsGreater((target - num2) * num, 0f);
			if (flag)
			{
				value = num2;
			}
			else
			{
				value = target;
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002F808 File Offset: 0x0002DA08
		public bool IsRectCycleCross(float rectw, float recth, Vector3 c, float r)
		{
			Vector3 vector=new Vector3(Mathf.Max(Mathf.Abs(c.x) - rectw, 0f), 0f, Mathf.Max(Mathf.Abs(c.z) - recth, 0f));
			return vector.sqrMagnitude < r * r;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0002F864 File Offset: 0x0002DA64
		public bool Intersection(Vector2 begin, Vector2 end, Vector2 center, float radius, out float t)
		{
			t = 0f;
			float num = radius * radius;
			Vector2 vector = center - begin;
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude < num;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Vector2 vector2 = end - begin;
				bool flag2 = vector2.sqrMagnitude > 0f;
				if (flag2)
				{
					float num2 = Mathf.Sqrt(sqrMagnitude) * Mathf.Cos(Vector2.Angle(vector, vector2));
					bool flag3 = num2 >= 0f;
					if (flag3)
					{
						float num3 = sqrMagnitude - num2 * num2;
						bool flag4 = num3 < num;
						if (flag4)
						{
							float num4 = num - num3;
							t = num2 - Mathf.Sqrt(num4);
							return true;
						}
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002F920 File Offset: 0x0002DB20
		private float CrossProduct(float x1, float z1, float x2, float z2)
		{
			return x1 * z2 - x2 * z1;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002F93C File Offset: 0x0002DB3C
		public bool IsLineSegmentCross(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2)
		{
			bool flag = Mathf.Min(p1.x, p2.x) <= Mathf.Max(q1.x, q2.x) && Mathf.Min(q1.x, q2.x) <= Mathf.Max(p1.x, p2.x) && Mathf.Min(p1.z, p2.z) <= Mathf.Max(q1.z, q2.z) && Mathf.Min(q1.z, q2.z) <= Mathf.Max(p1.z, p2.z);
			bool result;
			if (flag)
			{
				float num = this.CrossProduct(p1.x - q1.x, p1.z - q1.z, q2.x - q1.x, q2.z - q1.z);
				float num2 = this.CrossProduct(p2.x - q1.x, p2.z - q1.z, q2.x - q1.x, q2.z - q1.z);
				float num3 = this.CrossProduct(q1.x - p1.x, q1.z - p1.z, p2.x - p1.x, p2.z - p1.z);
				float num4 = this.CrossProduct(q2.x - p1.x, q2.z - p1.z, p2.x - p1.x, p2.z - p1.z);
				result = (num * num2 <= 0f && num3 * num4 <= 0f);
			}
			else
			{
				Vector3 vector = Vector3.Project(p1, Vector3.up);
				result = false;
			}
			return result;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002FB14 File Offset: 0x0002DD14
		public Vector3 Horizontal(Vector3 v)
		{
			v.y = 0f;
			return v.normalized;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002FB39 File Offset: 0x0002DD39
		public void Horizontal(ref Vector3 v)
		{
			v.y = 0f;
			v.Normalize();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002FB50 File Offset: 0x0002DD50
		public float AngleNormalize(float basic, float degree)
		{
			Vector3 vector = this.FloatToAngle(basic);
			Vector3 vector2 = this.FloatToAngle(degree);
			float num = Vector3.Angle(vector, vector2);
			return this.Clockwise(vector, vector2) ? (basic + num) : (basic - num);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002FB8C File Offset: 0x0002DD8C
		public Vector2 HorizontalRotateVetor2(Vector2 v, float degree, bool normalized = true)
		{
			degree = -degree;
			float num = degree * 0.0174532924f;
			float num2 = Mathf.Sin(num);
			float num3 = Mathf.Cos(num);
			float x = v.x * num3 - v.y * num2;
			float y = v.x * num2 + v.y * num3;
			v.x = x;
			v.y = y;
			return normalized ? v.normalized : v;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002FC00 File Offset: 0x0002DE00
		public Vector3 HorizontalRotateVetor3(Vector3 v, float degree, bool normalized = true)
		{
			degree = -degree;
			float num = degree * 0.0174532924f;
			float num2 = Mathf.Sin(num);
			float num3 = Mathf.Cos(num);
			float x = v.x * num3 - v.z * num2;
			float z = v.x * num2 + v.z * num3;
			v.x = x;
			v.z = z;
			return normalized ? v.normalized : v;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002FC74 File Offset: 0x0002DE74
		public float TicksToSeconds(long tick)
		{
			long num = tick / 10000L;
			return (float)num / 1000f;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002FC98 File Offset: 0x0002DE98
		public long SecondsToTicks(float time)
		{
			long num = (long)(time * 1000f);
			return num * 10000L;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002FCBC File Offset: 0x0002DEBC
		public float AngleToFloat(Vector3 dir)
		{
			float num = Vector3.Angle(Vector3.forward, dir);
			return XSingleton<XCommon>.singleton.Clockwise(Vector3.forward, dir) ? num : (-num);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002FCF4 File Offset: 0x0002DEF4
		public float AngleWithSign(Vector3 from, Vector3 to)
		{
			float num = Vector3.Angle(from, to);
			return this.Clockwise(from, to) ? num : (-num);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002FD20 File Offset: 0x0002DF20
		public Vector3 FloatToAngle(float angle)
		{
			return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002FD4C File Offset: 0x0002DF4C
		public Quaternion VectorToQuaternion(Vector3 v)
		{
			return XSingleton<XCommon>.singleton.FloatToQuaternion(XSingleton<XCommon>.singleton.AngleWithSign(Vector3.forward, v));
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002FD78 File Offset: 0x0002DF78
		public Quaternion FloatToQuaternion(float angle)
		{
			return Quaternion.Euler(0f, angle, 0f);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002FD9C File Offset: 0x0002DF9C
		public Quaternion RotateToGround(Vector3 pos, Vector3 forward)
		{
			RaycastHit raycastHit;
			bool flag = Physics.Raycast(new Ray(pos + Vector3.up, Vector3.down), out raycastHit, 5f, 513);
			Quaternion result;
			if (flag)
			{
				Vector3 normal = raycastHit.normal;
				Vector3 vector = forward;
				Vector3.OrthoNormalize(ref normal, ref vector);
				result = Quaternion.LookRotation(vector, normal);
			}
			else
			{
				result = Quaternion.identity;
			}
			return result;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002FE00 File Offset: 0x0002E000
		public bool Clockwise(Vector3 fiduciary, Vector3 relativity)
		{
			float num = fiduciary.z * relativity.x - fiduciary.x * relativity.z;
			return num > 0f;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002FE38 File Offset: 0x0002E038
		public bool Clockwise(Vector2 fiduciary, Vector2 relativity)
		{
			float num = fiduciary.y * relativity.x - fiduciary.x * relativity.y;
			return num > 0f;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002FE70 File Offset: 0x0002E070
		public bool IsInRect(Vector3 point, Rect rect, Vector3 center, Quaternion rotation)
		{
			float num = -(rotation.eulerAngles.y % 360f) / 180f * 3.14159274f;
			Quaternion identity = Quaternion.identity;
			identity.w = Mathf.Cos(num / 2f);
			identity.x = 0f;
			identity.y = Mathf.Sin(num / 2f);
			identity.z = 0f;
			point = identity * (point - center);
			return point.x > rect.xMin && point.x < rect.xMax && point.z > rect.yMin && point.z < rect.yMax;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002FF34 File Offset: 0x0002E134
		public float RandomPercentage()
		{
			return (float)this._random.NextDouble();
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002FF54 File Offset: 0x0002E154
		public float RandomPercentage(float min)
		{
			bool flag = this.IsEqualGreater(min, 1f);
			float result;
			if (flag)
			{
				result = 1f;
			}
			else
			{
				float num = (float)this._random.NextDouble();
				bool flag2 = this.IsGreater(num, min);
				if (flag2)
				{
					result = num;
				}
				else
				{
					result = num / min * (1f - min) + min;
				}
			}
			return result;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002FFA8 File Offset: 0x0002E1A8
		public float RandomFloat(float max)
		{
			return this.RandomPercentage() * max;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		public float RandomFloat(float min, float max)
		{
			return min + this.RandomFloat(max - min);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002FFE4 File Offset: 0x0002E1E4
		public int RandomInt(int min, int max)
		{
			return this._random.Next(min, max);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00030004 File Offset: 0x0002E204
		public int RandomInt(int max)
		{
			return this._random.Next(max);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00030024 File Offset: 0x0002E224
		public int RandomInt()
		{
			return this._random.Next();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00030044 File Offset: 0x0002E244
		public bool IsInteger(float n)
		{
			return Mathf.Abs(n - (float)((int)n)) < XCommon._eps || Mathf.Abs(n - (float)((int)n)) > 1f - XCommon._eps;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00030084 File Offset: 0x0002E284
		public float GetFloatingValue(float value, float floating)
		{
			bool flag = this.IsEqualLess(floating, 0f) || this.IsEqualGreater(floating, 1f);
			float result;
			if (flag)
			{
				result = value;
			}
			else
			{
				float num = this.IsLess(this.RandomPercentage(), 0.5f) ? (1f - floating) : (1f + floating);
				result = value * num;
			}
			return result;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000300E4 File Offset: 0x0002E2E4
		public float GetSmoothFactor(float distance, float timespan, float nearenough)
		{
			float result = 0f;
			distance = Mathf.Abs(distance);
			bool flag = distance > XCommon.XEps;
			if (flag)
			{
				float smoothDeltaTime = Time.smoothDeltaTime;
				float num = nearenough / distance;
				float num2 = timespan / smoothDeltaTime;
				bool flag2 = num2 > 1f;
				if (flag2)
				{
					float num3 = Mathf.Pow(num, 1f / num2);
					result = (1f - num3) / smoothDeltaTime;
				}
				else
				{
					result = float.PositiveInfinity;
				}
			}
			return result;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003015C File Offset: 0x0002E35C
		public float GetJumpForce(float airTime, float g)
		{
			return 0f;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00030174 File Offset: 0x0002E374
		public string SecondsToString(int time)
		{
			int num = time / 60;
			int num2 = time % 60;
			return string.Format("{0:D2}:{1}", num, num2);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000301A8 File Offset: 0x0002E3A8
		public double Clamp(double value, double min, double max)
		{
			return Math.Min(Math.Max(min, value), max);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000301C8 File Offset: 0x0002E3C8
		public Transform FindChildRecursively(Transform t, string name)
		{
			bool flag = t.name == name;
			Transform result;
			if (flag)
			{
				result = t;
			}
			else
			{
				for (int i = 0; i < t.childCount; i++)
				{
					Transform transform = this.FindChildRecursively(t.GetChild(i), name);
					bool flag2 = transform != null;
					if (flag2)
					{
						return transform;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00030228 File Offset: 0x0002E428
		public void CleanStringCombine()
		{
			this.shareSB.Length = 0;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00030238 File Offset: 0x0002E438
		public StringBuilder GetSharedStringBuilder()
		{
			return this.shareSB;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00030250 File Offset: 0x0002E450
		public string GetString()
		{
			return this.shareSB.ToString();
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00030270 File Offset: 0x0002E470
		public XCommon AppendString(string s)
		{
			this.shareSB.Append(s);
			return this;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00030290 File Offset: 0x0002E490
		public XCommon AppendString(string s0, string s1)
		{
			this.shareSB.Append(s0);
			this.shareSB.Append(s1);
			return this;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000302C0 File Offset: 0x0002E4C0
		public XCommon AppendString(string s0, string s1, string s2)
		{
			this.shareSB.Append(s0);
			this.shareSB.Append(s1);
			this.shareSB.Append(s2);
			return this;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000302FC File Offset: 0x0002E4FC
		public string StringCombine(string s0, string s1)
		{
			this.shareSB.Length = 0;
			this.shareSB.Append(s0);
			this.shareSB.Append(s1);
			return this.shareSB.ToString();
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00030340 File Offset: 0x0002E540
		public string StringCombine(string s0, string s1, string s2)
		{
			this.shareSB.Length = 0;
			this.shareSB.Append(s0);
			this.shareSB.Append(s1);
			this.shareSB.Append(s2);
			return this.shareSB.ToString();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00030394 File Offset: 0x0002E594
		public string StringCombine(string s0, string s1, string s2, string s3)
		{
			this.shareSB.Length = 0;
			this.shareSB.Append(s0);
			this.shareSB.Append(s1);
			this.shareSB.Append(s2);
			this.shareSB.Append(s3);
			return this.shareSB.ToString();
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000303F4 File Offset: 0x0002E5F4
		public uint CombineAdd(uint value, int heigh, int low)
		{
			int num = (int)((value >> 16) + (uint)heigh);
			int num2 = (int)((value & 65535U) + (uint)low);
			return (uint)(num << 16 | num2);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0003041D File Offset: 0x0002E61D
		public void CombineSetHeigh(ref uint value, uint heigh)
		{
			value = (heigh << 16 | (value & 65535U));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00030430 File Offset: 0x0002E630
		public ushort CombineGetHeigh(uint value)
		{
			return (ushort)(value >> 16);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00030447 File Offset: 0x0002E647
		public void CombineSetLow(ref uint value, uint low)
		{
			value = ((value & 4294901760U) | (low & 65535U));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0003045C File Offset: 0x0002E65C
		public ushort CombineGetLow(uint value)
		{
			return (ushort)(value & 65535U);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00030478 File Offset: 0x0002E678
		public void EnableParticleRenderer(GameObject go, bool enable)
		{
			Animator componentInChildren = go.GetComponentInChildren<Animator>();
			bool flag = componentInChildren != null;
			if (flag)
			{
				componentInChildren.enabled = enable;
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000304A4 File Offset: 0x0002E6A4
		public void EnableParticle(GameObject go, bool enable)
		{
			go.GetComponentsInChildren<ParticleSystem>(XCommon.tmpParticle);
			int i = 0;
			int count = XCommon.tmpParticle.Count;
			while (i < count)
			{
				ParticleSystem particleSystem = XCommon.tmpParticle[i];
				if (enable)
				{
					particleSystem.Play();
				}
				else
				{
					particleSystem.Stop();
				}
				i++;
			}
			XCommon.tmpParticle.Clear();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00030510 File Offset: 0x0002E710
		public static Object Instantiate(Object prefab)
		{
			return Object.Instantiate(prefab, null);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0003052C File Offset: 0x0002E72C
		public static T Instantiate<T>(T original) where T : Object
		{
			return Object.Instantiate<T>(original, null);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00030548 File Offset: 0x0002E748
		public override bool Init()
		{
			return true;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00003284 File Offset: 0x00001484
		public override void Uninit()
		{
		}

		// Token: 0x04000405 RID: 1029
		public readonly float FrameStep = 0.0333333351f;

		// Token: 0x04000406 RID: 1030
		private static readonly float _eps = 0.0001f;

		// Token: 0x04000407 RID: 1031
		private Random _random = new Random(DateTime.Now.Millisecond);

		// Token: 0x04000408 RID: 1032
		private int _idx = 0;

		// Token: 0x04000409 RID: 1033
		private uint[] _seeds = new uint[]
		{
			17U,
			33U,
			65U,
			129U
		};

		// Token: 0x0400040A RID: 1034
		private int _new_id = 0;

		// Token: 0x0400040B RID: 1035
		public StringBuilder shareSB = new StringBuilder();

		// Token: 0x0400040C RID: 1036
		public static List<Renderer> tmpRender = new List<Renderer>();

		// Token: 0x0400040D RID: 1037
		public static List<ParticleSystem> tmpParticle = new List<ParticleSystem>();

		// Token: 0x0400040E RID: 1038
		public static List<SkinnedMeshRenderer> tmpSkinRender = new List<SkinnedMeshRenderer>();

		// Token: 0x0400040F RID: 1039
		public static List<MeshRenderer> tmpMeshRender = new List<MeshRenderer>();
	}
}
