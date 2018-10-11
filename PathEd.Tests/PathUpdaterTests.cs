using FluentAssertions;
using NUnit.Framework;
using System;

namespace PathEd.Tests
{
	public class PathUpdaterTests
	{
		protected TestPath _path;
		protected PathUpdater _updater;

		[SetUp]
		public void Setup()
		{
			_path = new TestPath();
			_updater = new PathUpdater(_path);
		}

		public class AddMethod : PathUpdaterTests
		{
			
			[Test]
			public void Throws_If_Null_Is_Passed()
			{
				Action a = () => _updater.Add(null);
				a.Should().Throw<ArgumentNullException>();
			}

			[Test]
			public void Returns_Value_If_Path_Is_Null()
			{
				_path.Set(null);
				_updater.Add("xyz");
				_path.Get().Should().Be("xyz");
			}

			[Test]
			public void Returns_Value_If_Path_Is_Empty()
			{
				_path.Set("");
				_updater.Add("xyz");
				_path.Get().Should().Be("xyz");
			}

			[Test]
			public void Preserves_Prior_Values()
			{
				_path.Set("abc;def;ghi");
				_updater.Add("xyz");
				_path.Get().Should().Be("abc;def;ghi;xyz");
			}

			[Test]
			public void Does_Not_Add_Duplicates()
			{
				_path.Set("abc;xyz;ghi");
				_updater.Add("xyz");
				_path.Get().Should().Be("abc;xyz;ghi");
			}

			[Test]
			public void Does_Not_Add_Duplicates_Ignore_Case()
			{
				_path.Set("abc;XYz;ghi");
				_updater.Add("xYZ");
				_path.Get().Should().Be("abc;XYz;ghi");
			}
		}

		public class RemoveMethod : PathUpdaterTests
		{
			[Test]
			public void Throws_If_Null_Is_Passed()
			{
				Action a = () => _updater.Remove(null);
				a.Should().Throw<ArgumentNullException>();
			}

			[Test]
			public void Does_Nothing_If_Path_Is_Null()
			{
				_path.Set(null);
				_updater.Remove("xyz");
				_path.Get().Should().Be(null);
			}

			[Test]
			public void Does_Nothing_If_Path_Is_Empty()
			{
				_path.Set("");
				_updater.Remove("xyz");
				_path.Get().Should().Be("");
			}

			[Test]
			public void Removes_Value_From_The_End_With_Semicolon()
			{
				_path.Set("abc;def;ghi;xyz;");
				_updater.Remove("xyz");
				_path.Get().Should().Be("abc;def;ghi");
			}

			[Test]
			public void Removes_Value_From_The_End_Without_Semicolon()
			{
				_path.Set("abc;def;ghi;xyz");
				_updater.Remove("xyz");
				_path.Get().Should().Be("abc;def;ghi");
			}

			[Test]
			public void Removes_Value_From_The_Middle_With_One_Semicolon()
			{
				_path.Set("abc;xyz;ghi");
				_updater.Remove("xyz");
				_path.Get().Should().Be("abc;ghi");
			}

			[Test]
			public void Removes_Value_From_The_Middle_With_One_Semicolon_Ignore_Case()
			{
				_path.Set("abc;XYz;ghi");
				_updater.Remove("xYZ");
				_path.Get().Should().Be("abc;ghi");
			}

			[Test]
			public void Removes_Value_From_The_Beginning_With_One_Semicolon()
			{
				_path.Set("xyz;abc;ghi");
				_updater.Remove("xyz");
				_path.Get().Should().Be("abc;ghi");
			}

			/// <summary>
			/// This is especially important to remove directories with spaces in them
			/// like "C:\Program Files\RepoZ" which are often defined with quotes
			/// </summary>
			[Test]
			public void Removes_Value_With_Quotes_As_Well()
			{
				_path.Set("abc;def;\"x y z\";ghi");
				_updater.Remove("x y z");
				_path.Get().Should().Be("abc;def;ghi");
			}
		}
	}
}
