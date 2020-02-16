﻿using GoogleHashCode.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GoogleHashCode
{
	public static class ExtensionHelpers
	{
		public static string[] ReadFromFile(this string fileName)
		{
			var readAllLines = File.ReadAllLines(Path.Combine(EnvironmentConstants.DataPath, EnvironmentConstants.InputPath, fileName));
			return readAllLines;
		}

		public static void WriteToFile(this string fileName, string[] lines)
		{
			File.WriteAllLines(Path.Combine(EnvironmentConstants.DataPath, EnvironmentConstants.OutputPath, fileName), lines);
		}

		public static void WriteToFile(this string fileName, string line)
		{
			WriteToFile(fileName, new List<string> { line }.ToArray());
		}

		public static void ExecuteSolver<TInput, TOutput>(this BaseSolver<TInput, TOutput> solver, string fileName) where TInput : IInput, new() where TOutput : IOutput, new()
		{
			var sw = new Stopwatch();
			sw.Start();

			var content = fileName.ReadFromFile();
			
			solver.Input.Parse(content);
			solver.Solve();

			Console.WriteLine($"Total Score: {solver.Output.GetScore()}");

			fileName.WriteToFile(solver.Output.GetOutputFormat());

			Console.WriteLine($"Execution Time: {sw.Elapsed}");
		}

	}
}