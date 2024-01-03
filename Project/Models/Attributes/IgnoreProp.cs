namespace BTLWEB.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class IgnoreProp : Attribute
    {
        public IgnoreProp() { }
    }
}
