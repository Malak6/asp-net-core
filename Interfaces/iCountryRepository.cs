using firstapp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace firstapp.interfaces;

public interface iCountryRepository{

    ICollection<Country> GetCountries();

    Country GetCountry(int id);

    bool ValidCountry(int id);

    ICollection<Owner> GetOwnersByCountry(int countryId);

    Country GetCountryByOwner(int ownerId);

    bool CreateCountry(Country country);


}
