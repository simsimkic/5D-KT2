using SIMS_Project.Application.Interfaces.Repository;
using SIMS_Project.Repository.MvvmRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Project.Injector
{
    //Code taken from tutor example on DI 16.04.2023.
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            // Add more implementations to App.xaml.cs
            // Complex repositories (use other repositories) should be implemented last
        };

        public static Dictionary<Type, object> Implementations { get => _implementations; }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
