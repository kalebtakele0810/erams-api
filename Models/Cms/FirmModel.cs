namespace ERAManagementSystem.Models.Cms;

public class FirmModel
{
    public int? FirmId { get; set; }
    public string FirmType { get; set; }
    public string FirmName { get; set; }
    public string CountryOfOrigin { get; set; }
    public int? YearEstablished { get; set; }
    public string ConstructionLicenseNumber { get; set; }
    public decimal? RegisteredCapital { get; set; }
    public string Tin { get; set; }
    public string VatRegistrationNumber { get; set; }
    public string Region { get; set; }
    public string City { get; set; }
    public string SubCity { get; set; }
    public string Wereda { get; set; }
    public string PoBox { get; set; }
    public string Website { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Fax { get; set; }
    public bool? IsJointVenture { get; set; }
}