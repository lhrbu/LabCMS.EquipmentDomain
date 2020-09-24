using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabCMS.FixtureDomain.Shared.Models;
using RaccoonWebDevKit.ExpressionSerializer;

namespace LabCMS.FixtureDomain.Server.Services
{
    public class DynamicQueryService
    {
        private readonly Serializer _serializer = new();

        public async ValueTask<IEnumerable<Fixture>> QueryAsync(IEnumerable<Fixture> fixtures,string expression) 
        {
            Func<IEnumerable<Fixture>, IEnumerable<Fixture>> func = await
                _serializer.DesrializeAsync<Func<IEnumerable<Fixture>, IEnumerable<Fixture>>>(
                    expression, typeof(Fixture),typeof(IEnumerable<>),typeof(Enumerable));
            return func(fixtures);
        }
        
    }
}
