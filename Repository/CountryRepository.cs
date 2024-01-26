
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Repository;

public class CountryRepository : iCountryRepository
{
    private readonly DataContext _context;
    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public bool CreateCountry(Country country)
    {
        _context.Countries.Add(country);
        var num = _context.SaveChanges();
        return num >0 ? true : false ;
    }

    public ICollection<Country> GetCountries()
    {
        return _context.Countries.ToList();
    }

    public Country GetCountry(int id)
    {
        return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
    }

    public Country GetCountryByOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersByCountry(int countryId)
    {
        return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
    }

    public bool ValidCountry(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }
}