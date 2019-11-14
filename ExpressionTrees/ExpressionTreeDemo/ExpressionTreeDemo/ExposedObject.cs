using System;
using System.Dynamic;
using System.Reflection;

namespace ExpressionTreeDemo
{
    public class ExposedObject : DynamicObject
    {
        private readonly Type type;
        private static readonly BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
        private readonly object obj;

        public ExposedObject(object obj)
        {
            this.obj = obj;
            this.type = this.obj.GetType();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = this.type.GetProperty(binder.Name, flags);

            if (property == null)
                return base.TryGetMember(binder, out result);

            result = property.GetValue(obj);

            return true;
        }
    }
}
