namespace RepositoryTests;

internal class TestService
{
    public void GoTestWork(string s)
    {
        throw new ArgumentException(s, nameof(TestService));
    }
}