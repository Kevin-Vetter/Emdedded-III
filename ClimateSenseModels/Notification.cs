namespace ClimateSenseModels;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Message { get; set; } = "";

    public DateTime Created { get; set; } = DateTime.Now;
}