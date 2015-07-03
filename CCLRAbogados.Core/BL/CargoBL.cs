using CCLRAbogados.Core.DTO;
using CCLRAbogados.Data;
using CCLRAbogados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCLRAbogados.Core.BL
{
    public class CargoBL : Base
    {
        public IList<CargoDTO> getCargos()
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.Cargo.Select(x => new CargoDTO
                    {
                        IdCargo = x.IdCargo,
                        Nombre = x.Nombre,
                        Plural = x.Plural,
                        Estado = x.Estado
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public IList<CargoDTO> getCargosActivos()
        {
            using (var context = getContext())
            {
                try
                {
                    var result = context.Cargo.Where(x => x.Estado == true).Select(x => new CargoDTO
                    {
                        IdCargo = x.IdCargo,
                        Nombre = x.Nombre,
                        Plural = x.Plural,
                        Estado = x.Estado
                    }).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public CargoDTO getCargo(int id)
        {
            using (var context = getContext())
            {
                var result = context.Cargo.Where(x => x.IdCargo == id).Select(r => new CargoDTO
                {
                    IdCargo = r.IdCargo,
                    Nombre = r.Nombre,
                    Plural = r.Plural,
                    Estado = r.Estado
                }).SingleOrDefault();
                return result;
            }
        }

        public bool add(CargoDTO Cargo)
        {
            using (var context = getContext())
            {
                try
                {
                    Cargo nuevo = new Cargo();
                    nuevo.IdCargo = Cargo.IdCargo;
                    nuevo.Nombre = Cargo.Nombre;
                    nuevo.Plural = Cargo.Plural;
                    nuevo.Estado = true;
                    context.Cargo.Add(nuevo);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool update(CargoDTO Cargo)
        {
            using (var context = getContext())
            {
                try
                {
                    var dataRow = context.Cargo.Where(x => x.IdCargo == Cargo.IdCargo).SingleOrDefault();
                    dataRow.IdCargo = Cargo.IdCargo;
                    dataRow.Nombre = Cargo.Nombre;
                    dataRow.Plural = Cargo.Plural;
                    dataRow.Estado = Cargo.Estado;
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }
    }
}
