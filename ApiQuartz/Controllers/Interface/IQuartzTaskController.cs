using System;
using System.Threading.Tasks;
using Models;

namespace ApiQuartz.Controllers.Interface
{
    public interface IQuartzTaskController
    {
        Task<ResultObj<bool>> isStarted();
        Task<Result> start();
        Task<Result> stop();
        Task<Result> removeJob(DtoKey inEnt);
        Task<ResultObj<QuartzTaskModel>> list();
    }
}
