using System;

namespace Ionic.Zlib
{
	// Token: 0x0200003E RID: 62
	public static class ZlibConstants
	{
		// Token: 0x040001F1 RID: 497
		public const int WindowBitsMax = 15;

		// Token: 0x040001F2 RID: 498
		public const int WindowBitsDefault = 15;

		// Token: 0x040001F3 RID: 499
		public const int Z_OK = 0;

		// Token: 0x040001F4 RID: 500
		public const int Z_STREAM_END = 1;

		// Token: 0x040001F5 RID: 501
		public const int Z_NEED_DICT = 2;

		// Token: 0x040001F6 RID: 502
		public const int Z_STREAM_ERROR = -2;

		// Token: 0x040001F7 RID: 503
		public const int Z_DATA_ERROR = -3;

		// Token: 0x040001F8 RID: 504
		public const int Z_BUF_ERROR = -5;

		// Token: 0x040001F9 RID: 505
		public const int WorkingBufferSizeDefault = 16384;

		// Token: 0x040001FA RID: 506
		public const int WorkingBufferSizeMin = 1024;
	}
}
