namespace Shared.Requests.Identity;

public record RegisterRequest(
    [Required(ErrorMessage = "Поле не должно быть пустым!")] 
    string LastName,
    [Required(ErrorMessage = "Поле не должно быть пустым!")]
    string FirstName,
    string? MiddleName,
    [Required(ErrorMessage = "Поле не должно быть пустым!")]
    [EmailAddress(ErrorMessage = "Поле не соответствует маске 'mail@example.com'!")]
    string Email,
    [Required(ErrorMessage = "Поле не должно быть пустым!")]
    string Password,
    [Required]
    [Range(1, 1, ErrorMessage = "Вы должны согласиться с обработкой персональных данных для того чтобы продолжить!")] 
    bool AgreeTerms, 
    bool ActivateUser);