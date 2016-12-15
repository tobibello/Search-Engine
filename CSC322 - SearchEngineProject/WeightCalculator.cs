using System;

namespace CSC322_SearchEngineProject
{
	internal static class WeightCalculator
	{
		internal static double[] Normalize(params double[] input)
		{
			double[] result = new double[input.Length];
			input.CopyTo(result, 0);
			double acc = 0;
			foreach (var d in result)
			{
				acc += d * d;
			}
			double l2 = Math.Sqrt(acc);
			for (int i = 0; i < result.Length; i++)
			{
				result[i] = result[i] / l2;
			}
			return result;
		}
		internal static double IdfWeight(decimal n, decimal df)
		{
			return Math.Log10((double)(n / df));
		}

		internal static double TfWeight(decimal tf)
		{
			return 1 + Math.Log10((double)tf);
		}

		internal static double TfxIdfWeight(decimal tf, decimal n, decimal df)
		{
			return TfWeight(tf) * IdfWeight(n, df);
		}
	}
}
