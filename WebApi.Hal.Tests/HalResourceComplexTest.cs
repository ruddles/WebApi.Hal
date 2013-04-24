using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using ApprovalTests;
using ApprovalTests.Reporters;
using WebApi.Hal.Tests.Representations;
using Xunit;

namespace WebApi.Hal.Tests
{
    public class HalResourceComplexTest
    {
        readonly OrganisationsRepresentation organisations;

        public HalResourceComplexTest()
        {
            organisations = new OrganisationsRepresentation
            {
                Organisations = new Collection<OrganisationWithPeopleRepresentation>
                {
                    new OrganisationWithPeopleRepresentation(1, "first"),
                    new OrganisationWithPeopleRepresentation(2, "second")
                }
            };
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void children_have_self_link_test()
        {
            // arrange
            var mediaFormatter = new JsonHalMediaTypeFormatter { Indent = true };
            var content = new StringContent(string.Empty);
            var type = organisations.GetType();

            // act
            using (var stream = new MemoryStream())
            {
                mediaFormatter.WriteToStreamAsync(type, organisations, stream, content, null).Wait();
                stream.Seek(0, SeekOrigin.Begin);
                var serialisedResult = new StreamReader(stream).ReadToEnd();

                Console.WriteLine(serialisedResult);
                Approvals.Verify(serialisedResult);
            }
        }
    }
}
