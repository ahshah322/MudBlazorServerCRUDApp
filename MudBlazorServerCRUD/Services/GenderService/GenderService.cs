using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazorServerCRUD.Data;
using MudBlazorServerCRUD.Model;

namespace MudBlazorServerCRUD.Services.GenderService
{
    public class GenderService : IGenderService
    {
        private readonly ApplicationDbContext _context;
        private readonly NavigationManager _navigationManager;

        public GenderService(ApplicationDbContext context, NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
            _context.Database.EnsureCreated();
        }

        public List<Gender> Genders { get; set; } = new List<Gender>();

        public async Task GetGenders()
        {
            Genders = await _context.Genders.ToListAsync();
        }

        public async Task CreateGender(Gender Gender)
        {
            _context.Genders.Add(Gender);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGender(int id)
        {
            var dbGender = await _context.Genders.FindAsync(id);
            if (dbGender == null)
                throw new Exception("No Gender here. :/");

            _context.Genders.Remove(dbGender);
            await _context.SaveChangesAsync();
        }

        public async Task<Gender> GetSingleGender(int id)
        {
            var Gender = await _context.Genders
                                .FindAsync(id);

            if (Gender == null)
                throw new Exception("No Gender here. :/");
            return Gender;
        }


        public async Task UpdateGender(Gender Gender)
        {
            var dbGender = await _context.Genders
                                  .FirstOrDefaultAsync(s => s.Id == Gender.Id);
            if (dbGender == null)
                throw new Exception("No Gender here. :/");

            dbGender.Name = Gender.Name;
            dbGender.NameA = Gender.NameA;
            dbGender.NameB = Gender.NameB;

            await _context.SaveChangesAsync();
        }
    }
}