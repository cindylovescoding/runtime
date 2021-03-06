// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Microsoft.CSharp.RuntimeBinder.Tests
{
    public class CSharpArgumentInfoTests
    {
        private static readonly IEnumerable<CSharpArgumentInfoFlags> AllPossibleFlags =
            Enumerable.Range(0, ((int[])Enum.GetValues(typeof(CSharpArgumentInfoFlags))).Max() * 2)
                .Select(i => (CSharpArgumentInfoFlags)i);

        private static readonly string[] Names =
        {
            "arg", "ARG", "Arg", "Argument name that isn\u2019t a valid C\u266F name \uD83D\uDC7F\uD83E\uDD22",
            "horrid name with" + (char)0xD800 + "a half surrogate", "new", "break", null
        };

        public static IEnumerable<object[]> FlagsAndNames() =>
            AllPossibleFlags.Select((f, i) => new object[] {f, Names[i % Names.Length]});

        [Theory, MemberData(nameof(FlagsAndNames))]
        public void Create_ResultNotNull(CSharpArgumentInfoFlags flag, string name)
        {
            Assert.NotNull(CSharpArgumentInfo.Create(flag, name));
        }
    }
}
