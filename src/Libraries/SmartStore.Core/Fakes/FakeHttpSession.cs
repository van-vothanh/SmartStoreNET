using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartStore.Core.Fakes
{
    public class FakeHttpSession : ISession
    {
        private readonly Dictionary<string, byte[]> _sessionData = new Dictionary<string, byte[]>();

        public string Id { get; } = Guid.NewGuid().ToString();

        public bool IsAvailable => true;

        public IEnumerable<string> Keys => _sessionData.Keys;

        public void Clear()
        {
            _sessionData.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Remove(string key)
        {
            _sessionData.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _sessionData[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            return _sessionData.TryGetValue(key, out value);
        }
    }
}
