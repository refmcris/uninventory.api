using Microsoft.EntityFrameworkCore;
using Uninventory.Interfaces;
using Uninventory.Models.Equipment;
using Uninventory.Models.Users;
using Uninventory.Persistence;
using Uninventory.Persistence.Models;

namespace Uninventory.Services
{
  public class EquipmentService :IEquipmentService
  {
    private readonly UninventoryDBContext _context;

    public EquipmentService(UninventoryDBContext context)
    {
      _context = context;
    }

    private EquipmentDTO ToEquipmentDTO(Equipment eq)
    {
      return new EquipmentDTO
      {
        EquipmentId = eq.EquipmentId,
        Name = eq.Name,
        CategoryId = eq.CategoryId,
        SerialNumber = eq.SerialNumber,
        Status = eq.Status,
        Location = eq.Location,
        PurchaseDate = eq.PurchaseDate,
        WarrantyDate = eq.WarrantyDate,
        CreatedAt = eq.CreatedAt
      };
    }

    public async Task<EquipmentDTO> AddEquipment(EquipmentDTO add)
    {
      var equipment = new Equipment
      {
        Name = add.Name,
        CategoryId = add.CategoryId,
        SerialNumber = add.SerialNumber,
        Status = add.Status,
        Location = add.Location,
        PurchaseDate = add.PurchaseDate,
        WarrantyDate = add.WarrantyDate,



      };
      await _context.Equipment.AddAsync(equipment);

      await _context.SaveChangesAsync();

      return ToEquipmentDTO(equipment);
    }

    public async Task<IEnumerable<EquipmentDTO>> GetEquipments(int? EquipmentId)
    {
      var query = _context.Equipment.AsQueryable();

      if (EquipmentId.HasValue)
      {
        query = query.Where(e => e.EquipmentId == EquipmentId.Value);
      }
      var equipments = await query.ToListAsync();

      return equipments.Select(ToEquipmentDTO).ToList();
    }

    public async Task<EquipmentDTO> GetEquipment(int equipmentId)
    {
      var equipments = await GetEquipments(equipmentId);
      if (!equipments.Any())
      {
        throw new Exception($"El equipo {equipmentId} no está registrado.");
      }
      return equipments.First();
    }

  }
}
