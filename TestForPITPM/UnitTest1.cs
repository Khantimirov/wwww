using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Частный_детектив;

namespace ModulTest
{
	[TestClass]
	public class ModulTest
	{

		[TestMethod]
		public void datab()
		{
			bool expected = true;
			var af = new Vhod();
			bool actual = af.db();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Autoriz()
		{
			bool expected = true;
			var af = new Vhod();
			bool actual = af.auth("almira", "123");
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void Registr()
		{
			bool expected = true;
			var af = new Registr();
			bool actual = af.Reg("альмирочка", "89056675432","gibad11@mail.ru", "very", "123");
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void zayavka()
		{
			bool expected = true;
			var af = new Manager(1);
			bool actual = af.redzayavka("But100");
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void uslugiloading()
		{
			bool expected = true;
			var af = new AdminUs(1, 1, 1);
			bool actual = af.uslugiload();
			Assert.AreEqual(expected, actual);
		}
		[TestMethod]
		public void workeradding()
		{
			bool expected = true;
			var af = new AdminWorker(1, 1, 1);
			bool actual = af.addworker("serger", "89056656445", "work@mail.ru", "worker", "123", "сайт");
			Assert.AreEqual(expected, actual);
		}
	}
}
