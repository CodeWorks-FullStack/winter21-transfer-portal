namespace transferPortal.Models
{
  public class Player
  {
    public int Id { get; set; }
    // ? allows the int field to be null
    public int? TeamId { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Position { get; set; }
    public string Class { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    // NOTE we want to account for the Team model here so we can populate what team this particular player belongs to
    public Team Team { get; set; }
  }
}