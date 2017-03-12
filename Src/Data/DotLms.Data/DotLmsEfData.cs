using Bytes2you.Validation;
using DotLms.Data.Contracts;

namespace DotLms.Data
{
    public class DotLmsEfData : IDotLmsEfData
    {
        private readonly IDotLmsEfDbContext dotLmsEfDbContext;

        public DotLmsEfData(IDotLmsEfDbContext dotLmsEfDbContext)
        {
            Guard.WhenArgument(dotLmsEfDbContext, nameof(dotLmsEfDbContext)).IsNull().Throw();

            this.dotLmsEfDbContext = dotLmsEfDbContext;
        }

        public void Commit()
        {
            this.dotLmsEfDbContext.SaveChanges();
        }
    }
}