﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Assertions
{
    public static class Soft
    {
        private static readonly List<Exception> ErrorMessages = new List<Exception>();

        public static void Assert(params Action[] assertionsToRun)
        {
            foreach (var action in assertionsToRun)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception exception)
                {
                    ErrorMessages.Add(exception);
                }
            }
        }

        public static void AssertAll()
        {
            if (!ErrorMessages.Any())
            {
                return;
            }

            var errorMessage = string.Join("\n\n", ErrorMessages);
            throw new AssertFailedException($"The following asserts failed:\n\n{errorMessage}\n");
        }
    }
}