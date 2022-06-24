
using intinc_vsix.Commands;

namespace intinc_vsix.Test
{
    public class UtilityTests
    {
        [Fact]
        public void GetDigitsTest()
        {
            var span = Utility.GetDigits("asfælaskjfd", 4);
            Assert.Null(span);

            var str = "asdfasdf 3243243 asdfasdf";
            var sp2 = Utility.GetDigits(str, 12);
            Assert.NotNull(sp2);
            var substring = str.Substring(sp2.start, sp2.length);            
            Assert.Equal("3243243", substring);
        }
    }
}