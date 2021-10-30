using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NullCheck
{
	public static bool IsNull(this object obj)
	{
		return obj == null || ReferenceEquals(obj, null) || obj.Equals(null);
	}
	
}
