﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mastonet.Tests
{
    public class AccountTests : MastodonClientTests
    {
        [Fact]
        public async Task GetAccount()
        {
            var client = GetReadClient();

            var account = await client.GetAccount(1);

            Assert.NotNull(account.ProfileUrl);
            Assert.NotNull(account.UserName);
            Assert.Equal("glacasa",account.UserName);
        }

        [Fact]
        public async Task GetCurrentUser()
        {
            var client = GetReadClient();

            var account = await client.GetCurrentUser();

            Assert.NotNull(account.ProfileUrl);
            Assert.NotNull(account.UserName);
            Assert.Equal("TestAccount", account.UserName);
        }

        [Fact]
        public async Task GetAccountRelationships()
        {
            var client = GetReadClient();

            var relationships = await client.GetAccountRelationships(1);

            Assert.NotNull(relationships);
            Assert.Equal(1, relationships.Count());

            var rel = relationships.First();
            Assert.True(rel.Following);
            Assert.True(rel.FollowedBy);
            Assert.False(rel.Blocking);
            Assert.False(rel.Muting);
            Assert.False(rel.Requested);
        }
    }
}
