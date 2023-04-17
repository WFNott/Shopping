using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shopping.Data;
using Shopping.Data.Entities;

namespace Shopping.Helpers
{
	public class CombosHelper : ICombosHelper
	{
		private readonly DataContex _context;

		public CombosHelper(DataContex context)
        {
			_context = context;
		}

		public async Task<IEnumerable<SelectListItem>> GetComboCategoriesAsync()
		{
			List<SelectListItem> list = await _context.categories.Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			}).OrderBy(c => c.Text)
				.ToListAsync();
			list.Insert(0, new SelectListItem { Text = "Seleccione una Categoria...", Value ="0"});
			return list;
		}

		public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
		{
			List<SelectListItem> list = await _context.cities.Where(s => s.State.Id == stateId).Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			}).OrderBy(c => c.Text)
				.ToListAsync();
			list.Insert(0, new SelectListItem { Text = "Seleccione una ciudad...", Value = "0" });
			return list;
		}

		public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
		{
			List<SelectListItem> list = await _context.countries.Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			}).OrderBy(c => c.Text)
					.ToListAsync();
			list.Insert(0, new SelectListItem { Text = "Seleccione un Pais...", Value = "0" });
			return list;
		}

		public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
		{
			List<SelectListItem> list = await _context.states.Where(s => s.Country.Id == countryId).Select(c => new SelectListItem
			{
				Text = c.Name,
				Value = c.Id.ToString()
			}).OrderBy(c => c.Text)
				.ToListAsync();
			list.Insert(0, new SelectListItem { Text = "Seleccione un Departamento/Estado...", Value = "0" });
			return list;
		}
	}
}
