using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Domain.Interfaces;
using FindingATournamentApp.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

// Universidad Tecnológica Metropolitana
// Aplicaciones Web Orientada a Servicios
// Maestro: Joel Ivan Chuc UC
// Actividad: Solucion Tecnica del Proyecto
// Integrantes del equipo: Balam Rosas Christian Jesús, Herrera Caro Abraham Enrique, Sabido Reynoso Jonathan Missael
// 4C
// Parcial 2
// Entrega: 02/11/2021

namespace FindingATournamentApp.Infraestructure.Repositories
{
    public class ClubRepository : IClubRepository
    {

        private readonly FindingATournamentContext _context;

        public ClubRepository(FindingATournamentContext context)
        {
            this._context = context;

        }

    
        public async Task<IQueryable<Clube>> GetAll()
        {
            //Origen|Colección Método Iterador
            var query = await _context.Clubes.AsQueryable<Clube>().AsNoTracking().ToListAsync();
            return query.AsQueryable();
        }

        public async Task<Clube> GetById(int id)
        {            
            var query = await _context.Clubes.Include(x => x.ClubAddress).FirstOrDefaultAsync(x => x.Id == id);
            return query;

            /* var query = await _context.People.AsQueryable().Join(_context.Addresses, 
            p => p.Id,
            a => a.PersonId,
            (p, a) => new Person {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Age = p.Age,
                Gender = p.Gender,
                Job = p.Job,
                Address = a   
            }
            ).FirstOrDefaultAsync(x => x.Id == id); 

            return query;*/
        }

        public bool Exist(Expression<Func<Clube, bool>> expression)
        {
            return _context.Clubes.Any(expression);
        }
        public async Task<IQueryable<Clube>> GetByFilter(Clube clube)
        {
            if(clube == null)
                return new List<Clube>().AsQueryable();

            var query = _context.Clubes.AsQueryable();

            if(!string.IsNullOrEmpty(clube.ClubName))
                query = query.Where(x => x.ClubName.Contains(clube.ClubName));

            if(!string.IsNullOrEmpty(clube.ClubAddress))
                query = query.Where(x => x.ClubAddress == clube.ClubAddress);

            if(!string.IsNullOrEmpty(clube.ClubContactNumber))
                query = query.Where(x => x.ClubContactNumber == clube.ClubContactNumber);

            if(clube.ClubLatitude >= 0)
                query = query.Where(x => x.ClubLatitude == clube.ClubLatitude);

            if(clube.ClubLength >= 0)
                query = query.Where(x => x.ClubLength == clube.ClubLength);

            if(!string.IsNullOrEmpty(clube.ClubSchedule))
                query = query.Where(x => x.ClubSchedule == clube.ClubSchedule);

            var result = await query.ToListAsync();

            return result.AsQueryable().AsNoTracking();
        }
        

        /*
        public IEnumerable<Clube> GetByFiltersClub(Clube clubes)
        {
            var query = _context.Clubes.Select(x => x);
            if(!string.IsNullOrEmpty(clubes.ClubName))
                query = query.Where(x => x.ClubName.Contains(clubes.ClubName));
        
            if (!string.IsNullOrEmpty(clubes.ClubContactNumber))
                query = query.Where(x => x.ClubContactNumber.Contains(clubes.ClubContactNumber));
            return query;
        }
        */


        public async Task<int> Create(Clube club){
            
            var entity = club;
            await _context.AddAsync(entity);
            var rows = await _context.SaveChangesAsync();

            if(rows<= 0){
                throw new Exception("No se pudo realizar el registro");
            }

            return entity.Id;
        }
    }
}