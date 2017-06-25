<%@ webhandler Language="C#" class="FileManager" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class FileManager : IHttpHandler
{
	public void ProcessRequest(HttpContext context)
	{  

	}

	public class NameSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo xInfo = new FileInfo(x.ToString());
			FileInfo yInfo = new FileInfo(y.ToString());

			return xInfo.FullName.CompareTo(yInfo.FullName);
		}
	}

	public class SizeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo xInfo = new FileInfo(x.ToString());
			FileInfo yInfo = new FileInfo(y.ToString());

			return xInfo.Length.CompareTo(yInfo.Length);
		}
	}

	public class TypeSorter : IComparer
	{
		public int Compare(object x, object y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			FileInfo xInfo = new FileInfo(x.ToString());
			FileInfo yInfo = new FileInfo(y.ToString());

			return xInfo.Extension.CompareTo(yInfo.Extension);
		}
	}

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
}
