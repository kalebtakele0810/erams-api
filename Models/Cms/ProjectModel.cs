namespace ERAManagementSystem.Models;

public class ProjectModel
{
    public string Rsdp { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public double Duration { get; set; }
    public string DueDate { get; set; }
    
    public double RoadLength { get; set; }
    
    public DateTime? CreatedOn { get; set; }
}