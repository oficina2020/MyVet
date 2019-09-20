using System;
using System.Linq;
using System.Threading.Tasks;
using MyVet.Web.Data.Entities;


namespace MyVet.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            await CheckAgendasAsync();
            await CheckOwnersAsync();
            await CheckPetsAsync();
            await CheckPetTypesAsync();
            await CheckServiceTypesAsync();
        }

        private async Task CheckAgendasAsync()
        {
            if (!_dataContext.Agendas.Any())
            {
                var initialDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
                var finalDate = initialDate.AddYears(1);
                while (initialDate < finalDate)
                {
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        var finalDate2 = initialDate.AddHours(10);
                        while (initialDate < finalDate2)
                        {
                            _dataContext.Agendas.Add(new Agenda
                            {
                                Date = initialDate.ToUniversalTime(),
                                IsAvailable = true
                            });

                            initialDate = initialDate.AddMinutes(30);
                        }

                        initialDate = initialDate.AddHours(14);
                    }
                    else
                    {
                        initialDate = initialDate.AddDays(1);
                    }
                }

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckOwnersAsync()
        {
            if (!_dataContext.Owners.Any())
            {
                AddOwner("8989898", "Juan", "Zuluaga", "234 3232", "310 322 3221", "Calle Luna Calle Sol");
                AddOwner("7655544", "Jose", "Cardona", "343 3226", "300 322 3221", "Calle 77 #22 21");
                AddOwner("6565555", "Maria", "López", "450 4332", "350 322 3221", "Carrera 56 #22 21");
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddOwner(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _dataContext.Owners.Add(new Owner
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }

        private async Task CheckServiceTypesAsync()
        {
            if (!_dataContext.ServiceTypes.Any())
            {
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Consulta" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Urgencia" });
                _dataContext.ServiceTypes.Add(new ServiceType { Name = "Vacunación" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPetTypesAsync()
        {
            if (!_dataContext.PetTypes.Any())
            {
                _dataContext.PetTypes.Add(new PetType { Name = "Perro" });
                _dataContext.PetTypes.Add(new PetType { Name = "Gato" });
                _dataContext.PetTypes.Add(new PetType { Name = "Tortuga" });
                _dataContext.PetTypes.Add(new PetType { Name = "León" });
                _dataContext.PetTypes.Add(new PetType { Name = "Leona" });
                _dataContext.PetTypes.Add(new PetType { Name = "Hámsters" });
                _dataContext.PetTypes.Add(new PetType { Name = "Pájaros" });
                _dataContext.PetTypes.Add(new PetType { Name = "Conejo" });
                _dataContext.PetTypes.Add(new PetType { Name = "Hurón" });
                _dataContext.PetTypes.Add(new PetType { Name = "Hámster" });

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPetsAsync()
        {
            var owner   = _dataContext.Owners.FirstOrDefault();
            var petType = _dataContext.PetTypes.FirstOrDefault();

            if (!_dataContext.Pets.Any())
            {
                AddPet("Otto", owner, petType, "Shih tzu");
                AddPet("Killer", owner, petType, "Dobermann");
                await _dataContext.SaveChangesAsync();
            }
        }

        private void AddPet(string name, Owner owner, PetType petType, string race)
        {
            _dataContext.Pets.Add(new Pet
            {
                Born    = DateTime.Now.AddYears(-2),
                Name    = name,
                Owner   = owner,
                PetType = petType,
                Race    = race
            });
        }


    }
}
