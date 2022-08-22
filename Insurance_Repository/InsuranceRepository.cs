namespace Insurance_Repository;
public class RepoConsoleApp
{
    protected readonly Queue<ClaimQueue> _claim = new Queue<ClaimQueue>();

    public bool CreateNewClaim(ClaimQueue claim)
    {
        int prevCount = _claim.Count;

        _claim.Enqueue(claim);

        return prevCount < _claim.Count ? true : false;
    }

    public Queue<ClaimQueue> GetAllClaims()
    {
        return _claim;
    }

    public ClaimQueue NextClaim()
    {
        return _claim.Peek();
    }

    public ClaimQueue ProcessNext()
    {
        return _claim.Dequeue();
    }
}