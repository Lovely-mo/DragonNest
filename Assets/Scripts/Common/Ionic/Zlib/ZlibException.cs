using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000036 RID: 54
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000EC21 File Offset: 0x0000CE21
		public ZlibException()
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000EC2B File Offset: 0x0000CE2B
		public ZlibException(string s) : base(s)
		{
		}
	}
}
