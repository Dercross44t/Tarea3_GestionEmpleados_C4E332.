using System;
using System.Collections.Generic;
using System.Text;
using Tarea3.MODELS;

namespace Tarea3.DA.Repositorios
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AppDbContext _context;

        public EmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }
        private IQueryable<Empleado> AplicarFiltro(string? busqueda) { 
            var query = _context.DbEmpleados.AsQueryable();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var texto = busqueda.Trim().ToLower();

                query = query.Where(empleado => empleado.Nombre.ToLower().Contains(texto)
                || empleado.Apellidos.ToLower().Contains(texto)
                || empleado.Departamento.ToLower().Contains(texto));
            }

            return query.OrderBy(empleado => empleado.Nombre).ThenBy(empleado => empleado.Apellidos)
                .ThenBy(empleado => empleado.Departamento);
        }
        public void Actualizar(Empleado empleado)
        {
            var empleadoExistente = _context.DbEmpleados.FirstOrDefault(empleadoEncontrado
                => empleadoEncontrado.Id == empleado.Id);

            if (empleadoExistente != null)
            {
                empleadoExistente.Nombre = empleado.Nombre;
                empleadoExistente.Apellidos = empleado.Apellidos;
                empleadoExistente.Departamento = empleado.Departamento;
                empleadoExistente.Salario = empleado.Salario;
                empleadoExistente.Activo = empleado.Activo;

                _context.SaveChanges();
            }
        }

        public void Agregar(Empleado empleado)
        {
            _context.DbEmpleados.Add(empleado);
            _context.SaveChanges();
        }

        public IEnumerable<Empleado> BuscarPorNombreODepartamento(string termino)
        {
            return AplicarFiltro(termino).ToList();
        }

        public int ContarTotalEmpleados(string? busqueda)
        {
            return AplicarFiltro(busqueda).Count();
        }

        public void Eliminar(int id)
        {
            var empleadoEncontrado = _context.DbEmpleados.FirstOrDefault(empleado => empleado.Id == id);

            if (empleadoEncontrado != null)
            {
                _context.DbEmpleados.Remove(empleadoEncontrado);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Empleado> ObtenerPaginado(int pagina, int tamano, string? busqueda)
        {
            return AplicarFiltro(busqueda).Skip((pagina - 1) * tamano).Take(tamano).ToList();
        }

        public Empleado? ObtenerPorId(int id)
        {
            return _context.DbEmpleados.FirstOrDefault(empleado => empleado.Id == id);
        }

        public IEnumerable<Empleado> ObtenerTodos()
        {
            return _context.DbEmpleados.OrderBy(empleado => empleado.Nombre)
                .ThenBy(empleado => empleado.Apellidos).ToList();
        }
    }
}
