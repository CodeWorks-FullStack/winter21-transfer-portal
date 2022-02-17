namespace transferPortal.Models
{
  public class Team
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Conference { get; set; }
    public string Division { get; set; }
    public string Picture { get; set; }
    public Profile Owner { get; set; }
  }
}