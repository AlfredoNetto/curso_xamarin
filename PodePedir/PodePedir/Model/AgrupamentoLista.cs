using System.Collections.ObjectModel;
using System.Linq;

namespace PodePedir.Model
{
    public class AgrupamentoLista<S, T> : ObservableCollection<T>
    {
        private readonly S _key;

        public AgrupamentoLista(IGrouping<S, T> group) : base(group)
        {
            _key = group.Key;
        }

        public S key
        {
            get { return _key; }
        }
    }
}
