- Should allows test given pattern
public class TestAboutSomething : Tests<Class>
{

    public void ContextInitialization()
    {
        ServiceInitializationParameters.Add(1);
        ServiceInitializationParameters.Add(new Mock<IDependancy>);
    }
    public void Test1()
    {
    
    }
}