using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model.Container
{
    public abstract class TypeResolver
    {

        public TypeResolver()
        {
            BuildComponents();
        }
        public delegate object CreateCode();
        public delegate object CreateCodeWithParam(object param);

        private readonly Dictionary<Type, CreateCode> _typeToCreateCode
                        = new Dictionary<Type, CreateCode>();

        private readonly Dictionary<Type, CreateCodeWithParam> _typeToCreateWithParamCode
                        = new Dictionary<Type, CreateCodeWithParam>();

        protected readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        protected abstract void BuildComponents();


        public T Create<T>()
        {
            // Do a look up in the dictionary. Use the object type to do the lookup "typeof(T)".
            // The lookup will yield the object creation code.
            // Execute the object creation code.
            try
            {
                return (T)_typeToCreateCode[typeof(T)]();
            }catch(Exception ex)
            {
                Logging.Logger.Current.LogException(ex);
                Logging.Logger.Current.LogWarning(typeof(T).Name + " was not found");
                throw ex;
            }

        }
        public T Create<T>(object Param)
        {
            // Do a look up in the dictionary. Use the object type to do the lookup "typeof(T)".
            // The lookup will yield the object creation code.
            // Execute the object creation code.
            try
            {
                return (T)_typeToCreateWithParamCode[typeof(T)](Param);
            }
            catch (Exception ex)
            {
                Logging.Logger.Current.LogException(ex);
                Logging.Logger.Current.LogWarning(typeof(T).Name + " was not found");
                throw ex;
            }

        }
        
        public void AddComponent<T>(CreateCode CreateCode)
        {
            Logging.Logger.Current.LogInformation("Adding type " + typeof(T).Name);
            // Remove previous entry, if it exists
            if (_typeToCreateCode.ContainsKey(typeof(T)))
                _typeToCreateCode.Remove(typeof(T));
            // Add the new entry
            _typeToCreateCode.Add(typeof(T), CreateCode);
        }
        public void AddComponentWithParam<T>(CreateCodeWithParam CreateCode)
        {
            Logging.Logger.Current.LogInformation("Adding type " + typeof(T).Name);
            // Remove previous entry, if it exists
            if (_typeToCreateWithParamCode .ContainsKey(typeof(T)))
                _typeToCreateWithParamCode.Remove(typeof(T));
            // Add the new entry
            _typeToCreateWithParamCode.Add(typeof(T), CreateCode);
        }

    }
}
