using I4SWTMandatoryExercise2;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ATM.test.unit
{
    public class RendererTest
    {
        public IRenderer Uut { get; set; }
        public IConsoleWriter Cw { get; set; }

        [SetUp]
        public void SetUp()
        {
            Uut = new Renderer();
            Cw = Substitute.For<IConsoleWriter>();
        }

        [Test]
        public void WriteLine_Called_Amount_Of_Times()
        { 
            
        }

        [Test]
        public void WriteLine_Gets_Correct_String()
        {

            Cw.Received().WriteLine(Arg.Is<string>(str => str.Contains("")));
        }
    }
}