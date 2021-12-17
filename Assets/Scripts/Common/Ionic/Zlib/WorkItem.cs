using System;

namespace Ionic.Zlib
{
	// Token: 0x0200002F RID: 47
	internal class WorkItem
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		public WorkItem(int size, CompressionLevel compressLevel, CompressionStrategy strategy, int ix)
		{
			this.buffer = new byte[size];
			int num = size + (size / 32768 + 1) * 5 * 2;
			this.compressed = new byte[num];
			this.compressor = new ZlibCodec();
			this.compressor.InitializeDeflate(compressLevel, false);
			this.compressor.OutputBuffer = this.compressed;
			this.compressor.InputBuffer = this.buffer;
			this.index = ix;
		}

		// Token: 0x04000169 RID: 361
		public byte[] buffer;

		// Token: 0x0400016A RID: 362
		public byte[] compressed;

		// Token: 0x0400016B RID: 363
		public int crc;

		// Token: 0x0400016C RID: 364
		public int index;

		// Token: 0x0400016D RID: 365
		public int ordinal;

		// Token: 0x0400016E RID: 366
		public int inputBytesAvailable;

		// Token: 0x0400016F RID: 367
		public int compressedBytesAvailable;

		// Token: 0x04000170 RID: 368
		public ZlibCodec compressor;
	}
}
