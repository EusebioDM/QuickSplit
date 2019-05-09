﻿using QuickSplit.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace QuickSplit.Test.Domain
{
    public class GroupTest
    {
        [Fact]
        public void EmptyOrNullNameTest()
        {
            Group group = new Group();
            Assert.Throws<DomainException>(() =>
                group.Name = null
            );
            Assert.Throws<DomainException>(() =>
                group.Name = ""
            );
        }

        //[Fact]
        //public void EmptyOrNullAdminTest()
        //{
        //    Group group = new Group();
        //    Assert.Throws<DomainException>(() =>
        //        group.Admin = null
        //    );
        //    Assert.Throws<DomainException>(() =>
        //        group.Admin = 1
        //    );
        //}
    }
}
