﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace address_book_web
{
    public class TestBase
    {
        protected ApplicationManager app;
        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstanse();
        }

    }
}
