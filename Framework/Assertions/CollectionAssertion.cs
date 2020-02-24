﻿using System.Collections;
using System.Linq;

namespace Framework.Assertions
{
    public class CollectionAssertion : IAssert
    {
        private readonly string _message;
        private readonly ICollection _expectedCollection;
        private readonly ICollection  _actualCollection;
        

        public bool Failed { get; }


        public CollectionAssertion(ICollection expectedCollection, ICollection  actualCollection, string message)
        {
            _expectedCollection = expectedCollection;
            _actualCollection = actualCollection;
            _message = message;
              Failed = !EqualsAll(expectedCollection,actualCollection);
            if (Failed)
            {
                const string screenshot = "MethodToSaveScreenshotAndReturnFilename";
                _message += $". Screenshot captured at: {screenshot}";
            }
        }
        
        public override string ToString()
        {
            return $"'{_message}' assert was expected to be " +
                   $"'{_expectedCollection.Count}' but was '{_actualCollection.Count}'";
        }

        private static bool EqualsAll( ICollection a, ICollection b)
        {
            if (a == null || b == null)
                return (a == null && b == null);

            return a.Count == b.Count && a.Cast<object>().SequenceEqual(b.Cast<object>());
        }
    }
}

