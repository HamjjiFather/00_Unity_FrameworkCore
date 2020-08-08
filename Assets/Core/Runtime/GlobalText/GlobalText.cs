using System.Collections.Generic;
using KKSFramework.TableData;

namespace KKSFramework.GlobalText
{
	public class GlobalText : TableDataBase
	{
		// 인덱스.
		public string Id;

		// 글로벌 텍스트.
		public string[] GlobalTexts;


		public GlobalText ()
		{
		}


		public override void SetData (List<string> myData)
		{
			var rowIndex = 0;
			Id = myData[rowIndex++];
			GlobalTexts = myData[rowIndex++].Split ('/');
		}
	}
}
