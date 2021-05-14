using System.Threading.Tasks;
using TatsRoude.Client.AI.Data;

namespace TatsRoude.Client.AI
{
    public interface IRobot
    {
        Task<PredictionData> IsThisToxic(string message);
    }
}
