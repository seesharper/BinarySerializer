//namespace BinarySerializer.Tests
//{
    //using System;
    //using System.IO;

    //using Microsoft.VisualStudio.TestTools.UnitTesting;

    //public class TestBase
    //{
    //    protected readonly MemoryStream Stream = new MemoryStream();


    //    protected IBinarySerializationWriter GetWriter()
    //    {
    //        Stream.SetLength(0);
    //        return new BinarySerializationWriter_old(this.Stream);
    //    }

    //    protected BinarySerializationReader_old GetReader()
    //    {
    //        Stream.Position = 0;
    //        return new BinarySerializationReader_old(this.Stream);
    //    }

    //    protected void CompareDateTime(DateTime exptectedDateTime, DateTime actualDateTime)
    //    {
    //        Assert.AreEqual(exptectedDateTime.Year, actualDateTime.Year);
    //        Assert.AreEqual(exptectedDateTime.Month, actualDateTime.Month);
    //        Assert.AreEqual(exptectedDateTime.Day, actualDateTime.Day);
    //        Assert.AreEqual(exptectedDateTime.Hour, actualDateTime.Hour);
    //        Assert.AreEqual(exptectedDateTime.Minute, actualDateTime.Minute);
    //        Assert.AreEqual(exptectedDateTime.Second, actualDateTime.Second);
    //        Assert.AreEqual(exptectedDateTime.Millisecond, actualDateTime.Millisecond);
    //    }
//    }
//}