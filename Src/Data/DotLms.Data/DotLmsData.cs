using Bytes2you.Validation;
using DotLms.Data.Contracts;

namespace DotLms.Data
{
    public class DotLmsData : IDotLmsData
    {
        private IDotLmsDbContext dotLmsDbContext;

        public DotLmsData(IDotLmsDbContext dotLmsDbContext)
        {
            Guard.WhenArgument(dotLmsDbContext, nameof(dotLmsDbContext)).IsNull().Throw();

            this.dotLmsDbContext = dotLmsDbContext;
        }
        public void Dispose()
        {
        }

        public void Commit()
        {
            this.dotLmsDbContext.SaveChanges();
        }
    }
}