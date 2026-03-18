namespace DataAccess.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }
        string? Status { get; set; }
    }
}
