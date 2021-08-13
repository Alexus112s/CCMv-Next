using Reinforced.Typings.Attributes;
using System;

namespace CCMvNext.Infrastructure.ReinforcedTypings
{
    /// <summary>
    /// Marks Action to be translated to Angular service.
    /// </summary>
    public class InvokedFromAngularAttribute : TsFunctionAttribute
    {
        public override Type CodeGeneratorType { get { return typeof(AngularInvokeGenerator); } set { } }

        public InvokedFromAngularAttribute(Type resultType)
        {
            StrongType = resultType;
        }

        public InvokedFromAngularAttribute(string resultType)
        {
            Type = resultType;
        }

        public InvokedFromAngularAttribute()
        {
        }
    }

}
