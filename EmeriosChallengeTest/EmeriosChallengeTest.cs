using EmeriosChallenge;
using System.Collections.Generic;
using Xunit;

namespace EmeriosChallengeTest
{
    public class EmeriosChallengeTest
    {
        [Fact]
        public void ShouldMathLongestInHorizontal()
        {
            //arrange
            var path = @"../../../Files/horizontal.txt";

            //act
            var response = Program.GetMaxOCurrance(path);

            //assert
            Assert.Equal(new List<char>() { 'E', 'E', 'E', 'E', 'E', 'E' }, response);
        }

        [Fact]
        public void ShouldMatchLongestInVertical()
        {
            //arrange
            var path = @"../../../Files/vertical.txt";

            //act
            var response = Program.GetMaxOCurrance(path);

            //assert
            Assert.Equal(new List<char>() { 'Ñ', 'Ñ', 'Ñ', 'Ñ', 'Ñ' }, response);

        }
        //same example that excercise
        [Fact]
        public void ShouldMathLongestInDiagonal()
        {
            //arrange
            var path = @"../../../Files/diagonal1.txt";

            //act
            var response = Program.GetMaxOCurrance(path);

            //assert
            Assert.Equal(new List<char>() { 'D', 'D', 'D', 'D', 'D' }, response);

        }

        [Fact]
        public void ShouldMathLongestInDiagonal2()
        {
            //arrange
            var path = @"../../../Files/diagonal2.txt";

            //act
            var response = Program.GetMaxOCurrance(path);

            //assert
            Assert.Equal(new List<char>() { 'A', 'A', 'A', 'A', 'A', 'A' }, response);
        }
    }
}
