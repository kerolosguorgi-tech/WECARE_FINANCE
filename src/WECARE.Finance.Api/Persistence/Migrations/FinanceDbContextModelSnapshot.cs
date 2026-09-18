using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WECARE.Finance.Api.Persistence;

#nullable disable

namespace WECARE.Finance.Api.Persistence.Migrations;

[DbContext(typeof(FinanceDbContext))]
[Migration("202609180001_Foundation001")]
public partial class Foundation001
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder) { }
}
