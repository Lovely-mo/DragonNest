using System;

namespace Ionic.Zlib
{
	// Token: 0x02000038 RID: 56
	internal static class InternalConstants
	{
		// Token: 0x040001B6 RID: 438
		internal static readonly int MAX_BITS = 15;

		// Token: 0x040001B7 RID: 439
		internal static readonly int BL_CODES = 19;

		// Token: 0x040001B8 RID: 440
		internal static readonly int D_CODES = 30;

		// Token: 0x040001B9 RID: 441
		internal static readonly int LITERALS = 256;

		// Token: 0x040001BA RID: 442
		internal static readonly int LENGTH_CODES = 29;

		// Token: 0x040001BB RID: 443
		internal static readonly int L_CODES = InternalConstants.LITERALS + 1 + InternalConstants.LENGTH_CODES;

		// Token: 0x040001BC RID: 444
		internal static readonly int MAX_BL_BITS = 7;

		// Token: 0x040001BD RID: 445
		internal static readonly int REP_3_6 = 16;

		// Token: 0x040001BE RID: 446
		internal static readonly int REPZ_3_10 = 17;

		// Token: 0x040001BF RID: 447
		internal static readonly int REPZ_11_138 = 18;
	}
}
