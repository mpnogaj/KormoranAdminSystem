using System;
using System.Collections.Generic;
using System.Linq;

namespace KormoranAdminSystemRevamped.Services
{
	public interface ISessionManager
	{
		/// <summary>
		/// Gets username connected with given session id
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>Username when session is active or null when it isn't</returns>
		public string? GetUsernameFromSession(string sessionId);

		/// <summary>
		/// Creates new, or gets existing session from username
		/// </summary>
		/// <param name="username">Username</param>
		/// <returns>Session Guid</returns>
		public string CreateSession(string username);

		/// <summary>
		/// Extends session lifetime for 1h
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>false if session has already expired, true if operation was successful</returns>
		public bool ExtendSession(string sessionId);

		/// <summary>
		/// Makes session expired
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>false if session has already expired, true if operation was successful</returns>
		public bool ExpireSession(string sessionId);

		/// <summary>
		/// Gets session object from given session id
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>Session object</returns>
		public Session? GetSession(string sessionId);
	}

	public class SessionManager : ISessionManager
	{
		/// <summary>
		/// Stores valid session ids
		/// </summary>
		private readonly HashSet<Session> _sessions = new();

		/// <summary>
		/// Looks for expired ids and removes them from hash set
		/// </summary>
		private void CheckForExpired()
		{
			var date = DateTime.Now;
			_sessions.RemoveWhere(session => !session.IsValid(date));
		}
		
		/// <summary>
		/// Gets username connected with given session id
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>Username when session is active or null when it isn't</returns>
		public string? GetUsernameFromSession(string sessionId)
		{
			CheckForExpired();
			var session = _sessions.FirstOrDefault(x => x.GuidString == sessionId);
			return session?.Username;
		}

		/// <summary>
		/// Extends session lifetime for 1h
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>false if session has already expired, true if operation was successful</returns>
		public bool ExtendSession(string sessionId)
		{
			CheckForExpired();
			var session = _sessions.FirstOrDefault(x => x.GuidString == sessionId);
			if (session == null) return false;
			session.ExtendSession();
			return true;
		}

		/// <summary>
		/// Makes session expired
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>false if session has already expired, true if operation was successful</returns>
		public bool ExpireSession(string sessionId)
		{
			CheckForExpired();
			var session = _sessions.FirstOrDefault(x => x.GuidString == sessionId);
			return session != null && _sessions.Remove(session);
		}

		/// <summary>
		/// Creates new, or gets existing session from username
		/// </summary>
		/// <param name="username">Username</param>
		/// <returns>Session Guid</returns>
		public string CreateSession(string username)
		{
			CheckForExpired();
			var session = _sessions.FirstOrDefault(x => x.Username == username);
			if (session != null) return session.GuidString;
			session = new Session(username);
			_sessions.Add(session);
			return session.GuidString;
		}

		/// <summary>
		/// Gets session object from given session id
		/// </summary>
		/// <param name="sessionId">Session id</param>
		/// <returns>Session object</returns>
		public Session? GetSession(string sessionId)
		{
			CheckForExpired();
			return _sessions.FirstOrDefault(x => x.GuidString == sessionId);
		}
	}

	public class Session
	{
		private readonly TimeSpan _validTime = TimeSpan.FromHours(1);
		
		public DateTime ExpirationDate { get; private set; }
		public string Username { get; }
		public Guid Guid { get; }
		public string GuidString { get; }

		public Session(string username)
		{
			ExpirationDate = DateTime.Now + _validTime;
			Username = username;
			Guid = Guid.NewGuid();
			GuidString = Guid.ToString();
		}

		public bool IsValid(DateTime currentTime)
		{
			return currentTime <= ExpirationDate;
		}

		public void ExtendSession()
		{
			ExpirationDate = DateTime.Now + _validTime;
		}

		public override bool Equals(object? obj)
		{
			try
			{
				return obj is Session rhs && rhs.Username.Equals(this.Username);
			}
			catch
			{
				return false;
			}
		}

		protected bool Equals(Session other)
		{
			return Username == other.Username;
		}

		public override int GetHashCode()
		{
			return Username.GetHashCode();
		}
	}
}
