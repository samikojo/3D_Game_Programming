using GameProgramming3D.Utility;
using NUnit.Framework;

namespace GameProgramming3D.Tests
{
	public class FlagsTest
	{
		[Test]
		public void SetValid()
		{
			UnitBooster flags = 0;
			flags = Flags.Set( flags, UnitBooster.DoubleDamage );
			Assert.IsTrue( Flags.Contains( flags, UnitBooster.DoubleDamage ) );
		}

		[Test]
		public void Toggle()
		{
			UnitBooster flags = 0;
			flags = Flags.Toggle( flags, UnitBooster.DoubleSpeed );
			Assert.IsTrue( Flags.Contains( flags, UnitBooster.DoubleSpeed ) );
		}

		[Test]
		public void DoubleToggle()
		{
			UnitBooster flags = 0;
			flags = Flags.Toggle( flags, UnitBooster.TripleArmor );
			flags = Flags.Toggle( flags, UnitBooster.TripleArmor );
			Assert.IsFalse( Flags.Contains( flags, UnitBooster.TripleArmor ) );
		}

		[Test]
		public void SetTestZero()
		{
			UnitBooster flags = 0;
			flags = Flags.Set( flags, UnitBooster.Immortality );
			Assert.AreNotEqual( (int)flags, 0 );
		}
	}
}
