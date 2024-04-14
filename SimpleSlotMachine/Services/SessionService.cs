using SimpleSlotMachine.Models;

namespace SimpleSlotMachine.Services
{
    public interface ISessionService
    {
        UserSession CreateSession();
        UserSession GetSession(string sessionId);
        void UpdateSession(UserSession session);
        void DeleteSession(string sessionId);
    }
    public class SessionService : ISessionService
    {
        private readonly Dictionary<string, UserSession> _sessions = new Dictionary<string, UserSession>();

        public UserSession CreateSession()
        {
            var sessionId = Guid.NewGuid().ToString();
            var session = new UserSession
            {
                SessionId = sessionId,
                StartingCredits = 10,
                CurrentCredits = 10
            };
            _sessions[sessionId] = session;
            return session;
        }

        public UserSession GetSession(string sessionId)
        {
            if (_sessions.TryGetValue(sessionId, out UserSession session))
            {
                return session;
            }
            return null;
        }

        public void UpdateSession(UserSession session)
        {
            if (_sessions.ContainsKey(session.SessionId))
            {
                _sessions[session.SessionId] = session;
            }
            else
            {
                throw new InvalidOperationException("Session not found");
            }
        }

        public void DeleteSession(string sessionId)
        {
            _sessions.Remove(sessionId);
        }
    }
}
