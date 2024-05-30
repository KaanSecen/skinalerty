namespace BLL.Models;

public class ValidationResult<T> where T : class
{
    public string? Message { get; set; }

    public bool IsSuccess { get; set; }
    public IEnumerable<T>? Result { get; set; }

}