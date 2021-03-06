using Duende.IdentityServer.EntityFramework.Options;
using FinanceApp.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinanceApp.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Category> categories { get; set; }

        public DbSet<Wallet> wallets { get; set; }

        public DbSet<Transaction> transactions { get; set; }

        public DbSet<Stock> stocks { get; set; }
        
        public DbSet<ValuableAsset> valuableAssets { get; set; }

        public DbSet<Bond> bonds { get; set; }
        
        public DbSet<CashFlowItem> cashFlowItems { get; set; }
        
        public DbSet<Loan> loans { get; set; }

        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }
    }
}