using System;
using System.Collections.Generic;
using System.Text;
using Tarea3.MODELS;

namespace Tarea3.DA.Repositorios
{
    public interface IEmpleadoRepository
    {
        IEnumerable<Empleado> ObtenerTodos();
        Empleado? ObtenerPorId();
        IEnumerable<Empleado> BuscarPorNombreODepartamento(string termino);
        IEnumerable<Empleado> ObtenerPaginado(int pagina, int tamano, string? busqueda);
        int ContarTotalEmpleados(string? busqueda);
        void Agregar(Empleado empleado);
        void Actualizar(Empleado empleado);
        void Eliminar(int id);
    }
}
