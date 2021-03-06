﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mastonet.Tests
{
    public class FollowTests : MastodonClientTests
    {
        [Fact]
        public async Task GetAccountFollowers()
        {
            var client = GetReadClient();
            var accounts = await client.GetAccountFollowers(1);

            Assert.NotNull(accounts);
            Assert.Equal(2, accounts.Count());
        }

        [Fact]
        public async Task GetAccountFollowing()
        {
            var client = GetReadClient();
            var accounts = await client.GetAccountFollowing(1);

            Assert.NotNull(accounts);
            Assert.Equal(3, accounts.Count());
        }

        [Fact]
        public async Task Follow()
        {
            var client = GetFollowClient();
            // Make sure we don't follow
            await client.Unfollow(4);
            await client.Unfollow(2);

            // Follow local
            var relation = await client.Follow(4);
            Assert.NotNull(relation);
            Assert.True(relation.Following);

            //follow remote
            var followedAccount = await client.Follow("glacasa@mamot.fr");
            Assert.NotNull(followedAccount);
            Assert.Equal("glacasa", followedAccount.UserName);
            relation = (await client.GetAccountRelationships(followedAccount.Id)).First();
            Assert.True(relation.Following);

        }

        [Fact]
        public async Task Unfollow()
        {
            var client = GetFollowClient();
            // Make sure we follow
            await client.Follow(4);

            var relation = await client.Unfollow(4);
            Assert.NotNull(relation);
            Assert.False(relation.Following);
        }


        [Fact]
        public async Task GetFollowRequests()
        {
            throw new NotImplementedException();
            var client = GetFollowClient();
            var requests = await client.GetFollowRequests();
            Assert.NotNull(requests);
        }

        [Fact]
        public async Task AuthorizeRequest()
        {
            throw new NotImplementedException();
            var client = GetFollowClient();
        }

        [Fact]
        public async Task RejectRequest()
        {
            throw new NotImplementedException();
            var client = GetFollowClient();
        }
    }
}
