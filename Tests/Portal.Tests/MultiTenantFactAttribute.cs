using Xunit;

namespace Portal.Tests
{
    public sealed class MultiTenantFactAttribute : FactAttribute
    {
        public MultiTenantFactAttribute()
        {
            Skip = "MultiTenancy is disabled.";
        }
    }
}
