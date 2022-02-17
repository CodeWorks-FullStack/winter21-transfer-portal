namespace transferPortal.Models
{
  public class Profile
  {
    // NOTE profile is public facing - we don't want to give ALL account info everytime
    public string Name { get; set; }
    public string Picture { get; set; }
  }
}