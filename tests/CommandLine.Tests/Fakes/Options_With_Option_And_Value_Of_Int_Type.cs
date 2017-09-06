// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See License.md in the project root for license information.

using System;
using System.Linq;

namespace CommandLine.Tests.Fakes
{
  class Options_With_Option_And_Value_Of_Int_Type
  {
    [Option('o', "opt")] public int OptValue { get; set; }

    [Value(0)] public int PosValue { get; set; }
  }
}
