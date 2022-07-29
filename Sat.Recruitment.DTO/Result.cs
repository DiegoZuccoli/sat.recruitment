namespace Sat.Recruitment.DTO
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Errors { get; set; }
        public T? items { get; set; }

    }

}