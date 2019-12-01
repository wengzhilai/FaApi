using System;
using System.Threading.Tasks;
using Models;

namespace ApiQuartz.Controllers.Interface
{
    public interface IQuartzTaskController
    {
        ResultObj<Boolean> isStarted();
        Task<Result> start();
        Task<Result> stop();
        Task<Result> removeJob(DtoKey inEnt);
        Task<ResultObj<QuartzTaskModel>> list();
    }
}
