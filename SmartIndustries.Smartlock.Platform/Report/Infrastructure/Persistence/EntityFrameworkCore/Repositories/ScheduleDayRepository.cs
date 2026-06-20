using SmartIndustries.Smartlock.Platform.Report.Domain.Model.Aggregates;
using SmartIndustries.Smartlock.Platform.Report.Domain.Repositories;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Configuration;
using SmartIndustries.Smartlock.Platform.Shared.Infrastructure.Persistence.EntityFramerWorkCore.Repositories;

namespace SmartIndustries.Smartlock.Platform.Report.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ScheduleDayRepository(AppDbContext context) : BaseRepository<ScheduleDay>(context), IScheduleDayRepository
{
}
