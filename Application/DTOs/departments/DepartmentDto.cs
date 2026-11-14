namespace Application.DTOs.departments;

public class DepartmentDto
{
    public int Id{get;set;}
    public string Name{get;set;} = string.Empty;
    public string Description{get;set;} = string.Empty;
    public DateTime CreatedAt{get;set;}
}