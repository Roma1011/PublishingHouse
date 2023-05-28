namespace PublishingHouse.Data.HelperClases;

public class Response
{
    public List<string> Items { get; set; } = new List<string>();
    public int Pages { get; set; }
    public int CurrentPage { get; set; }
}