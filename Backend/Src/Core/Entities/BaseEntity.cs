namespace Core.Entities;

public class BaseEntity<Tid>
{
    public Tid Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}