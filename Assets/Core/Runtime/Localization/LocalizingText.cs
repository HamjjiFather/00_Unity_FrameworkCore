using System.Collections.Generic;
using KKSFramework.TableData;

namespace KKSFramework.Localization
{
	public class LocalizingText : TableDataBase
	{
		// 인덱스.
		public string Id;

		// 글로벌 텍스트.
		public string[] GlobalTexts;


		public LocalizingText ()
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
