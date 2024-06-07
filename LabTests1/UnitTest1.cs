using NUnit.Framework;
using System.Runtime.InteropServices;
using ClassLibrary;
using System;

namespace LabTests1
{
    public class ClassTests
    {
        [Test]
        public void IsNumberAndLetterInDiapason()
        {
            Class cl = new Class();
            cl.Number = 100;
            cl.Letter = '*';
            Assert.AreEqual(cl.Number, 1);
            Assert.AreEqual(cl.Letter, 'À');
        }
    }
}