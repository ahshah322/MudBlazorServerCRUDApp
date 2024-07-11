using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Services.GenderService
{
    public interface IGenderService
    {
        List<Gender> Genders { get; set; }
        Task GetGenders();
        Task<Gender> GetSingleGender(int Id);
        Task CreateGender(Gender Gender);
        Task UpdateGender(Gender Gender);
        Task DeleteGender(int Id);
    }
}
