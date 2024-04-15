using Microsoft.AspNetCore.Components;
using SimpleSlotMachine.Controllers;

namespace SimpleSlotMachine.Services
{
    public interface ISlotMachineService
    {
        RollResult StartRoll(string sessionId);
    }

    public class RollResult
    {
        public string[] Blocks { get; set; }
        public int Reward { get; set; }
    }

    public class SlotMachineService : ISlotMachineService
    {
        private readonly Random _random = new Random();
        private readonly ISessionService _sessionService;

        public SlotMachineService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public RollResult StartRoll(string sessionId)
        {
            var session = _sessionService.GetSession(sessionId);
            if (session == null)
            {
                throw new InvalidOperationException("Session not found");
            }

            int cheatFactor = CalculateCheatFactor(session.CurrentCredits);

            var blocks = new string[3];
            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i] = GetRandomBlock();
            }

            bool isWin = IsWin(blocks);

            if (isWin && ShouldReRoll(cheatFactor))
            {
                for (int i = 0; i <blocks.Length; i++)
                {
                    blocks[i] = GetRandomBlock();
                }
            }

            int reward = CalculateReward(blocks);

            if (isWin)
            {
                session.CurrentCredits += reward;
            }
            else
            {
                session.CurrentCredits--;
            }
            _sessionService.UpdateSession(session);

            return new RollResult
            {
                Blocks = blocks,
                Reward = isWin ? reward : 0
            };
        }

        private int CalculateCheatFactor(int currentCredits)
        {
            if (currentCredits >= 60)
            {
                return 60;
            }
            else if (currentCredits >= 40)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }

        private bool ShouldReRoll(int cheatFactor)
        {
            return _random.Next(100) < cheatFactor;
        }

        private string GetRandomBlock()
        {
            string[] blocks = { "C", "L", "O", "W" };
            return blocks[_random.Next(blocks.Length)];
        }

        private bool IsWin(string[] blocks)
        {
            return blocks[0] == blocks[1] && blocks[1] == blocks[2];
        }

        private int CalculateReward(string[] blocks)
        {
            switch (blocks[0])
            {
                case "C":
                    return 10;
                case "L": 
                    return 20;
                case "O": 
                    return 30;
                case "W":
                    return 40;
                default:
                    return 0;
            }
        }
    }
}
