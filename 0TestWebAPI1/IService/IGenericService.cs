using System.Collections.Generic;

namespace _0TestWebAPI1.IService
{
    public interface IGenericService<T>
    {
        void Crear(T item);
        void Actualizar(T item);
        void EliminarById<Z>(Z id);
        List<T> GetAll();
        T GetById<Z>(Z id);
    }
}
