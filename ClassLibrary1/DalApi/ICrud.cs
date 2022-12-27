using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T Entity);
    void Delete(T Entity);
    void Update(int ID, T Entity);
    T GetByID(int ID);
    public IEnumerable<T?> GetTheList(Func<T?, bool>? func = null);
}
