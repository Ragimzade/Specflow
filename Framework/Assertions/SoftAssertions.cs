﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Framework.Assertions
{
    public class SoftAssertions
    {
        private readonly List<IAssert> 
            _verifications = new List<IAssert>();
        
        public void Equals(string message, string expected, string actual)
        {
            _verifications.Add(new SingleAssert(message, expected, actual));
        }
 
        public void Equals(string message, bool expected, bool actual)
        {
            Equals(message, expected.ToString(), actual.ToString());
        }
 
        public void Equals(string message, int expected, int actual)
        {
            Equals(message, expected.ToString(), actual.ToString());
        }
 
        public void True(string message, bool actual)
        {
            _verifications
                .Add(new SingleAssert(message, true.ToString(), actual.ToString()));
        }

        public void CollectionsAreEqual(ICollection expected, ICollection actual, string message)
        {
            _verifications.Add( new CollectionAssertion(actual,expected,message));
        }
        public void AssertAll()
        {
            var failed = _verifications.Where(v => v.Failed).ToList();
            failed.Should().BeEmpty();
        }
    }
}