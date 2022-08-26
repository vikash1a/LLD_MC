namespace test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //arrange
        Driver driver = new Driver();
        //act
        var res = driver.play(3,new List<string>(){"2 3","1 2","2 2","2 1","1 1","3 3","3 2","3 1","1 3"});
        //assert
        Assert.Equal(res,true);
    }
}