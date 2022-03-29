using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevicesGroup;
using System;
using System.IO;

namespace Zadanie2UnitTests
{

    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }


    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            Assert.AreEqual(IDevice.State.off, mfd.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            Assert.AreEqual(IDevice.State.on, mfd.GetState());
        }


        // weryfikacja, czy po wywołaniu metody `Print` i włączonej kopiarce w napisie pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                mfd.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Print` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                mfd.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `Scan` i wyłączonej kopiarce w napisie pojawia się słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy wywołanie metody `Scan` z parametrem określającym format dokumentu
        // zawiera odpowiednie rozszerzenie (`.jpg`, `.txt`, `.pdf`)
        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                mfd.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                mfd.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }


        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie pojawiają się słowa `Print`
        // oraz `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                mfd.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywołaniu metody `ScanAndPrint` i wyłączonej kopiarce w napisie NIE pojawia się słowo `Print`
        // ani słowo `Scan`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void MultifunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                mfd.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            mfd.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            mfd.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            mfd.ScanAndPrint();
            mfd.ScanAndPrint();

            // 5 wydruków, gdy urządzenie włączone
            Assert.AreEqual(5, mfd.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            IDocument doc1;
            mfd.Scan(out doc1);
            IDocument doc2;
            mfd.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            mfd.ScanAndPrint();
            mfd.ScanAndPrint();

            // 4 skany, gdy urządzenie włączone
            Assert.AreEqual(4, mfd.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();
            mfd.PowerOn();
            mfd.PowerOn();

            IDocument doc1;
            mfd.Scan(out doc1);
            IDocument doc2;
            mfd.Scan(out doc2);

            mfd.PowerOff();
            mfd.PowerOff();
            mfd.PowerOff();
            mfd.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            mfd.ScanAndPrint();
            mfd.ScanAndPrint();

            // 3 włączenia
            Assert.AreEqual(3, mfd.Counter);
        }
    }
    [TestClass]
    public class UnitTestFax
    {
        [TestMethod]
        public void Fax_FaxCounter()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.SendFax("000-000-0000");
            fax.PowerOff();

            Assert.AreEqual(fax.Counter, 1);
        }

        [TestMethod]
        public void Fax_Message()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.SendFax("123-456-7890");
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax to"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void Fax_CounterWhenDeviceIsOff()
        {
            var fax = new MultifunctionalDevice();
            fax.PowerOn();
            fax.PowerOff();

            fax.SendFax("123-456-7890");
            fax.PowerOn();
            fax.PowerOff();

            Assert.IsTrue(fax.Counter == 2 && fax.FaxCounter == 0);
        }
    }
}
