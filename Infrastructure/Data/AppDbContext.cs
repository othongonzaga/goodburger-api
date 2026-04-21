using Microsoft.EntityFrameworkCore;

namespace goodburger_api.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}